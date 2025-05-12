using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject popupUI; // ��� UI ������Ʈ ����
    public GameObject arrowUI;
    private bool isPopupOpen = false; // ���� �˾� �����ִ��� ���� üũ
    public AudioSource audioSource;
    public AudioClip UIopenSound;
    public AudioClip dooropenSound;



    //FŰ�� ������ �� ȣ��ȴ�
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

            // ȿ���� ���
            if (audioSource != null && UIopenSound != null)
            {
                audioSource.PlayOneShot(UIopenSound);
            }
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

    public void OpenDoor(GameObject door)
    {
        float currentY = door.transform.eulerAngles.y;
        float targetY;

        if (door.CompareTag("LeftDoor"))
        {
            targetY = currentY + 90f;
        }
        else if (door.CompareTag("RightDoor"))
        {
            targetY = currentY - 90f;
        }
        else
        {
            targetY = currentY; // �⺻��
        }

        Quaternion openRotation = Quaternion.Euler(0, targetY, 0);
        door.transform.rotation = openRotation;

        //�� ���� �Ҹ� ���
        if (audioSource != null && dooropenSound != null)
        {
            audioSource.PlayOneShot(dooropenSound);
        }

        Debug.Log("�� ����: " + door.name);
    }
}
