using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public QuizDatabase quizDatabase;
    public QuizPanelUI quizPanelUI;
    private BuffManager buffManager;

    public void Start()
    {
        buffManager = GetComponent<BuffManager>();
    }

    // �ݹ�� PlayerState�� ��� �޵��� �ñ״�ó ����
    public void ShowRandomQuiz(System.Action<bool> onQuizEnd, PlayerState playerState = null)
    {
        var quizzes = quizDatabase.quizzes;
        if (quizzes.Count == 0) return;

        var quiz = quizzes[Random.Range(0, quizzes.Count)];
        quizPanelUI.Show(quiz, (isCorrect) => OnQuizResult(isCorrect, playerState, onQuizEnd));
    }

    // ���� ��� ó�� �� ���� ����
    private void OnQuizResult(bool isCorrect, PlayerState playerState, System.Action<bool> onQuizEnd)
    {
        if (isCorrect)
        {
            Buff selectedBuff = buffManager.GetRandomBuff();
            if (playerState != null)
            {
                ApplyBuff(selectedBuff, playerState);
            }
            Debug.Log($"����! '{selectedBuff.description}' ������ ����Ǿ����ϴ�.");
        }
        else
        {
            Debug.Log("����! ������ ������� �ʽ��ϴ�.");
        }
        // �ܺ� �ݹ� ȣ�� (���� �帧 ���� ��)
        onQuizEnd?.Invoke(isCorrect);
    }

    // ���� ȿ�� ����
    private void ApplyBuff(Buff buff, PlayerState player)
    {
        switch (buff.type)
        {
            case BuffType.ExtraThrow:
                player.hasExtraThrow = true;
                break;
            case BuffType.StackPiece:
                player.canStackPiece = true;
                break;
            case BuffType.NextMovePlus:
                player.nextMovePlus = 1;
                break;
            case BuffType.NextBuffAutoSuccess:
                player.nextBuffAutoSuccess = true;
                break;
        }
        // �ʿ��ϴٸ� UI�� ���� ������/���� ǥ��
    }
}
