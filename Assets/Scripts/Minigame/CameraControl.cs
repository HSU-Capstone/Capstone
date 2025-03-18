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
        // ���콺 ���� ��ư�� ������ ���� �� ī�޶� ȸ��
        if (Input.GetMouseButton(0))  // 0�� ���� ���콺 ��ư
        {
            isRotating = true;
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // ī�޶� ȸ�� ���
            currentRotation.x -= mouseY * rotationSpeed;
            currentRotation.y += mouseX * rotationSpeed;

            // ȸ�� ���� (���� ȸ�� ����)
            currentRotation.x = Mathf.Clamp(currentRotation.x, -90f, 90f);

            // ī�޶� ȸ�� ����
            transform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        }
        else
        {
            isRotating = false;
        }

        // WASD Ű�� �̵�
        if (!isRotating)
        {
            float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            // ī�޶� �̵�
            transform.Translate(moveX, 0, moveZ);
        }
    }
}
