using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� �߰�
using UnityEngine.UI; // UI ��Ҹ� �ٷ�� ���� �ʿ�

public class SceneLoader : MonoBehaviour
{
    public Button sceneChangeButton; // ��ư ����

    void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ �߰�
        sceneChangeButton.onClick.AddListener(LoadGudleScene);
        sceneChangeButton.onClick.AddListener(LoadGoraeScene);
    }

    public void LoadGudleScene()
    {
        Debug.Log("��ư Ŭ��! 'Gudle' ������ �̵�");
        SceneManager.LoadScene("Gudle"); // �� �̵�
    }

    public void LoadGoraeScene()
    {
        Debug.Log("��ư Ŭ��! 'Gorae' ������ �̵�");
        SceneManager.LoadScene("Gorae"); // �� �̵�
    }
}



