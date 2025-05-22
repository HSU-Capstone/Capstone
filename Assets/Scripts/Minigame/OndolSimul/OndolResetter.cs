using UnityEngine;
using System.Collections.Generic;

public class OndolResetter : MonoBehaviour
{
    public GameObject parentObject;

    private class TransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    private Dictionary<Transform, TransformData> initialStates = new Dictionary<Transform, TransformData>();

    void Start()
    {
        if (parentObject == null)
        {
            Debug.LogError("parentObject�� �������� �ʾҽ��ϴ�.");
            return;
        }

        foreach (Transform child in parentObject.transform)
        {
            TransformData data = new TransformData
            {
                position = child.position,
                rotation = child.rotation,
                scale = child.localScale
            };
            initialStates[child] = data;
        }
    }

    void OnMouseDown() // �� ��ũ��Ʈ�� ���� ������Ʈ(Cube)�� Ŭ���ϸ� �����
    {
        foreach (var pair in initialStates)
        {
            pair.Key.position = pair.Value.position;
            pair.Key.rotation = pair.Value.rotation;
            pair.Key.localScale = pair.Value.scale;
        }

        Debug.Log("�ڽ� ������Ʈ���� ��ġ/ȸ��/�������� �ʱ� ���·� �����߽��ϴ�.");
    }
}
