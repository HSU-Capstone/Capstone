using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectDisabler : MonoBehaviour
{
    public Button disableButton; // ��Ȱ��ȭ�� ��ư
    public GameObject targetObject; // ��Ȱ��ȭ�� ������Ʈ

    void Start()
    {
        if (disableButton != null && targetObject != null)
        {
            // ��ư Ŭ�� �� DisableObject �޼��� ȣ��
            disableButton.onClick.AddListener(DisableObject);
        }
        else
        {
            Debug.LogError("Button or Target Object is not assigned!");
        }
    }

    void DisableObject()
    {
        targetObject.SetActive(false); // ������Ʈ ��Ȱ��ȭ
    }
}

