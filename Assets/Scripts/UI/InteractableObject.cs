using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject popupUI; // ��� UI ������Ʈ ����
    public GameObject arrowUI;
    private bool isPopupOpen = false; // ���� �˾� �����ִ��� ���� üũ

    // �̰� FŰ�� ������ �� ȣ��ȴ�
    public void Interact()
    {
        ShowPopupUI();
    }

    public void ShowPopupUI()
    {
        if (popupUI != null)
        {
            popupUI.SetActive(true);
            isPopupOpen = true;
            Debug.Log(gameObject.name + " popup UI opened!");
        }
        else
        {
            Debug.LogWarning("Popup UI�� ����Ǿ� ���� �ʽ��ϴ�!");
        }
    }

    public void ClosePopupUI()
    {
        if (popupUI != null)
        {
            popupUI.SetActive(false);
            isPopupOpen = false;
            Debug.Log(gameObject.name + " popup UI closed!");
        }
    }

    public bool IsPopupOpen()
    {
        return isPopupOpen;
    }

    public void ShowArrowUI()
    {
        if (arrowUI != null)
        {
            arrowUI.SetActive(true);
        }
    }

    public void HideArrowUI()
    {
        if (arrowUI != null)
        {
            arrowUI.SetActive(false);
        }
    }
}
