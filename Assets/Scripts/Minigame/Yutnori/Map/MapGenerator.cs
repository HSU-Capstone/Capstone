using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeightedPOI
{
    public PointOfInterest prefab;
    [Range(0.1f, 10f)]
    public float weight = 1f;
}
public class ColumnInfo
{
    public bool hasComponentPOI; // ������Ʈ POI ���� ����
    public int lowestRowIndex;   // ��ü ������ ���� ��
}

// ���� �����ϴ� Ŭ����
public class MapGenerator : MonoBehaviour
{
    // �� ������Ʈ���� �� �θ� Transform
    [SerializeField] private Transform boardContainer;

    [SerializeField]
    private List<WeightedPOI> weightedPointsOfInterestPrefabs = new();

    private ColumnInfo[] _columnStatus; // �÷��� ���� ����
    [SerializeField]
    private PointOfInterest componentPOI; // �ʼ� ���� POI ������ (�ν����� �Ҵ�)
    [SerializeField]
    private PointOfInterest shortcutPOI;

    // ���(����) ������
    [SerializeField] private GameObject pathPrefab;
    // ���� ������ ����
    [SerializeField] private int numberOfStartingPoints = 4;
    // ���� ���� ����(�� ��)
    [SerializeField] private int mapLength = 10;
    // ���� ���� �ִ� ��
    [SerializeField] private int maxWidth = 5;
    // ���� ���� �ִ� ũ��
    [SerializeField] private float xMaxSize;
    // �� �� y�� ����
    [SerializeField] private float yPadding;
    // ��ΰ� �����ϴ� ���� ������� ����
    [SerializeField] private bool allowCrisscrossing;
    // �߰� ��� ���� Ȯ��
    [Range(0.1f, 1f), SerializeField] private float chancePathMiddle;
    // �翷 ��� ���� Ȯ��
    [Range(0f, 1f), SerializeField] private float chancePathSide;
    // ���� ���ݿ� �������� ��
    [SerializeField, Range(0.9f, 5f)] private float multiplicativeSpaceBetweenLines = 2.5f;
    // �ּ� ���� ������ �������� ��
    [SerializeField, Range(1f, 5.5f)] private float multiplicativeNumberOfMinimunConnections = 3f;

    // �� ���� PointOfInterest 2���� �迭
    private PointOfInterest[][] _pointOfInterestsPerFloor;
    // ������ PointOfInterest ����Ʈ
    private readonly List<PointOfInterest> pointsOfInterest = new();
    // ������� ������ ����(����) ����
    private int _numberOfConnections = 0;
    // ����(���) ����
    private float _lineLength;
    // ����(���) ����
    private float _lineHeight;

    // ���� ���� �� �� ����
    private void Start()
    {
        RecreateBoard();
    }

    // ���� ���� �����ϴ� �Լ�
    public void RecreateBoard()
    {
        // ���� �������� ���� ���̿� ���� ���
        _lineLength = pathPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.z * pathPrefab.transform.localScale.z;
        _lineHeight = pathPrefab.GetComponent<MeshFilter>().sharedMesh.bounds.size.y * pathPrefab.transform.localScale.y;
        // ���� �� ������Ʈ ��� ����
        DestroyImmediateAllChildren(boardContainer);
        // ���� ���� �ʱ�ȭ
        _numberOfConnections = 0;
        // ���� �õ� �ʱ�ȭ
        GenerateRandomSeed();
        // ���� PointOfInterest ����Ʈ �ʱ�ȭ
        pointsOfInterest.Clear();
        // ���� PointOfInterest �迭 �ʱ�ȭ
        _pointOfInterestsPerFloor = new PointOfInterest[mapLength][];
        for (int i = 0; i < _pointOfInterestsPerFloor.Length; i++)
        {
            _pointOfInterestsPerFloor[i] = new PointOfInterest[maxWidth];
        }

        // �÷� ���� �ʱ�ȭ �߰�
        _columnStatus = new ColumnInfo[maxWidth];
        for (int i = 0; i < maxWidth; i++)
            _columnStatus[i] = new ColumnInfo { lowestRowIndex = -1 };

        // �� ����
        CreateMap();
    }

    // ���� �õ� ���� �Լ�
    private void GenerateRandomSeed()
    {
        int tempSeed = (int)System.DateTime.Now.Ticks;
        Random.InitState(tempSeed);
    }

    // PointOfInterest�� �����ϰ�, ���� ���� ������ ��������� �����ϴ� �Լ�
    private PointOfInterest InstantiatePointOfInterest(int floorN, int xNum)
    {

        if ((floorN + 1) % 5 == 0)
            return _pointOfInterestsPerFloor[floorN][xNum];

        // �̹� �ش� ��ġ�� PointOfInterest�� ������ ��ȯ
        if (_pointOfInterestsPerFloor[floorN][xNum] != null)
        {
            return _pointOfInterestsPerFloor[floorN][xNum];
        }

        // x�� �� ĭ�� ũ�� ���
        float xSize = xMaxSize / maxWidth;
        // x, y ��ġ ���
        float xPos = (xSize * xNum) + (xSize / 2f);
        float yPos = yPadding * floorN;

        // ��ġ�� ���� �е� �߰�
        xPos += Random.Range(-xSize / 4f, xSize / 4f);
        yPos += Random.Range(-yPadding / 4f, yPadding / 4f);

        // ���� ��ġ ���� ����
        Vector3 pos = new Vector3(xPos, 0, yPos);
        // ���� PointOfInterest ������ ����
        //PointOfInterest randomPOI = pointsOfInterestPrefabs[Random.Range(0, pointsOfInterestPrefabs.Count)];
        PointOfInterest randomPOI = GetWeightedRandomPOI();

        // �ν��Ͻ� ���� �� ����Ʈ�� �߰�
        PointOfInterest instance = Instantiate(randomPOI, boardContainer);
        pointsOfInterest.Add(instance);

        // ��ġ ����
        instance.transform.localPosition = pos;
        // ���� �迭�� ����
        _pointOfInterestsPerFloor[floorN][xNum] = instance;
        // ������ ���� ����
        int created = 0;

        // ���� ���� PointOfInterest�� �����ϰ� �����ϴ� ���� �Լ�
        void InstantiateNextPoint(int index_i, int index_j)
        {
            PointOfInterest nextPOI = InstantiatePointOfInterest(index_i, index_j);
            AddLineBetweenPoints(instance, nextPOI);
            instance.NextPointsOfInterest.Add(nextPOI);
            created++;
            _numberOfConnections++;
        }

        // ������ �ϳ��� �������� �ʾҰ�, ������ ���� �ƴϸ� ���� �õ�
        while (created == 0 && floorN < mapLength - 1)
        {
            // ���� �밢�� ���� �õ�
            if (xNum > 0 && Random.Range(0f, 1f) < chancePathSide)
            {
                if (allowCrisscrossing || _pointOfInterestsPerFloor[floorN + 1][xNum] == null)
                {
                    InstantiateNextPoint(floorN + 1, xNum - 1);
                }
            }

            // ������ �밢�� ���� �õ�
            if (xNum < maxWidth - 1 && Random.Range(0f, 1f) < chancePathSide)
            {
                if (allowCrisscrossing || _pointOfInterestsPerFloor[floorN + 1][xNum] == null)
                {
                    InstantiateNextPoint(floorN + 1, xNum + 1);
                }
            }

            // ���� ���� �õ�
            if (Random.Range(0f, 1f) < chancePathMiddle)
            {
                InstantiateNextPoint(floorN + 1, xNum);
            }

            if (randomPOI == componentPOI)
                _columnStatus[xNum].hasComponentPOI = true;

            if (_columnStatus[xNum].lowestRowIndex < floorN)
                _columnStatus[xNum].lowestRowIndex = floorN;

        }

        return instance;
    }

    // ���� ������ �����ϴ� �Լ�
    private void CreateMap()
    {
        // �÷� ���� �ʱ�ȭ
        _columnStatus = new ColumnInfo[maxWidth];
        for (int i = 0; i < maxWidth; i++)
            _columnStatus[i] = new ColumnInfo { lowestRowIndex = -1 };

        // 1. ��Ģ ��(5��° ��)�� Shortcut POI ���� ----------------------------
        for (int floor = 0; floor < mapLength; floor++)
        {
            if ((floor + 1) % 5 == 0) // 1-based 5��° �� (0-based: 4,9,14...)
            {
                for (int col = 0; col < maxWidth; col++)
                {
                    Vector3 pos = CalculatePosition(floor, col);
                    PointOfInterest instance = Instantiate(shortcutPOI, boardContainer);
                    instance.transform.localPosition = pos;
                    _pointOfInterestsPerFloor[floor][col] = instance;
                    pointsOfInterest.Add(instance);
                }
            }
        }

        // 2. ���� ������ ���� ���� -------------------------------------------
        List<int> positions = GetRandomIndexes(numberOfStartingPoints);
        foreach (int j in positions)
        {
            _ = InstantiatePointOfInterest(0, j);
        }

        // 3. ��Ģ �� ���� �� ���� ���� �߰� ----------------------------------
        for (int floor = 0; floor < mapLength; floor++)
        {
            if ((floor + 1) % 5 == 0 && floor < mapLength - 1) // ������ �� ����
            {
                for (int col = 0; col < maxWidth; col++)
                {
                    // �̹� ������ Shortcut POI���� ���� ������ ���� ����
                    CreateConnectionsFromShortcut(floor, col);
                }
            }
        }

        // 4. ������Ʈ POI ���� ���� ------------------------------------------
        CheckAndEnforceComponentPOI();

        // 5. ���� ���� ���� -------------------------------------------------
        if (_numberOfConnections <= mapLength * multiplicativeNumberOfMinimunConnections)
        {
            Debug.Log($"Recreating board with {_numberOfConnections} connections");
            RecreateBoard();
            return;
        }

        Debug.Log($"Created board with {_numberOfConnections} connections");
        Debug.Log($"Created board with {pointsOfInterest.Count} points");
    }

    private void CreateConnectionsFromShortcut(int floor, int col)
    {
        PointOfInterest shortcutNode = _pointOfInterestsPerFloor[floor][col];
        if (shortcutNode == null) return;

        // ������ ���� ī��Ʈ
        int created = 0;

        // �߰�, ����, ������ ���� �õ� (���� Ȯ�� ����)
        if (col > 0 && Random.Range(0f, 1f) < chancePathSide)
        {
            if (allowCrisscrossing || _pointOfInterestsPerFloor[floor + 1][col] == null)
            {
                CreateConnectionToNextFloor(shortcutNode, floor, col, col - 1);
                created++;
            }
        }

        if (col < maxWidth - 1 && Random.Range(0f, 1f) < chancePathSide)
        {
            if (allowCrisscrossing || _pointOfInterestsPerFloor[floor + 1][col] == null)
            {
                CreateConnectionToNextFloor(shortcutNode, floor, col, col + 1);
                created++;
            }
        }

        // ���� ���� �õ� (��Ȳ�� ���� ���� ����)
        if (created == 0 || Random.Range(0f, 1f) < chancePathMiddle)
        {
            CreateConnectionToNextFloor(shortcutNode, floor, col, col);
        }
    }

    private void CreateConnectionToNextFloor(PointOfInterest source, int currentFloor, int currentCol, int nextCol)
    {
        // ���� ���� POI ���� �Ǵ� ��������
        PointOfInterest nextPOI = InstantiatePointOfInterest(currentFloor + 1, nextCol);

        // �ð��� ���ἱ �߰�
        AddLineBetweenPoints(source, nextPOI);

        // ���� ���� ���� ����
        source.NextPointsOfInterest.Add(nextPOI);

        // ���� ī��Ʈ ����
        _numberOfConnections++;

        // �÷� ���� ������Ʈ (���� �� ��尡 ������Ʈ�� ���)
        if (nextPOI != null && nextPOI.GetType() == componentPOI.GetType())
        {
            _columnStatus[nextCol].hasComponentPOI = true;
        }

        // ���� �� �ε��� ������Ʈ
        if (_columnStatus[nextCol].lowestRowIndex < currentFloor + 1)
        {
            _columnStatus[nextCol].lowestRowIndex = currentFloor + 1;
        }
    }


    private void CheckAndEnforceComponentPOI()
    {
        for (int col = 0; col < maxWidth; col++)
        {
            if (!_columnStatus[col].hasComponentPOI)
            {
                if (_columnStatus[col].lowestRowIndex == -1)
                {
                    Debug.LogError($"�÷� {col}�� POI�� ���� ������Ʈ ���� �Ұ�!");
                    continue;
                }
                ReplacePOIWithComponent(_columnStatus[col].lowestRowIndex, col);
            }
        }
    }

    private Vector3 CalculatePosition(int floorN, int xNum)
    {
        float xSize = xMaxSize / maxWidth;
        float xPos = (xSize * xNum) + (xSize / 2f);
        float yPos = yPadding * floorN;

        xPos += Random.Range(-xSize / 4f, xSize / 4f);
        yPos += Random.Range(-yPadding / 4f, yPadding / 4f);

        return new Vector3(xPos, 0, yPos);
    }

    private void ReplacePOIWithComponent(int floor, int col)
    {
        // ���� POI ����
        PointOfInterest oldPOI = _pointOfInterestsPerFloor[floor][col];
        if (oldPOI != null)
        {
            pointsOfInterest.Remove(oldPOI);
            DestroyImmediate(oldPOI.gameObject);
        }

        // ������Ʈ POI ����
        Vector3 pos = CalculatePosition(floor, col);
        PointOfInterest instance = Instantiate(componentPOI, boardContainer);
        instance.transform.localPosition = pos;
        _pointOfInterestsPerFloor[floor][col] = instance;
        pointsOfInterest.Add(instance);

        // ���� ���� ������Ʈ
        foreach (var poi in pointsOfInterest)
        {
            poi.NextPointsOfInterest.RemoveAll(p => p == oldPOI);
            if (poi != instance)
                poi.NextPointsOfInterest.Add(instance);
        }
    }


    private PointOfInterest GetWeightedRandomPOI()
    {
        if (weightedPointsOfInterestPrefabs.Count == 0)
            throw new System.Exception("������ ����Ʈ�� ����ֽ��ϴ�!");

        // �� ����ġ ���
        float totalWeight = 0f;
        foreach (var wpoi in weightedPointsOfInterestPrefabs)
            totalWeight += wpoi.weight;

        if (totalWeight <= Mathf.Epsilon) // ��� ����ġ�� 0�� ���
            return weightedPointsOfInterestPrefabs[Random.Range(0, weightedPointsOfInterestPrefabs.Count)].prefab;

        // ����ġ ��� ����
        float randomValue = Random.Range(0f, totalWeight);
        float cumulative = 0f;
        foreach (var wpoi in weightedPointsOfInterestPrefabs)
        {
            cumulative += wpoi.weight;
            if (randomValue <= cumulative)
                return wpoi.prefab;
        }

        return weightedPointsOfInterestPrefabs[0].prefab; // fallback
    }


    // �� PointOfInterest ���̿� ����(���) ������Ʈ�� �����ϴ� �Լ�
    private void AddLineBetweenPoints(PointOfInterest thisPoint, PointOfInterest nextPoint)
    {
        float len = _lineLength;
        float height = _lineHeight;

        // �� �� ������ ���� ���� ���
        Vector3 dir = (nextPoint.transform.position - thisPoint.transform.position).normalized;

        // �� �� ������ �Ÿ� ���
        float dist = Vector3.Distance(thisPoint.transform.position, nextPoint.transform.position);

        // �� �� ���̿� �� ���� ���� ��� (�е� ����)
        int num = (int)(dist / (len * multiplicativeSpaceBetweenLines));

        // ���� �е� �Ÿ� ��� (num�� ������ ���� �Ÿ� �й�)
        float pad = (dist - (num * len)) / (num + 1);

        // ù ��° ������ ��ġ ��� (len/2f�� ���� �߽�)
        Vector3 pos_i = thisPoint.transform.position + (dir * (pad + (len / 2f)));

        // ��� ���� ��ġ
        for (int i = 0; i < num; i++)
        {
            Vector3 pos = pos_i + ((len + pad) * i * dir);
            GameObject lineCreated = Instantiate(pathPrefab, pos, Quaternion.identity, boardContainer);
            // ������ ���� ����Ʈ�� �ٶ󺸵��� ȸ��
            lineCreated.transform.LookAt(nextPoint.transform);
            // ���� ���̸�ŭ �Ʒ��� ���� (�߽� ���߱�)
            lineCreated.transform.position -= Vector3.up * (height / 2f);
        }
    }

    // 0~maxWidth-1 �� n���� �����ϰ� �̴� �Լ� (�ߺ� ����)
    private List<int> GetRandomIndexes(int n)
    {
        List<int> indexes = new List<int>();
        if (n > maxWidth)
        {
            throw new System.Exception("Number of starting points greater than maxWidth!");
        }

        while (indexes.Count < n)
        {
            int randomNum = Random.Range(0, maxWidth);
            if (!indexes.Contains(randomNum))
            {
                indexes.Add(randomNum);
            }
        }
        return indexes;
    }

    // Transform�� ��� �ڽ� ������Ʈ�� ��� �����ϴ� �Լ�
    private void DestroyImmediateAllChildren(Transform transform)
    {
        List<Transform> toKill = new();

        // ������ �ڽ� Transform ����Ʈ�� �߰�
        foreach (Transform child in transform)
        {
            toKill.Add(child);
        }

        // �ڿ������� ���� (�����ϰ�)
        for (int i = toKill.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(toKill[i].gameObject);
        }
    }
}
