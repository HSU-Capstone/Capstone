using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    public GameObject instructionCanvas; // ���� ��� ����â Canvas

    public void OnStartGame()
    {
        instructionCanvas.SetActive(false);
        // ���⿡ �߰��� ������ �����ϴ� ������ ���� �� �־��!
    }
}
