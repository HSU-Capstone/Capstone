using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // ������ ������Ʈ ������
    public Vector3 spawnPosition = new Vector3(25, 0, 17); // ���� ��ġ
    private int spawnCount = 0; // ��ư Ŭ�� Ƚ��(������ ������Ʈ ����)

    public void SpawnObject()
    {
        // ������Ʈ ����
        GameObject newObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
        newObject.name = "SpawnedObject_" + spawnCount; // ������ �̸� ����
        spawnCount++; // ������ ���� ����

        // ������ ������Ʈ�� �巡�� ��ũ��Ʈ �߰� (�ʿ��ϸ� ���)
        if (!newObject.GetComponent<ObjectDraggable>())
        {
            newObject.AddComponent<ObjectDraggable>();
        }
    }
}
