using TMPro;
using UnityEngine;

public class ScrollLightDetect : MonoBehaviour
{
    public TextMeshPro textMeshPro;

    void Start()
    {
        textMeshPro.gameObject.SetActive(false);  // �⺻�� �� ���̰�
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flashlight"))  // ����� Tag ���� �ʼ�!
        {
            textMeshPro.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flashlight"))
        {
            textMeshPro.gameObject.SetActive(false);
        }
    }
}
