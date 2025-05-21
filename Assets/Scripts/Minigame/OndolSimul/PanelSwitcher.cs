using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    public GameObject panelE; // �г� E
    public GameObject panelG; // �г� G
    public List<GameObject> panelEChildren; // �г� E�� �ڽ� ��ü��

    private bool isPanelEActive = false; // �г� E ���� �Ϸ� ���� Ȯ��

    // Start is called before the first frame update
    void Start()
    {
        // �г� E �ڽ� ��ü�� ����Ʈ�� �߰�
        panelEChildren = new List<GameObject>();
        foreach (Transform child in panelE.transform)
        {
            panelEChildren.Add(child.gameObject);
        }

        // �г� E�� ó������ ��Ȱ��ȭ
        panelE.SetActive(false);
        panelG.SetActive(true); // �г� G�� Ȱ��ȭ
    }

    // ��ư "f" Ŭ�� �� �г� E�� �̵�
    public void OnButtonClick()
    {
        panelG.SetActive(false); // �г� G ��Ȱ��ȭ
        panelE.SetActive(true); // �г� E Ȱ��ȭ
        isPanelEActive = true;
        ActivatePanelEChildren(); // �г� E �ڽ� ��ü���� Ȱ��ȭ
    }

    // �г� E�� �ڽ� ��ü���� Ȱ��ȭ
    private void ActivatePanelEChildren()
    {
        foreach (var child in panelEChildren)
        {
            child.SetActive(true);
        }
    }

    // �г� E�� ��� �ڽ� ��ü�� Ȱ��ȭ�Ǿ���, �� �� ��Ȱ��ȭ�� �Ǹ� ȣ��
    public void CompletePanelE()
    {
        // �ڽ� ��ü���� ��Ȱ��ȭ
        foreach (var child in panelEChildren)
        {
            child.SetActive(false);
        }

        // �г� E ��Ȱ��ȭ �� �г� G�� ���ƿ�
        panelE.SetActive(false);
        panelG.SetActive(true);
    }
}

