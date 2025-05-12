using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HanokPart
{
    Roof,   // ����
    Column,   // ��
    Wall, // ���
    Floor   // �ٴ�
}

public class PlayerPiece : MonoBehaviour
{
    // ���� �ٲ�� highlight �� �Ǳ⵵ �� 
    [SerializeField] private YutnoriGameManager gameManager;

    [SerializeField] private Highlighter highlighter;

    [SerializeField] private HanokPart hanokPart;

    [SerializeField] private MapGenerator mapGenerator;

    private bool shortcutUsed = false; // �̹� ������ ��������� üũ��, �ߺ� ������

    // ���� ���� ��ġ (�ʿ� ���� ������ null)
    public PointOfInterest currentNode { get; private set; }

    private Coroutine blinkCoroutine;

    void Start()
    {
        SetCurrentNode(mapGenerator.getStartingPoint());
        //Debug.Log(currentNode.Type);
    }
    public void SetCurrentNode(PointOfInterest node)
    {
        currentNode = node;
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
        StartCoroutine(MoveByPath(destination));
    }

    private IEnumerator MoveByPath(PointOfInterest destination)
    {
        // ���~���������� ��� ����Ʈ ���ϱ�
        List<PointOfInterest> path = NodeManager.FindPath(currentNode, destination);
        if (path == null || path.Count < 2)
            yield break;

        for (int i = 1; i < path.Count; i++)
        {
            Vector3 start = path[i - 1].transform.position + Vector3.up * 0.5f;
            Vector3 end = path[i].transform.position + Vector3.up * 0.5f;
            yield return MoveAlongArc(start, end, 0.4f, 4.0f); // (duration, arcHeight)
            currentNode = path[i];
        }

        // �̵� �Ϸ� �� ���� �ܰ��
        gameManager.setGameStage(GameStage.Interact);
        gameManager.interactByPOI(this, currentNode);
    }

    // ������ �ִϸ��̼� (�ռ� ������ �ڵ�� ����)
    private IEnumerator MoveAlongArc(Vector3 start, Vector3 end, float duration, float arcHeight)
    {
        float time = 0;
        while (time < duration)
        {
            float t = time / duration;
            float height = 4 * arcHeight * t * (1 - t);
            Vector3 pos = Vector3.Lerp(start, end, t);
            pos.y += height;
            transform.position = pos;
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
    }

    public void SetShortcutUsed(bool used) { shortcutUsed = used; }
    public bool HasUsedShortcut() { return shortcutUsed; }
}
