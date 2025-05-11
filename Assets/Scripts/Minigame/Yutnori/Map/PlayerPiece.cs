using System.Collections;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    [SerializeField] private YutnoriGameManager gameManager;

    [SerializeField] private Highlighter highlighter;

    // ���� ���� ��ġ (�ʿ� ���� ������ null)
    public PointOfInterest currentNode { get; private set; }

    private Coroutine blinkCoroutine;

    void Start()
    {
    }

    void OnMouseDown()
    {
        if (gameManager.stage == GameStage.Move)
            gameManager.SelectPiece(this);
    }

    public void Highlight(bool highlight)
    {
        if (highlight)
            highlighter.StartBlink();
        else
            highlighter.StopBlink();
    }

    public void MoveTo(PointOfInterest destination)
    {
        // �� �̵� �ִϸ��̼� (������ ����)
        StartCoroutine(MoveAnimation(destination));
    }

    private IEnumerator MoveAnimation(PointOfInterest destination)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = destination.transform.position + Vector3.up * 0.5f; // ��� ���� ��ġ
        float duration = 0.5f;
        float time = 0;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        currentNode = destination;

        // �̵� �Ϸ� �� ���� �ܰ��
        if (gameManager.canThrowAgain)
            gameManager.setGameStage(GameStage.Throw); // �� �� �� ������
        else
            gameManager.setGameStage(GameStage.Interact); // ���� �÷��̾� ��
    }
}
