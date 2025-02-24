using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HammerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�

    private void Update()
    {
        // WASD Ű �Է� ���� (W = ��, A = ����, S = �Ʒ�, D = ������)
        float horizontal = 0f;
        float vertical = 0f;

        // W (���� �̵�) => Z�� ����
        if (Input.GetKey(KeyCode.W))
        {
            vertical = 1f;
        }
        // S (�Ʒ��� �̵�) => Z�� ����
        else if (Input.GetKey(KeyCode.S))
        {
            vertical = -1f;
        }

        // A (�������� �̵�) => X�� ����
        if (Input.GetKey(KeyCode.A))
        {
            horizontal = -1f;
        }
        // D (���������� �̵�) => X�� ����
        else if (Input.GetKey(KeyCode.D))
        {
            horizontal = 1f;
        }

        // �̵� ���� ���� ���
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;

        // ��ġ �̵�
        transform.Translate(moveDirection);
    }
}

