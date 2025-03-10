using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OndolSimul : MonoBehaviour
{
    public GameObject parentObject; // �� ������Ʈ A
    public float activationInterval = 1f; // Ȱ��ȭ ���� (��)
    public Button ondolSimulationButton; // "Ondol Simulation" ��ư

    void Start()
    {
        if (parentObject == null)
        {
            Debug.LogError("Parent Object is not assigned!");
            return;
        }

        // �� ������Ʈ A Ȱ��ȭ
        parentObject.SetActive(true);

        // ��ư�� �̺�Ʈ ������ ���
        if (ondolSimulationButton != null)
        {
            ondolSimulationButton.onClick.AddListener(StartOndolSimulation);
        }
        else
        {
            Debug.LogError("Ondol Simulation Button is not assigned!");
        }
    }

    public void StartOndolSimulation()
    {
        StartCoroutine(ActivateChildrenSequentially());
    }

    IEnumerator ActivateChildrenSequentially()
    {
        // �ڽ� ������Ʈ�� �������� (��Ȱ��ȭ�� �ڽĵ� ����)
        Transform[] children = new Transform[parentObject.transform.childCount];
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            children[i] = parentObject.transform.GetChild(i);
        }

        // �ڽ� ������Ʈ ��ü ��Ȱ��ȭ
        foreach (var child in children)
        {
            if (child != null)
            {
                child.gameObject.SetActive(false);
            }
        }

        // 1�� �������� �ڽ� ������Ʈ ������ Ȱ��ȭ
        for (int i = 0; i < children.Length; i++)
        {
            yield return new WaitForSeconds(activationInterval);

            if (children[i] != null)
            {
                children[i].gameObject.SetActive(true);
            }
        }
    }
}
