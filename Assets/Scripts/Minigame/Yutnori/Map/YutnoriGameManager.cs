using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEngine;

public enum GameStage { Start, Throw, Move, Interact, End}
public class YutnoriGameManager : MonoBehaviour
{
    public GameStage stage;
    [SerializeField] private NodeManager nodeManager;
    [SerializeField] private ShortcutDialogUI shortcutDialogUI;
    [SerializeField] private PlayerPiece[] playerPieces; // �÷��̾� �� 4��

    // ���� �� ��� ����
    private string currentYutResult = "";
    private int moveDistance = 0;
    public bool canThrowAgain = false;
    public bool canMove = false; // MoveStage������ POI Ŭ�� ������ �ñ�� ������ �־ �̰ɷ� ����

    // ���õ� ��
    private PlayerPiece selectedPiece = null;

    public bool isDraggingYut { get; private set; } 
    public void SetYutDragState(bool state) => isDraggingYut = state; 

    // Start is called before the first frame update
    void Start()
    {
        setGameStage(GameStage.Throw);
        // YutController���� �� ������ ���� ����� ��� GameStage�� Move�� �ٲ�
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setGameStage(GameStage stage) 
    {
        this.stage = stage;
        switch (stage)
        {
            case GameStage.Start:
                {
                    // ó�� ���۽� 
                    break;
                }
            case GameStage.Throw:
                {
                    HighlightAllPieces(false);
                    break;
                }
            case GameStage.Move:
                {
                    startMoveStage();
                    break;
                }
            case GameStage.End:
            {
                    Debug.Log("END");
                break;
            }
        }
    }
    public void ProcessYutResult(string result)
    {
        currentYutResult = result;

        // ����� ���� �̵� �Ÿ� ����
        switch (result)
        {
            case "��": moveDistance = 1; canThrowAgain = false; break;
            case "��": moveDistance = 2; canThrowAgain = false; break;
            case "��": moveDistance = 3; canThrowAgain = false; break;
            case "��": moveDistance = 4; canThrowAgain = true; break;
            case "��": moveDistance = 5; canThrowAgain = true; break;
            case "����": moveDistance = -1; canThrowAgain = false; break;
            default: moveDistance = 0; canThrowAgain = false; break;
        }
    }
    public void startMoveStage() 
    {
        // �� ���� �ܰ�� ����
        HighlightAllPieces(true);
        // PlayerPiece���� onMouseDown���� SelectPiece ȣ��
        // SelectPiece�� nodeManager�� HighlightReachableNodes ȣ��
    }

    // ��� �� ���̶���Ʈ
    public void HighlightAllPieces(bool highlight)
    {
        foreach (var piece in playerPieces)
        {
            piece.Highlight(highlight);
        }
    }

    // �� ����
    public void SelectPiece(PlayerPiece piece)
    {
        selectedPiece = piece;
        HighlightAllPieces(false);
        piece.Highlight(true);

        // NodeManager���� ��� Ž�� �� ���̶���Ʈ ��û
        nodeManager.HighlightReachableNodes(piece.currentNode, moveDistance);
    }

    // ���õ� ���� ���� �̵�
    public void MoveSelectedPieceTo(PointOfInterest node)
    {
        if (selectedPiece != null)
        {
            nodeManager.ClearHighlights(); // NodeManager���� ���̶���Ʈ ���� ��û
            selectedPiece.MoveTo(node);
            selectedPiece = null;
        }
    }
    private void UseShortcut(PlayerPiece piece, PointOfInterest currentPoi)
    {
        // �̹� ������ ��� ������ üũ (�÷��� �ʿ��)
        // ���� ������ ��� ã�� (��: currentPoi.NextPointsOfInterest �� Type == Shortcut)
        PointOfInterest nextShortcut = FindNextShortcut(currentPoi, 10); // 10ĭ �̳����� Ž��
        Debug.Log(nextShortcut.name);


        if (nextShortcut != null)
        {
            // �ߺ� ���� �÷��� ����
            piece.SetShortcutUsed(true); 
            selectedPiece = piece;
            MoveSelectedPieceTo(nextShortcut);
        }
    }

    // Nĭ ���� Shortcut ��� ã�� (����: 5ĭ ��)
    private PointOfInterest FindNextShortcut(PointOfInterest start, int maxStep = 10)
    {
        var current = start;
        for (int i = 0; i < maxStep; i++)
        {
            if (current.Type == POIType.Shortcut && i != 0)
                return current;
            if (current.NextPointsOfInterest == null || current.NextPointsOfInterest.Count == 0)
                break;
            current = current.NextPointsOfInterest[0]; // �б� ���� �ܼ� ��ζ�� [0]
        }
        return null;
    }

    public void interactByPOI(PlayerPiece piece, PointOfInterest poi)
    {
        switch (poi.Type)
        {
            case POIType.Start:
                break;
            case POIType.Component:
                // UIâ ����
                setGameStage(GameStage.Throw);
                break;
            case POIType.Upgrade:
                // ���׷��̵� ó��
                setGameStage(GameStage.Throw);
                break;
            case POIType.Buff:
                // ���� ����
                setGameStage(GameStage.Throw);
                break;
            case POIType.Shortcut:
                {
                    if (!piece.HasUsedShortcut()) // �̹� �Ͽ� ������ �� ������
                        shortcutDialogUI.Show(
                            () => UseShortcut(piece, poi), // ��
                            () => { setGameStage(GameStage.Throw); } // �ƴϿ�
                        );
                    else setGameStage(GameStage.Throw); 
                break;
                }
            case POIType.End:
                setGameStage(GameStage.End);
                break;
                // ...
        }
    }
}
