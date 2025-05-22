using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float rotationSpeed = 3.0f;
    public float moveSpeed = 5.0f;
    private Vector2 currentRotation;
    private bool isRotating = false;

    void Start()
    {
        // ���� �� ī�޶� ȸ�� �ʱⰪ�� (0, 180, 0)���� ����
        currentRotation = new Vector2(0, 180);
    }

    void Update()
    {
        // ���콺 ������ ��ư ������ �ִ� ���� ȸ��
        if (Input.GetMouseButton(1))  // 1 = ���콺 ������ ��ư
        {
            isRotating = true;

            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            currentRotation.x -= mouseY;
            currentRotation.y += mouseX;

            // ���� ȸ�� ���� (ī�޶� �������� �ʵ���)
            currentRotation.x = Mathf.Clamp(currentRotation.x, -90f, 90f);

            // ȸ�� ���� (���Ϸ� ��)
            transform.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        }
        else
        {
            isRotating = false;
        }

        // ���콺 ������ ��ư �� ������ ���� �̵� ���
        if (!isRotating)
        {
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            transform.Translate(moveX, 0, moveZ);
        }
    }

}
