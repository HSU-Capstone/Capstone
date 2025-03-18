using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowMouse : MonoBehaviour
{
    private bool followMouse = false;

    // �ܺο��� FollowMouse�� Ȱ��ȭ/��Ȱ��ȭ�� �� �ְ� ����
    public void SetFollowMouse(bool follow)
    {
        followMouse = follow;
    }

    void Update()
    {
        if (followMouse)
        {
            // UI ��Ұ� �ƴ� ���� ������Ʈ�� ���콺�� ���󰡰� ó��
            if (GetComponent<RectTransform>() == null) // UI ��Ұ� �ƴ� ��쿡�� ó��
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0; // z���� 0���� ���� (2D ���ӿ��� Z���� ����)
                transform.position = mousePos;
            }
        }
    }
}
