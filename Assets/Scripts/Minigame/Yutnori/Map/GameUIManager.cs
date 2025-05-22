using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnCountText;

    public void UpdateTurn(int turnCount)
    {
        turnCountText.text = $"��: {turnCount}";
    }

    // ���� ������ ��ư � ���⿡ �߰�
}
