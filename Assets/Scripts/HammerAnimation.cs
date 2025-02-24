using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HammerAnimation : MonoBehaviour
{
    private Vector3 originalPosition; // �ʱ� ��ġ
    private Quaternion originalRotation; // �ʱ� ȸ����
    public float hammerDownAngle = 30f; // ����ĥ ���� (Z��)
    public float animationSpeed = 0.1f; // �ִϸ��̼� �ӵ�

    void Start()
    {
        // ���� ��ġ�� ȸ���� ����
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // ���콺 ������ ��ư Ŭ���ϸ� �ִϸ��̼� ����
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(SwingHammer());
        }
    }

    IEnumerator SwingHammer()
    {
        // ����ġ�� ȸ���� ���� (���� ȸ���� ����)
        Quaternion downRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 70f);

        // ������� (������)
        float elapsedTime = 0;
        while (elapsedTime < animationSpeed)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, downRotation, elapsedTime / animationSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = downRotation; // ���� ��ġ ����

        // ��� ��� (0.2��)
        yield return new WaitForSeconds(0.2f);

        // ���� ��ġ�� ���� (�ε巴��)
        elapsedTime = 0;
        while (elapsedTime < animationSpeed)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, elapsedTime / animationSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = originalRotation; // ���� ��ġ ����
    }
}
