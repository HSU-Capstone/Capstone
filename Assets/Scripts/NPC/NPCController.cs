using UnityEngine;
using System;

public class NPCController : MonoBehaviour
{
    private Animator animator;
    private Transform playerTransform;
    private PlayerController playerController;

    // UI �Ŵ����� ������ �̺�Ʈ
    public event Action<NPCController> OnInteractionStarted;
    public event Action OnInteractionCanceled;
    public event Action OnDialogueStarted;
    public event Action OnDialogueEnded;

    void Start()
    {
        animator = GetComponent<Animator>();

        // �÷��̾ ������ �� �ڵ����� ã�Ƽ� ����
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {        
            playerController =player.GetComponent<PlayerController>();
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        // Ȥ�� �÷��̾ ���� ���� �ȵǾ��� ���� ����ؼ� �ٽ� �õ�
        if (playerTransform == null || playerController == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerController = player.GetComponent<PlayerController>();
                playerTransform = player.transform;
            }
            else
            {
                return; // �÷��̾� ������ ���� �� ��
            }
        }
    }

        // FŰ�� NPC ��ȣ�ۿ� ����. Playerinteraction.cs���� ȣ��
        public void InteractWithPlayer()
    {
        // �ִϸ��̼� Any State > Idle
        ForceIdle();

        // NPC�� �÷��̾� �ٶ󺸱�
        Vector3 lookPos = playerTransform.position;
        lookPos.y = transform.position.y; // Y�� ����(ȸ����)
        transform.LookAt(lookPos);

        // UI ǥ�� �̺�Ʈ �߼�. UI �Ŵ����� NPCController�� �̺�Ʈ�� ��������
        playerController.setIsTalking(true);
        OnInteractionStarted?.Invoke(this);
    }

    // ��ȭ ���� ��ư��. �ƿ� ��ȭ ui �� ��
    public void CancelInteraction()
    {
        playerController.setIsTalking(false); // �̵� �� �ϰ� �ϴ� bool
        OnInteractionCanceled?.Invoke();
    }

    // ��ȭ ��ư��
    public void StartDialogue()
    {
        animator.SetBool("isTalking", true); // �ִϸ��̼� ���� bool
        OnDialogueStarted?.Invoke();
    }

    // FŰ(��ȭ �� ����) �Է� ��. ��ȭ �ڸ� ���� �ٽ� �������� ���ư���
    public void EndDialogue()
    {
        animator.SetBool("isTalking", false);
        OnDialogueEnded?.Invoke();
        OnInteractionStarted?.Invoke(this); // �ٽ� ��ȭ ������ ��
    }

    // �ִϸ��̼� Idle�� ���� ��ȯ
    public void ForceIdle()
    {
        animator.SetTrigger("forceIdle");
    }
}
