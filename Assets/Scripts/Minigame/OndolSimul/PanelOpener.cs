using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{
    public GameObject panelB; // �г� B
    public GameObject panelC; // �г� C

    // ��ư�� ������ �� �г� B�� ���� �г� C�� �ݱ�
    public void OpenPanel()
    {
        if (panelB != null)
        {
            panelB.SetActive(true); // �г� B Ȱ��ȭ
        }

        if (panelC != null)
        {
            panelC.SetActive(false); // �г� C ��Ȱ��ȭ
        }
    }
}
