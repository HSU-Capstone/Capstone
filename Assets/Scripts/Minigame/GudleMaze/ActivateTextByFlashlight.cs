using UnityEngine;

public class ActivateTextByFlashlight : MonoBehaviour
{
    public Light flashlight; // Spot Light
    public GameObject textObject; // Ȱ��ȭ�� �ؽ�Ʈ ������Ʈ
    public float maxDistance = 10f;

    void Update()
    {
        if (flashlight.enabled)
        {
            Ray ray = new Ray(flashlight.transform.position, flashlight.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.collider.CompareTag("TargetText")) // �ؽ�Ʈ�� �� �±� ���̱�
                {
                    textObject.SetActive(true);
                    return;
                }
            }
        }

        // ���� ���� ������ ��Ȱ��ȭ
        textObject.SetActive(false);
    }
}
