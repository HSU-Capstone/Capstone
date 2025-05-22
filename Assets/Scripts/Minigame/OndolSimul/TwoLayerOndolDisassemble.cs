using UnityEngine;
using System.Collections;

public class OnDolDisassemble : MonoBehaviour
{
    public GameObject[] ondolParts;  // �µ� ��ǰ���� ���� �迭

    private Vector3[] initialPositions; // �� ��ǰ�� �ʱ� ��ġ
    private Quaternion[] initialRotations; // �� ��ǰ�� �ʱ� ȸ��

    public float animationDuration = 2f;  // �ִϸ��̼� ���� �ð�

    private Camera mainCamera;

    void Start()
    {
        // ���� ī�޶� ã��
        mainCamera = Camera.main;

        // �� ��ǰ�� �ʱ� ���� ����
        initialPositions = new Vector3[ondolParts.Length];
        initialRotations = new Quaternion[ondolParts.Length];

        for (int i = 0; i < ondolParts.Length; i++)
        {
            initialPositions[i] = ondolParts[i].transform.position;
            initialRotations[i] = ondolParts[i].transform.rotation;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� ��
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);  // ī�޶󿡼� Ŭ���� �������� ���� �߻�
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) // Ŭ���� ������ ������Ʈ�� ������
            {
                if (hit.collider.gameObject == this.gameObject)  // �� ��ũ��Ʈ�� ���� ������Ʈ(Cube)�� Ŭ��������
                {
                    StartCoroutine(DisassembleCoroutine());
                }
            }
        }
    }

    // �ε巴�� ��ü�ϴ� Coroutine
    IEnumerator DisassembleCoroutine()
    {
        float elapsedTime = 0f;

        // �� ��ǰ�� ��ǥ ��ġ�� ������ �̵�
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;

            for (int i = 0; i < ondolParts.Length; i++)
            {
                // �� ��ǰ�� �ε巴�� �̵�
                Vector3 targetPosition = initialPositions[i] + new Vector3(0, -3f, 0);  // ���÷� Y������ �������ٰ� ����
                ondolParts[i].transform.position = Vector3.Lerp(initialPositions[i], targetPosition, t);

                // ȸ���� �ε巴�� ���� ����
                Quaternion targetRotation = initialRotations[i] * Quaternion.Euler(0, 90f, 0);  // ȸ�� ����
                ondolParts[i].transform.rotation = Quaternion.Lerp(initialRotations[i], targetRotation, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ִϸ��̼� ���� �� ������ ��ġ�� ȸ��
        for (int i = 0; i < ondolParts.Length; i++)
        {
            ondolParts[i].transform.position = initialPositions[i] + new Vector3(0, -3f, 0);  // ���� ��ġ
            ondolParts[i].transform.rotation = initialRotations[i] * Quaternion.Euler(0, 90f, 0);  // ���� ȸ��
        }

        Debug.Log("�µ� ��ü �Ϸ�!");
    }
}
