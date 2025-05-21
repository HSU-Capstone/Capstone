using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dotorri : MonoBehaviour
{
    // ���丮�� �ٶ���� �浹�� �� ȣ��
    private void OnTriggerEnter(Collider other)
    {
        // �ٶ���� �浹���� ���� ���丮 ȸ��
        if (other.CompareTag("Squirrel")) // �ٶ��� ������Ʈ�� �±װ� "Squirrel"�� ���
        {
            Collect();
        }
    }

    // ���丮 ȸ���ϴ� �޼���
    void Collect()
    {
        // ���丮�� ȸ���ϰ� ���ֱ�
        Destroy(gameObject); // ���丮 ������Ʈ�� ����
    }
}
