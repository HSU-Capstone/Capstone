using UnityEngine;

public class ToggleCanvasByKey : MonoBehaviour
{
    public GameObject targetCanvas;  // Ȱ��ȭ/��Ȱ��ȭ�� Canvas ������Ʈ

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (targetCanvas != null)
            {
                // ���� ���� �ݴ�� ��ȯ (���� ������ ����, ���� ������ �Ѱ�)
                bool isActive = targetCanvas.activeSelf;
                targetCanvas.SetActive(!isActive);
            }
        }
    }
}
