using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour

{
    public List<GameObject> uiElements; // UI ��Ҹ� ���� ����Ʈ
    public Button toggleButton;  // ��ư

    private int currentIndex = 0;  // ���� ǥ���� UI ����� �ε���

    void Start()
    {
        // ��ư Ŭ�� �� UI ���
        toggleButton.onClick.AddListener(ToggleVisibility);

        // ó������ ��� UI ��Ҹ� ����ϴ�.
        foreach (var ui in uiElements)
        {
            ui.SetActive(false);
        }
    }

    // UI ��Ҹ� ����ϴ� �Լ�
    void ToggleVisibility()
    {
        // ���� UI ��Ұ� Ȱ��ȭ �Ǿ� ������ ��Ȱ��ȭ
        uiElements[currentIndex].SetActive(false);

        // �ε����� �������� ���� UI ��ҷ� �Ѿ��, ����Ʈ�� ���������� ������ ó������ ���ư��ϴ�.
        currentIndex = (currentIndex + 1) % uiElements.Count;

        // ���ο� UI ��Ҹ� Ȱ��ȭ
        uiElements[currentIndex].SetActive(true);
    }
}
