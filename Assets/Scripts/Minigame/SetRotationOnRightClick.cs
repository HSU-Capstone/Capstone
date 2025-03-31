using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotationOnRightClick : MonoBehaviour
{
    public float targetZRotation = 70f; // ��ǥ Z ȸ�� ��
    public float rotationSpeed = 5f; // ȸ�� �ӵ�
    private Quaternion targetRotation; // ��ǥ ȸ����
    private bool isRotating = false; // ȸ�� �� ����

    void Start()
    {
        targetRotation = transform.rotation; // �ʱ� ȸ���� ����
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // ���콺 ������ ��ư Ŭ��
        {
            if (!isRotating) // �̹� ȸ�� ���� �ƴϸ� ����
            {
                isRotating = true;
                targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, targetZRotation);
            }
        }

        if (isRotating) // ȸ�� ���̸� �ε巴�� ȸ��
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // ��ǥ ȸ������ ���� �����ϸ� ����
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isRotating = false;
            }
        }
    }
}
