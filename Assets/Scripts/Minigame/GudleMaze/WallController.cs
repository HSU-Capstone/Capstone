using UnityEngine;

public class WallController : MonoBehaviour
{
    public GameObject correctWall;  // ���� ��
    public GameObject wrongWall;    // ���� ��
    public GameObject quizCanvas;   // ���� UI�� ���Ե� Canvas

    // ���� ��ư Ŭ�� �� ����
    public void RemoveCorrectWall()
    {
        if (correctWall != null)
            correctWall.SetActive(false);  // ���� �� ����

        DisableCanvas();  // Canvas ��Ȱ��ȭ
    }

    // ���� ��ư Ŭ�� �� ����
    public void RemoveWrongWall()
    {
        if (wrongWall != null)
            wrongWall.SetActive(false);    // ���� �� ����

        DisableCanvas();  // Canvas ��Ȱ��ȭ
    }

    // Canvas�� ��Ȱ��ȭ�ϴ� ���� �Լ�
    private void DisableCanvas()
    {
        if (quizCanvas != null)
            quizCanvas.SetActive(false);
    }
}
