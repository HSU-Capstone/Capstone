using UnityEngine;

public class ToggleController : MonoBehaviour
{
    public GameObject targetCanvas;  // Ȱ��ȭ/��Ȱ��ȭ�� Canvas
    private int pressCount = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            pressCount++;

            // Ȧ���� �� Ȱ��ȭ, ¦���� �� ��Ȱ��ȭ
            if (pressCount % 2 == 1)
            {
                targetCanvas.SetActive(true);
            }
            else
            {
                targetCanvas.SetActive(false);
            }
        }
    }
}
