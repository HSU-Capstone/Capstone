using UnityEngine;

public class SquirrelTrader : MonoBehaviour
{
    public GameObject rewardItemPrefab; // ���� ������
    public Transform rewardSpawnPoint;  // ������ ������ ��ġ
    public int requiredAcorns = 1;      // �ʿ��� ���丮 ��

    private bool playerInRange = false;

    void Update()
    {
        // �÷��̾ ��ó�� �ְ� FŰ ������ �ŷ�
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            TryTrade();
        }
    }

    void TryTrade()
    {
        // ���丮�� ���� �÷��̾ �ִ��� Ȯ��
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();
        if (inventory != null && inventory.acornCount >= requiredAcorns)
        {
            // ���丮 ����
            inventory.acornCount -= requiredAcorns;

            // ���� ������ ����
            Instantiate(rewardItemPrefab, rewardSpawnPoint.position, Quaternion.identity);
            Debug.Log("�ŷ� ����! ���� ���޵�.");
        }
        else
        {
            Debug.Log("���丮�� �����ؿ�!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
