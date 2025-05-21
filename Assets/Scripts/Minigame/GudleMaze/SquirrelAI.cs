using UnityEngine;
using UnityEngine.AI;

public class SquirrelAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform squirrelHandTransform;
    public GameObject acornPrefab;
    public Animator animator;
    public Transform exitPoint;  // ���� ��ġ (�ɾ ���� �� �ʿ�)

    private int hitCount = 0;
    private bool isDefeated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Acorn") && !isDefeated)  // ���丮�� �¾Ҵ��� üũ
        {
            hitCount++;

            if (hitCount >= 3)
            {
                DefeatSquirrel();
            }
        }
    }

    void DefeatSquirrel()
    {
        isDefeated = true;
        agent.isStopped = true;  // AI ���߱�

        // ���丮�� �տ� ��
        GameObject acorn = Instantiate(acornPrefab, squirrelHandTransform.position, Quaternion.identity);
        acorn.transform.SetParent(squirrelHandTransform);
        acorn.transform.localPosition = Vector3.zero;
        acorn.transform.localRotation = Quaternion.identity;

        // ǥ�� ���� (�ִϸ��̼� Ʈ����)
        animator.SetTrigger("Surprised");

        // ������ ���� (�ɾ ������ or �������)
        bool walkAway = Random.value > 0.5f; // 50% Ȯ���� ����
        if (walkAway)
        {
            agent.SetDestination(exitPoint.position);
        }
        else
        {
            Destroy(gameObject, 2f);
        }
    }
}
