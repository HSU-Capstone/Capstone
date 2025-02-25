using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SaveObject : MonoBehaviour
{
    private bool isLocked = false; // �̵� �Ұ��� �������� Ȯ��

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("dug"))
        {
            // "save" ������Ʈ�� "dug" ������Ʈ ��ġ�� �̵�
            transform.position = other.transform.position;

            // "dug" ������Ʈ ����
            Destroy(other.gameObject);

            // �̵� �Ұ��� ���·� ����
            isLocked = true;

            DisableMovement();

            // Rigidbody�� �ִٸ� �߷� �� �̵� ����
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // ������ �̵� ����
            }
        }
    }

    private void DisableMovement()
    {
        // Rigidbody�� �ִٸ� ������ �̵� ����
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;  // �������� ���� ���� �ʵ��� ����
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // �巡�� �̵��� �����ϴ� ��ũ��Ʈ�� �ִٸ� ��Ȱ��ȭ
        if (TryGetComponent(out ObjectDraggable draggable))
        {
            draggable.enabled = false;
        }

        // Transform ���� (�ʿ� ��)
        transform.SetParent(null); // �θ� �ִٸ� ����
    }
}

