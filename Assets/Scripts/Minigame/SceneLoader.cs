using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Button sceneChangeButton; // ��ư ����
    public string sceneName; // �� �̸� ����

    void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ �߰�
        sceneChangeButton.onClick.AddListener(LoadScene);
    }

    // ��ư Ŭ�� �� �� ��ȯ
    public void LoadScene()
    {
        Debug.Log("��ư Ŭ��! " + sceneName + " ������ �̵�");
        SceneManager.LoadScene(sceneName); // �� �̵�
    }
}

