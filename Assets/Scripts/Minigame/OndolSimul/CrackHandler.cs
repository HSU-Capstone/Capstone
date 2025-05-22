using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackHandler : MonoBehaviour
{
    public GameObject crackedPrefab; // "cracked" ������
    public GameObject dugPrefab; // "dug" ������

    private int hitCount = 0; // �浹 Ƚ��

    private void OnCollisionEnter(Collision collision)
    {
        // ��ġ�� �浹���� ��
        if (collision.gameObject.CompareTag("hammer"))
        {
            hitCount++; // �浹 Ƚ�� ����

            // "cracked" ������Ʈ ����
            Instantiate(crackedPrefab, transform.position, transform.rotation);

            // 4ȸ �浹 �� ���� "crack" ������Ʈ�� ��Ȱ��ȭ�ϰ� "dug" ������Ʈ ����
            if (hitCount >= 4)
            {
                GameObject dugObject = Instantiate(dugPrefab, transform.position, transform.rotation);
                dugObject.tag = "dug"; // �±� ����
                gameObject.SetActive(false); // crack ������Ʈ ��Ȱ��ȭ
            }
        }
    }
}
