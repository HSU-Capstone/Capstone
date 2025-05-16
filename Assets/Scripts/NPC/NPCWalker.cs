using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCWalker : MonoBehaviour
{
    /*
    - ���� �������� ���ϰ� �̵�.
    - ��ȭ���̸� ��� ����. ��ȭ�� ������ �簳.
    - �̵� �������� ���� Ȯ���� ����ٰ� �ٽ� �̵�.
    - ������ ������ ��� ����ٰ� �� ������ ���� �� �̵�. 
     */


    NavMeshAgent agent;
    Animator animator;
    NPCController controller;

    private bool prevIsWalking = false; // ���� �������� NPC �ȱ� ���¸� ����. ����/�̵� �Ǻ���
    private bool isWaiting = false; // ������ ���� �� ��� ������.
    private bool isPaused = false; // �̵� ���� ���� �������� (�ڿ��������� ����)
    private Vector3 currentDestination;

    // �˻� �ֱ�� Ȯ��. �� �ʸ��� �� �� Ȯ���� ��� ���� ����.
    private float pauseCheckInterval = 1.5f; // 1.5�ʸ��� �˻�
    private float pauseChance = 0.12f;       // 12% Ȯ��

    // ���� �ð� ����. 3~6�� ���� ����.
    public float minPauseTime = 3f;
    public float maxPauseTime = 6f;
    // ������ ���� �� ��� �ð��� �����ϰ� ����
    public float waitAtDestinationMin = 3f;
    public float waitAtDestinationMax = 6f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        controller = GetComponent<NPCController>();

        SetNewDestination();
        StartCoroutine(RandomPauseCheckRoutine());
    }

    void Update()
    {
        bool isTalking = controller != null && controller.getIsNPCTalking();

        // 1. ��ȭ ���̸� ��� ����(Idle)
        if (isTalking)
        {
            agent.isStopped = true;
            SetWalkingAnimation(false);
            return;
        }
        else
        {
            // ��ȭ�� ������ �̵� �簳
            if (!isPaused && agent.isStopped)
                agent.isStopped = false;
        }

        // 2. ������ ���� �� ���
        if (!isWaiting && !isPaused && !agent.pathPending &&
            agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude == 0f))
        {
            float waitTime = Random.Range(waitAtDestinationMin, waitAtDestinationMax);
            StartCoroutine(WaitAndMove(waitTime));
        }

        // �ִϸ��̼� ���� ��ȭ �ÿ��� ����
        bool isWalking = !agent.isStopped && agent.velocity.magnitude > 0.1f;
        SetWalkingAnimation(isWalking);
    }

    void SetNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 10f + transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
        {
            currentDestination = hit.position;
            agent.SetDestination(currentDestination);
        }
    }

    // ������ ���� �� ���, �� �� �� �������� �̵�
    IEnumerator WaitAndMove(float waitTime)
    {
        isWaiting = true;
        agent.isStopped = true;
        SetWalkingAnimation(false);
        yield return new WaitForSeconds(waitTime);
        SetNewDestination();
        agent.isStopped = false;
        isWaiting = false;
    }

    // �̵� �� ���� ���� (���� �������� �簳)
    IEnumerator PauseCoroutine(float waitTime)
    {
        isPaused = true;
        agent.isStopped = true;
        SetWalkingAnimation(false);
        yield return new WaitForSeconds(waitTime);
        agent.isStopped = false;
        isPaused = false;
        // �������� �״��, �̵� �簳
    }

    // 1.5�ʸ��� �̵� �� ���� ���� �˻�
    IEnumerator RandomPauseCheckRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(pauseCheckInterval);

            // �̵� ��, ���/�Ͻ�����/��ȭ ���� �ƴ� ���� �˻�
            if (!isPaused && !isWaiting && agent.hasPath && agent.velocity.magnitude > 0.1f)
            {
                if (Random.value < pauseChance)
                {
                    float pauseTime = Random.Range(minPauseTime, maxPauseTime);
                    StartCoroutine(PauseCoroutine(pauseTime));
                }
            }
        }
    }

    // �ִϸ����� �Ķ���� ����ȭ
    void SetWalkingAnimation(bool isWalking)
    {
        if (isWalking != prevIsWalking)
        {
            animator.SetBool("isWalking", isWalking);
            prevIsWalking = isWalking;
        }
    }
}
