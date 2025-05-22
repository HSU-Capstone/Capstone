using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float lookSpeedX = 3.0f;  // �¿� ȸ�� �ӵ�
    public float lookSpeedY = 3.0f;  // ���� ȸ�� �ӵ�
    public float lookSpeedZ = 3.0f;
    public Transform playerBody;     // �÷��̾��� ��ü(ī�޶�� ����� ��ü)

    private float currentXRotation = 0.0f;  // ���� ȸ�� ����
    private float rotationY = 0.0f;         // �¿� ȸ�� ����

    void Update()
    {
        // ���콺 �̵��� ���� �þ� ȸ��
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;  // �¿� ���콺 �̵�
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;  // ���� ���콺 �̵�
        float mouseZ = Input.GetAxis("Mouse Z") * lookSpeedZ;

        // ���� ȸ�� ����
        currentXRotation -= mouseY;
        currentXRotation = Mathf.Clamp(currentXRotation, -90f, 90f);  // -90�� ~ 90�� ���� ������ ȸ��

        // �¿� ȸ�� (���콺 ���� ��ư�� ������ ��)
        rotationY += mouseX;

        // ī�޶��� ���� ȸ��
        transform.localRotation = Quaternion.Euler(currentXRotation, 0f, 0f);

        // �÷��̾��� ��ü(�ַ� ĳ����) �¿� ȸ��
        playerBody.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}
