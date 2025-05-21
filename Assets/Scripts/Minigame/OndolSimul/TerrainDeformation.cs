using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TerrainDeformation : MonoBehaviour
{
    public Terrain terrain;  // Terrain ��ü
    public Vector3 holePosition; // �������� ��ġ
    public float holeRadius = 3f;  // ������ ������
    public float holeDepth = 2f;   // �������� ����

    void Start()
    {
        DeformTerrain();
    }

    void DeformTerrain()
    {
        // Terrain�� ���̸� ��������
        TerrainData terrainData = terrain.terrainData;
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution; ;

        // ���̸� �迭 ��������
        float[,] heights = terrainData.GetHeights(0, 0, width, height);

        // ����ģ �κ��� ��ġ�� �ݰ濡 ���� ���̰��� ����
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                // ���� ��ġ�� ���� �߽� �Ÿ� ���
                Vector3 worldPos = terrain.transform.TransformPoint(x, 0, z);
                float distance = Vector3.Distance(worldPos, holePosition);

                if (distance < holeRadius)
                {
                    // ����ģ ���̸�ŭ ���̸� ���߱�
                    float depthFactor = Mathf.Clamp01(1 - (distance / holeRadius));  // �Ÿ� ������ ���� ���� ���
                    heights[x, z] -= depthFactor * holeDepth;
                }
            }
        }

        // ������ ���̸� �ٽ� ����
        terrainData.SetHeights(0, 0, heights);
    }
}

