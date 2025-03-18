using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // �� ��ȯ�� ���� �߰�

public class SliderTimer : MonoBehaviour
{
    public Slider timerSlider;  // UI �����̴�
    public float gameTime = 30f; // 30�� Ÿ�̸�

    private float elapsedTime = 0f; // ��� �ð�

    void Update()
    {
        if (elapsedTime < gameTime)
        {
            elapsedTime += Time.deltaTime;  // �ð� ����
            timerSlider.value = elapsedTime; // �����̴� �� �ݿ�
        }
        else
        {
            EndGame();  // 30�ʰ� ������ �� ����
        }
    }

    void EndGame()
    {
        Debug.Log("���� ����! Gudle_End ������ �̵�");
        SceneManager.LoadScene("Gudle_End"); // �� ����
    }
}

