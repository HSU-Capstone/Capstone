using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerinteraction : MonoBehaviour
{
    public float interactionDistance = 10f;
    public LayerMask interactableLayer;
    public Camera playerCamera;

    private InteractableObject currentTarget = null;
    private InteractableObject openedTarget = null; //�˾� ���� ������Ʈ ���

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

            if (interactable != null)
            {
                if (currentTarget != interactable)
                {
                    
                    if (currentTarget != null)
                    {
                        currentTarget.HideArrowUI();
                    }

                    currentTarget = interactable;
                    currentTarget.ShowArrowUI();

                    Debug.Log("New target: " + currentTarget.gameObject.name);
                }
            }
        }
        else
        {
            if (currentTarget != null)
            {
                currentTarget.HideArrowUI();
                Debug.Log("Lost target: " + currentTarget.gameObject.name);
                currentTarget = null;
            }
        }

        // FŰ�� ������ ���� UI ����
        if (currentTarget != null && Input.GetKeyDown(KeyCode.F))
        {
            currentTarget.Interact();
            openedTarget = currentTarget; //�˾� ���� ������Ʈ ���
        }

        //ESC Ű�� ������ �˾� �ݱ�
        if (openedTarget != null && Input.GetKeyDown(KeyCode.Escape))
        {
            if (openedTarget.IsPopupOpen())
            {
                openedTarget.ClosePopupUI();
                openedTarget = null; // �ݾ����� �ʱ�ȭ
            }
        }
    }
}
