using UnityEngine;
using UnityEngine.AI;

public class Squirrel : MonoBehaviour
{
    private NavMeshAgent agent;
    public float roamRadius = 5f;  // �ٶ��㰡 ���ƴٴ� �ݰ�
    public float waitTime = 2f;     // ��ǥ�� ���� �� ��ٸ� �ð�
    private float waitTimer;

    void Start()
    {
        // NavMeshAgent ������Ʈ ��������
        agent = GetComponent<NavMeshAgent>();
        agent.baseOffset = 0f;  // Y�࿡ ���� ������ ����
        agent.autoBraking = false;  // �ӵ� ���� ����
        agent.stoppingDistance = 0.1f;  // ��ǥ�� ���� �� ��������� �� ���߱�
        SetNewDestination();  // ù ��° ��ǥ ����
    }

    void Update()
    {
        // �������� �����ϸ� ��ٸ� �� ���ο� ��ǥ ����
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                SetNewDestination();
                waitTimer = 0f;
            }
        }
    }

    void SetNewDestination()
    {
        // ���� ��ġ ���
        Vector3 randomDirection = Random.insideUnitSphere * roamRadius;
        randomDirection += transform.position;

        // y���� ���� �ٶ����� y������ ���� (�ϴ÷� ���ư��� �ʵ���)
        randomDirection.y = transform.position.y;

        // ��ȿ�� NavMesh ��ġ���� Ȯ��
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamRadius, NavMesh.AllAreas))
        {
            // ���ο� ������ ����
            agent.SetDestination(hit.position);
        }
        else
        {
            // NavMesh ���� ��ġ���� ������ �ٽ� �õ�
            SetNewDestination();
        }
    }
}
