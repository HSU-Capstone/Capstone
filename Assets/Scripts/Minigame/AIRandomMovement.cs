using UnityEngine;
using UnityEngine.AI;  // NavMesh�� ����Ϸ��� �ݵ�� �߰��ؾ� �մϴ�.

public class AIRandomMovement : MonoBehaviour
{
    public float changeInterval = 4f;  // 4�ʸ��� �ִϸ��̼� ���� �� ȸ�� ����
    private float timer = 0f;  // Ÿ�̸�

    // �ִϸ��̼ǰ� ȸ��, ��ġ ����
    private Vector3 rotation1 = new Vector3(0, 715.33f, 0);
    private Vector3 rotation2 = new Vector3(0, 713.67f, 0);
    private Vector3 rotation3 = new Vector3(-244.8f, 729.25f, -715.8f);
    private float positionY = 34.92f;

    private int currentState = 0;  // �ִϸ��̼� ���� ���� (0: ���� 1, 1: ���� 2, 2: ���� 3)
    private Animator animator;
    private NavMeshAgent agent;  // NavMeshAgent �߰�

    public Transform[] targetPoints;  // �̵��� ��ǥ ������
    private int currentTargetIndex = 0;  // ���� ��ǥ ���� �ε���

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();  // NavMeshAgent ������Ʈ �Ҵ�

        // ù ��° ��ǥ �������� �̵� ����
        if (targetPoints.Length > 0)
        {
            agent.SetDestination(targetPoints[currentTargetIndex].position);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;  // ��� �ð� ���

        // 4�ʸ��� ȸ�� �� �ִϸ��̼� ����
        if (timer >= changeInterval)
        {
            timer = 0f;  // Ÿ�̸� �ʱ�ȭ
            ChangeRotationAndAnimation();  // ȸ�� ���� �ִϸ��̼� ����
        }

        // NavMeshAgent�� ��ǥ ������ ��������� ���� ��ǥ�� �̵�
        if (agent.remainingDistance < 1f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % targetPoints.Length;  // ��ǥ ��ȯ
            agent.SetDestination(targetPoints[currentTargetIndex].position);  // ���� ��ǥ �������� �̵�
        }

        // �̵� ���¿� �´� �ִϸ��̼� ó��
        if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsRunning", false);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
        }
    }

    // ȸ�� �� �ִϸ��̼� ���¸� �����ϴ� �Լ�
    private void ChangeRotationAndAnimation()
    {
        currentState = (currentState + 1) % 4;  // ���� ��ȯ (0 -> 1 -> 2 -> 3)

        switch (currentState)
        {
            case 0:
                // ���� 1: Y_ROTATION = 715.33
                transform.rotation = Quaternion.Euler(rotation1);
                animator.SetTrigger("Idle");  // �ִϸ��̼� ���� (Idle)
                break;

            case 1:
                // ���� 2: Y_ROTATION = 713.67
                transform.rotation = Quaternion.Euler(rotation2);
                animator.SetTrigger("Walk");  // �ִϸ��̼� ���� (Walk)
                break;

            case 2:
                // ���� 3: X_ROTATION = -244.8, Y_ROTATION = 729.25, Z_ROTATION = -715.8
                transform.rotation = Quaternion.Euler(rotation3);
                animator.SetTrigger("Run");  // �ִϸ��̼� ���� (Run)
                break;

            case 3:
                // ���� 4: Y_POSITION = 34.92
                transform.position = new Vector3(transform.position.x, positionY, transform.position.z);
                animator.SetTrigger("Attack");  // �ִϸ��̼� ���� (Attack)
                break;
        }
    }
}
