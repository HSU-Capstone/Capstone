using UnityEngine;

public class DragObject : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    // ���콺 ��ư�� ���� ��
    void OnMouseDown()
    {
        // ��ü�� ���� ��ǥ�� ȭ�� ��ǥ ���� ���̸� ���
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    // ���콺�� �巡���� ��
    void OnMouseDrag()
    {
        // ���콺 ��ġ�� �������� ��ü�� ������
        Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 currentWorldPoint = Camera.main.ScreenToWorldPoint(currentScreenPoint) + offset;
        transform.position = currentWorldPoint;
    }
}
