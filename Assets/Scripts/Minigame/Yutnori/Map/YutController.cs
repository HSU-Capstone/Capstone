using System.Collections;
using UnityEngine;

public class YutController : MonoBehaviour
{
    public Rigidbody[] yutRigidbodies;
    private Vector3 dragStartPos;
    private bool isDragging = false;
    [SerializeField] private float throwForce = 500f;
    [SerializeField] private LayerMask yutLayer;
    [SerializeField] private YutnoriGameManager gameManager;
    private Camera mainCamera;
    [SerializeField] private float velocityThreshold = 0.1f;  // ���� ���� �ӵ�
    [SerializeField] private float maxWaitTime = 5f;          // �ִ� ��� �ð� (�ν����Ϳ��� ����)
    [SerializeField] private float faceUpThreshold = 0.7f;    // �ո� ���� ���� (0.7 = 45�� �̳�)



    void Start()
    {
        mainCamera = Camera.main;
        yutRigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void StartDrag()
    {
        if (gameManager.stage != GameStage.Interact) return;
        if (gameManager.isDraggingYut) return;

        gameManager.SetYutDragState(true);
        isDragging = true;
        dragStartPos = Input.mousePosition;
    }

    public void EndDrag()
    {
        gameManager.SetYutDragState(false);
        if (!isDragging) return;

        // �� ��� �� ����
        Vector3 dragEndPos = Input.mousePosition;
        Vector3 forceDir = (dragEndPos - dragStartPos);

        // Y�� �� ��� (�⺻ 300f + �巡�� ���� ���� ����)
        float yForce = 300f + (forceDir.magnitude * 0.5f);
        yForce = Mathf.Clamp(yForce, 250f, 450f); // 250~450 ���� ����


        forceDir.z = forceDir.y;
        forceDir.y = yForce;

        foreach (var rb in yutRigidbodies)
        {
            //  ���� ���� (���� ���� ���� + ���� ����)
            Vector3 variedForce = new Vector3(
                forceDir.x * Random.Range(0.9f, 1.1f), // X�� ��10% ����
                forceDir.y * Random.Range(0.8f, 1.2f), // Y�� ��20% ����
                forceDir.z * Random.Range(0.9f, 1.1f)  // Z�� ��10% ����
            );

            //  �� ���� (ũ��� ���� ��� ����)
            rb.AddForce(variedForce.normalized * throwForce * Random.Range(0.95f, 1.05f));

            //  ȸ���� ���� ��ȭ
            rb.AddTorque(Random.insideUnitSphere * 80f); // ȸ���� 60% ����
        }

        StartCoroutine(DelayedStateCheck());
        isDragging = false;
    }

    private IEnumerator DelayedStateCheck()
    {
        yield return new WaitForSeconds(0.2f); // ���� ���� ��� �ð�
        StartCoroutine(CheckYutStateCoroutine());
    }

    // �ڷ�ƾ: �� ���� üũ
    private IEnumerator CheckYutStateCoroutine()
    {
        // 1�� �˻�: �ּ� 1ȸ �̻� ���������� Ȯ��
        bool hasMoved = false;
        float timeout = 3f; // ������ ���� Ÿ�Ӿƿ�

        while (timeout > 0 && !hasMoved)
        {
            foreach (var rb in yutRigidbodies)
            {
                if (rb.velocity.magnitude > 0.1f)
                {
                    hasMoved = true;
                    break;
                }
            }
            timeout -= Time.deltaTime;
            yield return null;
        }

        // 2�� �˻�: ���� ���� ����
        float elapsed = 0f;
        while (!AllYutsStopped() && elapsed < maxWaitTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        // ��� ���� �� ���
        int faceUpCount = CalculateFaceUpCount();
        string result = GetYutResult(faceUpCount);

        gameManager.setGameStage(GameStage.Move);
        Debug.Log($"���: {result} ({faceUpCount}�� �ո�)");
    }

    // ��� ���� �����ߴ��� Ȯ��
    private bool AllYutsStopped()
    {
        foreach (var rb in yutRigidbodies)
        {
            if (rb.velocity.magnitude > velocityThreshold ||
                rb.angularVelocity.magnitude > velocityThreshold)
                return false;
        }
        return true;
    }

    // �ո� ���� ���
    private int CalculateFaceUpCount()
    {
        int count = 0;
        foreach (var rb in yutRigidbodies)
        {
            if (Vector3.Dot(rb.transform.up, Vector3.up) > faceUpThreshold)
                count++;
        }
        return count;
    }

    // ��� ���ڿ� ��ȯ
    // ���� ���� �迭�� 0��°��� ����
    private string GetYutResult(int faceUpCount)
    {
        // ���� ����: 3�� ��, 1��(���� ��)�� ��
        if (faceUpCount == 3)
        {
            // ���� ���� ���������� Ȯ�� (��: 0��°)
            if (Vector3.Dot(yutRigidbodies[0].transform.up, Vector3.up) < -faceUpThreshold)
                return "����";
            else
                return "��";
        }
        else if (faceUpCount == 1)
        {
            return "��";
        }
        else if (faceUpCount == 2)
        {
            return "��";
        }
        else if (faceUpCount == 0)
        {
            return "��";
        }
        else if (faceUpCount == 4)
        {
            return "��";
        }
        return "ERROR";
    }


    void LateUpdate()
    {
        if (mainCamera == null) return;

        // ��� �ڽ� ��ġ ����
        foreach (var rb in yutRigidbodies)
        {
            Vector3 viewportPos = mainCamera.WorldToViewportPoint(rb.position);
            viewportPos.x = Mathf.Clamp(viewportPos.x, 0.05f, 0.95f);
            viewportPos.y = Mathf.Clamp(viewportPos.y, 0.05f, 0.95f);
            Vector3 clampedPos = mainCamera.ViewportToWorldPoint(viewportPos);
            rb.position = new Vector3(clampedPos.x, rb.position.y, clampedPos.z);
        }
    }
}
