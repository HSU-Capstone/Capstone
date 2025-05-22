using TMPro;
using UnityEngine;

public class ScrollTextSwitcher : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public string ancientText = "Ondol is warm";  // ���� ����
    public string koreanText = "�µ��� �����ؿ�";  // �ؼ��� �ؽ�Ʈ

    private bool isTranslated = false;

    void Start()
    {
        textMeshPro.text = ancientText;
    }

    void OnMouseDown()
    {
        isTranslated = !isTranslated;
        textMeshPro.text = isTranslated ? koreanText : ancientText;
    }
}
