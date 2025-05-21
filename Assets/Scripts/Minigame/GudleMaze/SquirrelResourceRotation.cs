using UnityEngine;

public class SquirrelResourceRotation : MonoBehaviour
{
    public Transform squirrelHead; // �ٶ��� �Ӹ� ��ġ
    public float heightOffset = 2f; // �Ӹ� ���� ��� ����

    public float rotationSpeed = 20f; // ȸ�� �ӵ�

    void Start()
    {
        // �ڿ��� �ٶ��� �Ӹ� ��ġ ���� ��ġ
        if (squirrelHead != null)
        {
            transform.position = squirrelHead.position + new Vector3(0, heightOffset, 0);
        }
    }

    void Update()
    {
        // �ڿ��� Y���� �������� ��� ȸ��
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
