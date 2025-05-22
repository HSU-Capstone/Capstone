using UnityEngine;

public class ClickToActivateUI : MonoBehaviour
{
    public GameObject uiPanelToActivate; // Ŭ�� �� Ȱ��ȭ�� UI

    void OnMouseDown()
    {
        if (uiPanelToActivate != null)
        {
            uiPanelToActivate.SetActive(true);
        }
    }
}
