using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // ���󰡾� �� ���
    public Transform targetTr;
    private Transform camTr;

    // ���󰡾� �� ������κ��� �󸶳� ������ ������
    [Range(2.0f, 20.0f)] // ���� �Է� ���� ����, �ν����� �信 �����̵��
    public float distance = 3.5f;

    // Y������ �̵��� ����, ī�޶� ����
    [Range(0.0f, 10.0f)]
    public float height = 1.5f;

    //ī�޶� LookAt�� offset ��
    public float targetOffset = 1.7f;

    // ī�޶� ���� �ӵ�
    public float damping = 0.1f;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // Main Camera �ڽ��� Transform ������Ʈ ����
        camTr = GetComponent<Transform>();
    }

    // Update���� �̵� ���� �� �� �����ϱ� ����
    void LateUpdate()
    {
        // �����ؾ� �� ����� �������� distance��ŭ, ���� height��ŭ �̵�
        // Ÿ���� ��ġ + (Ÿ���� ���� ���� * ������ �Ÿ�) + (y�� ���� * ����)
        Vector3 pos = targetTr.position
            + (Vector3.back * distance)
            + (Vector3.up * height);

        // ���� ���� ���� ���, ��ġ �ε巴�� �ٲٱ�
        //camTr.position = Vector3.Slerp(camTr.position, pos, Time.deltaTime*damping);

        // SmoothDamp�� ��ġ ����
        camTr.position = Vector3.SmoothDamp(camTr.position, pos, ref velocity, damping);

        //// �ǹ� ��ǥ ���� ȸ��
        //camTr.LookAt(targetTr.position + (targetTr.up * targetOffset));
    }
}
