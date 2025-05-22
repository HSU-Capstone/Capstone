using UnityEngine;

public class ResetObjectPosition : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        // �ʱ� ��ġ ����
        initialPosition = transform.position;
    }

    // �ʱ�ȭ �Լ�
    public void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
