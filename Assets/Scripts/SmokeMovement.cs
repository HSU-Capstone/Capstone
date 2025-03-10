using System.Collections;
using UnityEngine;

public class SmokeMovement : MonoBehaviour
{
    public Transform[] points; // 5���� ����
    public float moveSpeed = 2f; // ���� �̵� �ӵ�
    public Camera mainCamera; // ���� ī�޶�
    public Transform[] zoomPoints; // �� ���� Ȯ�� ��ġ
    public GameObject[] uiScreens; // �� ������ UI ȭ��

    private int currentPoint = 0;
    private bool isMoving = false;

    private Vector3 initialCameraPosition;
    private Quaternion initialCameraRotation;

    void Start()
    {
        // �ʱ� ī�޶� ��ġ ����
        initialCameraPosition = mainCamera.transform.position;
        initialCameraRotation = mainCamera.transform.rotation;
    }

    void Update()
    {
        if (!isMoving && Input.GetMouseButtonDown(0))
        {
            if (currentPoint < points.Length)
            {
                StartCoroutine(MoveToNextPoint());
            }
            else
            {
                // ���� ������Ʈ ��Ȱ��ȭ
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator MoveToNextPoint()
    {
        isMoving = true;

        if (currentPoint >= points.Length) yield break;

        // ���� �̵�
        Vector3 startPos = transform.position;
        Vector3 targetPos = points[currentPoint].position;
        float journey = 0f;

        while (journey < 1f)
        {
            journey += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos, targetPos, journey);
            yield return null;
        }

        // ī�޶� Ȯ�� �� UI ǥ��
        mainCamera.transform.position = zoomPoints[currentPoint].position;
        mainCamera.transform.rotation = zoomPoints[currentPoint].rotation;

        uiScreens[currentPoint].SetActive(true);
        currentPoint++; // ���⼭ ����

        // ���콺 Ŭ�� ��� �� ����ġ ����
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        mainCamera.transform.position = initialCameraPosition;
        mainCamera.transform.rotation = initialCameraRotation;

        uiScreens[currentPoint - 1].SetActive(false);

        isMoving = false;
    }
}
