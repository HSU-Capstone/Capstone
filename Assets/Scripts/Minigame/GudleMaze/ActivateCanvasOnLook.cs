using UnityEngine;

public class ActivateCanvasOnLook : MonoBehaviour
{
    public GameObject canvasToActivate;       // ������ ĵ����
    public float detectionDistance = 5f;       // ť�� ���� �Ÿ�
    public string targetObjectTag = "TargetCube"; // ������ ť�� �±�

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ī�޶� �������� Ray �߻�
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, detectionDistance))
            {
                if (hit.collider.CompareTag(targetObjectTag))
                {
                    canvasToActivate.SetActive(true);
                }
            }
        }
    }
}
