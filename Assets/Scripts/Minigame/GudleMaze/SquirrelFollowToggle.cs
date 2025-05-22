using UnityEngine;
using UnityEngine.AI;

public class SquirrelFollowToggle : MonoBehaviour
{
    public Transform player;              // ���� ��� (ī�޶� �Ǵ� �÷��̾� ������Ʈ)
    public Transform[] wanderPoints;      // �����Ӱ� ���ƴٴ� ��ġ��
    private NavMeshAgent agent;
    private int pressCount = 0;
    private int currentWanderIndex = 0;
    private bool isFollowing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextWanderPoint();
    }

    void Update()
    {
        // QŰ ������ ���
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pressCount++;
            isFollowing = (pressCount % 2 == 1);

            if (isFollowing)
            {
                // �÷��̾� ����
                agent.SetDestination(player.position);
            }
            else
            {
                // ���� ������ ����
                GoToNextWanderPoint();
            }
        }

        // �����Ӱ� ���ƴٴϱ�
        if (!isFollowing && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextWanderPoint();
        }

        // ���󰡴� ���̸� ��� �÷��̾� ��ġ�� ������Ʈ
        if (isFollowing)
        {
            agent.SetDestination(player.position);
        }
    }

    void GoToNextWanderPoint()
    {
        if (wanderPoints.Length == 0) return;

        agent.destination = wanderPoints[currentWanderIndex].position;
        currentWanderIndex = (currentWanderIndex + 1) % wanderPoints.Length;
    }
}
