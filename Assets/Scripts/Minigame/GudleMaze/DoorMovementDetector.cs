using UnityEngine;

public class DoorMovementDetector : MonoBehaviour
{
    public Transform targetColliderTransform; // ������ �Ǵ� ��ġ (Collider�� �ִ� ������Ʈ�� Transform)
    public Transform playerCamera;            // 1��Ī ī�޶�
    public GameObject panelToActivate;        // Ȱ��ȭ�� UI Panel
    public float activationDistance = 3f;     // �Ÿ� ����

    private bool hasActivated = false;

    void Start()
    {
        if (panelToActivate != null)
            panelToActivate.SetActive(false);
    }

    void Update()
    {
        if (hasActivated || targetColliderTransform == null || playerCamera == null)
            return;

        float distance = Vector3.Distance(playerCamera.position, targetColliderTransform.position);

        if (distance <= activationDistance)
        {
            panelToActivate.SetActive(true);
            hasActivated = true;
            Debug.Log("�Ÿ��� ��������� �г��� Ȱ��ȭ��");
        }
    }
}
