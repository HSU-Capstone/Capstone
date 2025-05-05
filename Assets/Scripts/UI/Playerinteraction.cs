using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UI;

public class Playerinteraction : MonoBehaviour
{
    public float interactionDistance = 10f;
    public LayerMask interactableLayer;
    public Camera playerCamera;

    private InteractableObject currentTarget = null;
    private InteractableObject openedTarget = null; // �˾� ���� ������Ʈ ���

    void Update()
    {

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionDistance, Color.red);
        if (Physics.Raycast(ray, out hit, interactionDistance, interactableLayer))
        {
            GameObject hitObj = hit.collider.gameObject;
            InteractableObject interactable = hitObj.GetComponent<InteractableObject>();

            if (interactable != null)
            {
                if (currentTarget != interactable)
                {
                    if (currentTarget != null)
                        currentTarget.HideArrowUI();

                    currentTarget = interactable;
                    currentTarget.ShowArrowUI();
                    Debug.Log("New target: " + currentTarget.gameObject.name);
                }

                // FŰ�� ������ �� Tag�� ���� ��ȣ�ۿ�
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if (hitObj.CompareTag("Introduce"))
                    {
                        interactable.Interact();
                        openedTarget = interactable;
                    }
                    else if (hitObj.CompareTag("Door"))
                    {
                        OpenDoor(hitObj);
                       

                    }
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

        // ESC Ű�� �˾� �ݱ�
        if (openedTarget != null && Input.GetKeyDown(KeyCode.Escape))
        {
            if (openedTarget.IsPopupOpen())
            {
                openedTarget.ClosePopupUI();
                openedTarget = null;
            }
        }
    }

    private void OpenDoor(GameObject door)
    {
        //�� ����
        Quaternion openRotation = Quaternion.Euler(0, 90, 0);
        door.transform.rotation = openRotation;
        Debug.Log("���� ���Ƚ��ϴ�: " + door.name);

    }
}
