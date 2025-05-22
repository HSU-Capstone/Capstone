using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFlowController : MonoBehaviour
{
    public GameObject uiParent; // "UI_exp" �� ������Ʈ
    public Button smogFlowButton; // "smog flow" ��ư
    public GameObject buttonPanel; // ��ư�� ���Ե� �г�

    private void Start()
    {
        // ���� �� �� ������Ʈ�� Ȱ��ȭ, �ڽ� ������Ʈ�� ��Ȱ��ȭ
        uiParent.SetActive(true);
        ToggleChildObjects(false);

        // ��ư�� �̺�Ʈ ������ ���
        smogFlowButton.onClick.AddListener(StartFlowSequence);
    }

    void ToggleChildObjects(bool isActive)
    {
        foreach (Transform child in uiParent.transform)
        {
            child.gameObject.SetActive(isActive);
        }
    }

    public void StartFlowSequence()
    {
        // ��ư�� ������ �� ��ư�� ���Ե� �г� ��Ȱ��ȭ
        if (buttonPanel != null)
        {
            buttonPanel.SetActive(false);
        }

        StartCoroutine(FlowSequence());
    }

    IEnumerator FlowSequence()
    {
        // �ڽ� ��ü���� ���ʴ�� Ȱ��ȭ�ǵ��� 2���� Ȱ��ȭ
        for (int i = 0; i < uiParent.transform.childCount; i += 2)
        {
            // ���� �ڽ� ��ü 2�� Ȱ��ȭ
            if (i < uiParent.transform.childCount)
                uiParent.transform.GetChild(i).gameObject.SetActive(true);
            if (i + 1 < uiParent.transform.childCount)
                uiParent.transform.GetChild(i + 1).gameObject.SetActive(true);

            yield return new WaitForSeconds(3f);

            // 2�� ��Ȱ��ȭ
            if (i < uiParent.transform.childCount)
                uiParent.transform.GetChild(i).gameObject.SetActive(false);
            if (i + 1 < uiParent.transform.childCount)
                uiParent.transform.GetChild(i + 1).gameObject.SetActive(false);
        }

        // ��� �ڽ� ��ü�� Ȱ��ȭ�Ǿ����� ��ư�� ���Ե� �г��� �ٽ� Ȱ��ȭ
        if (buttonPanel != null)
        {
            buttonPanel.SetActive(true);
        }
    }
}
