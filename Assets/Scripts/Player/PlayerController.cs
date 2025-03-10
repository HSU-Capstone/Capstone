using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator m_animator;
    private Vector3 m_velocity;

    private bool m_wasGrounded;
    private bool m_isGrounded = true;

    public float m_moveSpeed = 4.0f;
    public float m_jumpForce = 5.0f;

    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Animator �Ķ���� ������Ʈ
        m_animator.SetBool("Grounded", m_isGrounded);

        PlayerMove();

        // ���� ���� ������Ʈ
        m_wasGrounded = m_isGrounded;
    }

    private void PlayerMove()
    {
        CharacterController controller = GetComponent<CharacterController>();
        float gravity = 20.0f;

        if (controller.isGrounded)
        {
            // �̵� ���� ���. Ű���� �� ���� ���ӵǴ� �� ���� ���� GetAxis ��� GetAxisRaw
            m_velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            m_velocity = m_velocity.normalized;

            // ��������
            if (Input.GetKey(KeyCode.LeftShift)) 
            {
                // ������ movement�� ���ع����� �̼��� ���İ��� ��û Ŀ������
                m_velocity *= 2.0f;
            }
            // �ȱ�
            if (Input.GetKey(KeyCode.LeftControl)) 
            {
                m_velocity /= 2.0f;
            }
            // m_velocity.magnitude�� 0.5/1/2 �� �ϳ�. 
            m_animator.SetFloat("MoveSpeed", m_velocity.magnitude*m_moveSpeed);
        

            if (Input.GetKey(KeyCode.Space))
            {
                // ���� ó��
                m_animator.SetTrigger("Jump");
                m_velocity.y = m_jumpForce;
            }
            else if (m_velocity.magnitude > 0.1f)
            {
                transform.LookAt(transform.position + m_velocity); // �����̴� �������� ȸ��
            }
        }

        // �߷� ���� �� �̵� ó��
        m_velocity.y -= gravity * Time.deltaTime;
        controller.Move(m_velocity * m_moveSpeed * Time.deltaTime);

        // ���� ��Ҵ��� Ȯ��
        m_isGrounded = controller.isGrounded;
    }
}
