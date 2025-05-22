using UnityEngine;
using TMPro;

public class ScrollTranslation : MonoBehaviour
{
    public TextMeshPro textAncient;   // ���� �ؽ�Ʈ
    public TextMeshPro textKorean;   // �ѱ� �ؽ�Ʈ

    private void Start()
    {
        textAncient.gameObject.SetActive(true);
        textKorean.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Flashlight"))
        {
            textAncient.gameObject.SetActive(false);
            textKorean.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Flashlight"))
        {
            textAncient.gameObject.SetActive(true);
            textKorean.gameObject.SetActive(false);
        }
    }
}
