using UnityEngine;
using UnityEngine.UI;

public class OndolTemperature : MonoBehaviour
{
    public GameObject effectPrefab; // �µ� ��ȭ ȿ�� ������
    public GameObject speechBubble; // ��ǳ�� UI
    public Text temperatureText;    // �µ� ǥ�� �ؽ�Ʈ
    public Button tempButton;       // �µ� Ȯ�� ��ư

    private GameObject currentEffect;

    void Start()
    {
        // ��ư�� �̺�Ʈ ����
        tempButton.onClick.AddListener(ShowTemperatureEffect);
        speechBubble.SetActive(false); // �ʱ⿡�� ��ǳ�� ��Ȱ��ȭ
    }

    void ShowTemperatureEffect()
    {
        if (currentEffect == null)
        {
            // �µ� ���� ȿ�� ����
            currentEffect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            // �µ� ���� ������Ʈ
            speechBubble.SetActive(true);
            temperatureText.text = "�µ�: 45��C";

            // ���� �ð� �� ȿ�� ����
            Invoke("HideEffect", 5f);
        }
    }

    void HideEffect()
    {
        Destroy(currentEffect);
        speechBubble.SetActive(false);
    }
}
