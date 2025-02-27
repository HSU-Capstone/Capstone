using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAfterDelay : MonoBehaviour
{
    private List<GameObject> prefabs = new List<GameObject>(); // ���� ������ ����Ʈ
    private List<GameObject> remainingPrefabs; // Ȱ��ȭ�� ������ ����Ʈ (���� ���ÿ�)
    public float startDelay = 3f; // ù ��° ������ Ȱ��ȭ ������ (3��)
    public float interval = 4f; // ������ Ȱ��ȭ ���� (4��)

    void Start()
    {
        // ���� ������Ʈ ã��
        foreach (Transform child in transform)
        {
            prefabs.Add(child.gameObject);
            child.gameObject.SetActive(false); // ó���� ��� ��Ȱ��ȭ
        }

        // Ȱ��ȭ�� ����Ʈ �ʱ�ȭ
        remainingPrefabs = new List<GameObject>(prefabs);

        // 3�� �� ù ��° ������ Ȱ��ȭ ����
        InvokeRepeating(nameof(ActivateRandomPrefab), startDelay, interval);
    }

    void ActivateRandomPrefab()
    {
        if (remainingPrefabs.Count == 0)
        {
            CancelInvoke(nameof(ActivateRandomPrefab)); // ��� �������� Ȱ��ȭ�Ǹ� ����
            return;
        }

        int randomIndex = Random.Range(0, remainingPrefabs.Count); // ���� �ε��� ����
        GameObject selectedPrefab = remainingPrefabs[randomIndex]; // �ش� ������ ��������
        remainingPrefabs.RemoveAt(randomIndex); // ����Ʈ���� ����
        selectedPrefab.SetActive(true); // ������ Ȱ��ȭ
    }
}
