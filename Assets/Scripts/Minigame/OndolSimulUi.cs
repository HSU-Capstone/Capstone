using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OndolSimulUi : MonoBehaviour
{
    private Transform[] children;
    private int currentIndex = -1; // �ʱⰪ�� -1�� ������ ù ��° Ŭ�� �� ù �ڽ��� Ȱ��ȭ�ǵ��� ��

    void Start()
    {
        // �ڽ� ��ü���� �迭�� ����
        children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
            children[i].gameObject.SetActive(false); // ��� ��Ȱ��ȭ
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
        {
            ActivateNextChild();
        }
    }

    void ActivateNextChild()
    {
        // ���� Ȱ��ȭ�� ��ü ��Ȱ��ȭ
        if (currentIndex >= 0 && currentIndex < children.Length)
        {
            children[currentIndex].gameObject.SetActive(false);
        }

        // ���� ��ü Ȱ��ȭ (1������ ��ȯ)
        currentIndex = (currentIndex + 1) % children.Length;
        children[currentIndex].gameObject.SetActive(true);
    }
}
