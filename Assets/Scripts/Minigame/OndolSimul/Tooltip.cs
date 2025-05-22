using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public GameObject tooltipUI; // ���� UI (�̸� ��ġ�� World Space Canvas)

    void Start()
    {
        if (tooltipUI != null)
        {
            tooltipUI.SetActive(false); // ���� �� ��Ȱ��ȭ
        }
    }

    void OnMouseEnter()
    {
        if (tooltipUI != null)
        {
            tooltipUI.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        if (tooltipUI != null)
        {
            tooltipUI.SetActive(false);
        }
    }
}
