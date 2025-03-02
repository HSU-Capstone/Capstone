using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float movementSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero; // ����

        // �̵� 
        // �� 
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            dir += Vector3.forward;
        }
        // ��
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            dir += Vector3.back;
        }
        // ��
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            dir += Vector3.right;
        }
        // ��
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            dir += Vector3.left;
        }

        // �����̴� ���� ���� �ϱ�
        dir = dir.normalized;
        if (dir.magnitude > 0.5f)
        {
            transform.LookAt(transform.position + dir);
        }
    }
}
