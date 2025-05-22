using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SquirrelBehavior : MonoBehaviour
{
    public GameObject balloonUI;  // ��ǳ�� UI ������Ʈ
    public Text balloonText;      // ��ǳ�� �ؽ�Ʈ
    private NavMeshAgent agent;    // ? �ٶ��� �̵��� ������Ʈ
    private Transform player;     // �÷��̾�(ī�޶�)

    private bool hasReacted = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // ? NavMeshAgent ������Ʈ ��������
        player = Camera.main.transform;        // ī�޶� Ʈ������ ��������
        balloonUI.SetActive(false);             // ��ǳ�� ��Ȱ��ȭ
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !hasReacted)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    // ? �ٶ��� ���߰� �ϱ�
                    if (agent != null)
                    {
                        agent.isStopped = true;
                    }

                    // ? �ٶ��㸦 �÷��̾� ������ �ٶ󺸰�
                    Vector3 lookPos = player.position - transform.position;
                    lookPos.y = 0;
                    transform.rotation = Quaternion.LookRotation(lookPos);

                    // ? ��ǳ�� �����ֱ�
                    balloonText.text = "�ȳ�! �� �ٶ����!";
                    balloonUI.SetActive(true);

                    // ��ǳ�� ��ġ �ٶ��� �Ӹ� ��
                    balloonUI.transform.position = transform.position + Vector3.up * 2f;

                    // ? ��ǳ���� ī�޶� �ٶ󺸰�
                    balloonUI.transform.LookAt(player);

                    hasReacted = true;
                }
            }
        }
    }
}
