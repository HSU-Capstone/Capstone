using UnityEngine;

public class LockCursor : MonoBehaviour
{
    void Start()
    {
        // ���콺 Ŀ�� ��ױ� + �����
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ESC ������ ���콺 ��� ���� (�׽�Ʈ��)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
