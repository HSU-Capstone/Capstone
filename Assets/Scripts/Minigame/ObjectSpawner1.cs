using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner1 : MonoBehaviour
{
    public GameObject objectPrefab; // ������ ������Ʈ ������
    public Vector3 spawnPosition = new Vector3(25, 0, 17); // ���� ��ġ
    public Vector3 spawnRotation = new Vector3(216.556f, 175.347f, 51.504f); // ���� ȸ����
    private int spawnCount = 0; // ��ư Ŭ�� Ƚ��(������ ������Ʈ ����)
    private GameObject spawnedObject; // ������ ������Ʈ ����

    public void SpawnObject()
    {
        spawnCount++; // ��ư ���� ������ ����

        if (spawnedObject == null) // ó�� ������ ���
        {
            spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.Euler(spawnRotation));
            spawnedObject.name = "SpawnedObject_" + spawnCount;

            // �巡�� ��ũ��Ʈ �߰�
            if (!spawnedObject.GetComponent<ObjectDraggable>())
            {
                spawnedObject.AddComponent<ObjectDraggable>();
            }
        }

        // ��ư�� ¦�� �� ������ ��Ȱ��ȭ, Ȧ�� �� ������ Ȱ��ȭ
        if (spawnedObject != null)
        {
            spawnedObject.SetActive(spawnCount % 2 != 0);
        }
    }
}
