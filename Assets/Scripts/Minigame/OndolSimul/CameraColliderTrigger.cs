using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColliderTrigger : MonoBehaviour



{
    public GameObject[] uiElements;  // �� UI â�� �迭�� ����
    private bool[] isColliding;      // �� collider���� �浹 ���θ� ����

    private void Start()
    {
        isColliding = new bool[uiElements.Length];

        // ��� UI â�� ��Ȱ��ȭ
        foreach (var ui in uiElements)
        {
            ui.SetActive(false);
        }
    }

    // �浹 ���� �� ȣ��
    private void OnCollisionEnter(Collision collision)
    {
        // �浹�� ��ü�� �±׳� �̸��� �������� Ȱ��ȭ�� UI ã��
        for (int i = 0; i < uiElements.Length; i++)
        {
            if (collision.gameObject.CompareTag("Collider" + (i + 1)))
            {
                uiElements[i].SetActive(true);
                isColliding[i] = true;
                break;
            }
        }
    }

    // �浹 ���� �� ȣ��
    private void OnCollisionExit(Collision collision)
    {
        for (int i = 0; i < uiElements.Length; i++)
        {
            if (collision.gameObject.CompareTag("Collider" + (i + 1)) && isColliding[i])
            {
                uiElements[i].SetActive(false);
                isColliding[i] = false;
                break;
            }
        }
    }
}
