using UnityEngine;

public class AcornHandler : MonoBehaviour
{
    public GameObject hand;               // HAND ������Ʈ
    public GameObject dotoriPrefab;       // DOTORI ������
    public AudioClip soundEffect;         // ȿ����
    private Animator animator;            // A ��ü�� �ִϸ�����
    private int collisionCount = 0;       // DOTORI���� �浹 Ƚ��

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // "DOTORI" �±׸� ���� ��ü�� �浹 ��
        if (collision.gameObject.CompareTag("DOTORI"))
        {
            collisionCount++;  // �浹 Ƚ�� ����

            if (collisionCount == 5)
            {
                // Ƚ���� 5���� �� �ִϸ��̼�2 �ο� + ȿ����
                animator.SetTrigger("Animation2");
                AudioSource.PlayClipAtPoint(soundEffect, transform.position);
            }
            else
            {
                // �ִϸ��̼�1 �ο� + DOTORI�� HAND�� ���̱�
                animator.SetTrigger("Animation1");
                AttachDotoriToHand(collision.gameObject);

                // 3�� �Ŀ� �� ���·� ���ư��� �������� ���ƴٴϱ�
                Invoke("ReturnToOriginalState", 3f);
            }
        }
    }

    // DOTORI�� HAND�� ������Ű�� �Լ�
    void AttachDotoriToHand(GameObject dotori)
    {
        dotori.transform.SetParent(hand.transform); // DOTORI�� HAND�� ����
        dotori.transform.localPosition = Vector3.zero; // ��ġ�� HAND�� �߾����� ����
        dotori.transform.localRotation = Quaternion.identity; // ȸ�� �ʱ�ȭ
    }

    // 3�� �� �� ���·� ���ư���, A ��ü�� �������� ���ƴٴϰ� �ϴ� �Լ�
    void ReturnToOriginalState()
    {
        // DOTORI�� HAND���� ������
        GameObject dotori = hand.transform.GetChild(0).gameObject;
        if (dotori != null)
        {
            dotori.transform.SetParent(null); // DOTORI�� �θ𿡼� �и�
            Destroy(dotori);  // DOTORI ��ü ���� (Ȥ�� ��Ȱ��ȭ)
        }

        // A ��ü�� ���� �̵� �ڵ� ȣ�� (�̹� ������ ���� �̵� �ڵ�)
        // RandomMovement(); // ���� �̵� ���� �κ� ȣ��
    }
}

