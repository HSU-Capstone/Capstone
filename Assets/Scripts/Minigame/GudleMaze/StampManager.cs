using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StampManager : MonoBehaviour
{
    public GameObject stampPrefab;      // ���� �̹��� ������ (Image + Text)
    public Transform canvasTransform;   // ������ �ö� ĵ���� (Screen Space Canvas)
    public Vector2 stampPosition = new Vector2(0, 100); // ���� ��ġ (Screen Space ����)
    public Vector2 stampSize = new Vector2(300, 300);   // ���� ũ�� (Width, Height)
    public int textFontSize = 40;       // �ؽ�Ʈ ��Ʈ ũ�� (int)
    public Color textColor = Color.black; // �ؽ�Ʈ ����

    void Start()
    {
        StartCoroutine(PlayStampEffect());
    }

    IEnumerator PlayStampEffect()
    {
        GameObject stamp = Instantiate(stampPrefab, canvasTransform);
        RectTransform rt = stamp.GetComponent<RectTransform>();

        rt.anchorMin = rt.anchorMax = rt.pivot = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = stampPosition + new Vector2(0, 500f); // ������ ����
        rt.sizeDelta = stampSize;
        rt.localScale = Vector3.one * 3f;  // ũ�� ����

        Transform textTransform = stamp.transform.Find("Text");
        if (textTransform == null)
        {
            Debug.LogError("�ڽ� ������Ʈ�� 'Text'�� �����ϴ�. �̸� Ȯ�����ּ���.");
            yield break;
        }

        Text uiText = textTransform.GetComponent<Text>();
        if (uiText == null)
        {
            Debug.LogError("Text ������Ʈ�� Text ������Ʈ�� �����ϴ�.");
            yield break;
        }

        // �ؽ�Ʈ �ʱ⿣ ��Ȱ��ȭ
        uiText.enabled = false;

        uiText.fontSize = textFontSize;
        uiText.color = textColor;

        float duration = 0.5f;
        float time = 0f;

        Vector2 startPos = rt.anchoredPosition;
        Vector2 endPos = stampPosition;

        Vector3 startScale = rt.localScale;
        Vector3 endScale = Vector3.one;

        while (time < duration)
        {
            float t = time / duration;
            rt.anchoredPosition = Vector2.Lerp(startPos, endPos, EaseOutCubic(t));
            rt.localScale = Vector3.Lerp(startScale, endScale, t);
            time += Time.deltaTime;
            yield return null;
        }

        rt.anchoredPosition = endPos;
        rt.localScale = endScale;

        // ������ ������ �Ŀ� �ؽ�Ʈ Ȱ��ȭ
        uiText.enabled = true;
    }

    float EaseOutCubic(float t)
    {
        return 1f - Mathf.Pow(1f - t, 3);
    }
}
