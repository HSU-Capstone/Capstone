using UnityEngine;
using UnityEngine.UI; // �� Text ������Ʈ�� ���⼭ ��

public class TextDimmingByDistance : MonoBehaviour
{
    public Transform cameraTransform; // ���ΰ� ī�޶�
    public float maxDistance = 10f;   // �� �Ÿ� �̻��̸� ������ ��ο�
    private Text uiText;              // Unity UI�� �⺻ �ؽ�Ʈ

    void Start()
    {
        uiText = GetComponent<Text>();
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, cameraTransform.position);
        float brightness = Mathf.Clamp01(1f - dist / maxDistance); // �������� ����

        // ���� ���� �����ϸ鼭 ��� ����
        Color baseColor = Color.white;
        uiText.color = baseColor * brightness;
    }
}
