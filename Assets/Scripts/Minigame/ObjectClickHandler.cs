using UnityEngine;

public class ObjectClickHandler : MonoBehaviour
{
    public GameObject panel; // ���� ���� UI �г�

    private void Start()
    {
        // �г��� ������ �� ��Ȱ��ȭ
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    void Update()
    {
        // ���콺 Ŭ������ ������Ʈ�� Ŭ���� ���
        if (Input.GetMouseButtonDown(0))  // 0�� ���� ���콺 Ŭ��
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Ŭ���� ������Ʈ�� ���� ������Ʈ���� Ȯ��
                if (hit.collider.gameObject == gameObject)
                {
                    TogglePanel();
                }
            }
        }
    }

    // �г� ����/�ݱ� ���
    void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
