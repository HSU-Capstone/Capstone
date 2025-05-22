using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Squirrel : MonoBehaviour
{

    public int squirrelCount = 0;
    public Text squirrelText;
    public Slider heaterProgressSlider;
    public float progressSpeed = 0.1f;

    void Start()
    {
        // �ʱ� ����
        squirrelText.text = "�ٶ��� ��: " + squirrelCount;
    }

    void Update()
    {
        // �µ� ���� ����
        if (heaterProgressSlider.value < 1)
        {
            heaterProgressSlider.value += progressSpeed * Time.deltaTime;
        }

        // �ٸ� ���� ���� �߰�
    }

    // �ٶ����� �ڿ��� ���İ��� ������ �߰�
    public void AddSquirrel()
    {
        squirrelCount++;
        squirrelText.text = "�ٶ��� ��: " + squirrelCount;
    }
}
