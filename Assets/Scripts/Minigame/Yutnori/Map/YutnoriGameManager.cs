using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public enum GameStage { Start, Throw, Move, Interact, End}
public class YutnoriGameManager : MonoBehaviour
{
    public GameStage stage;
    [SerializeField] private NodeManager nodeManager;
    [SerializeField] private PlayerPiece[] playerPieces; // �÷��̾� �� 4��

    // ���� �� ��� ����
    private string currentYutResult = "";
    private int moveDistance = 0;
    public bool canThrowAgain = false;
    public bool canMove = false;

    // ���õ� ��
    private PlayerPiece selectedPiece = null;

    public bool isDraggingYut { get; private set; } 
    public void SetYutDragState(bool state) => isDraggingYut = state; 

    // Start is called before the first frame update
    void Start()
    {
        setGameStage(GameStage.Interact);
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
            case GameStage.Move:
                {
                    startMoveStage();
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
            case "��": moveDistance = -1; canThrowAgain = false; break;
            case "��": moveDistance = -1; canThrowAgain = false; break;
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

    public void interactByPOI(PlayerPiece piece, PointOfInterest poi)
    {
        switch (poi.Type)
        {
            case POIType.Start:
                break;
            case POIType.Component:
                // UIâ ����
                break;
            case POIType.Upgrade:
                // ���׷��̵� ó��
                break;
            case POIType.Buff:
                // ���� ����
                break;
            case POIType.Shortcut:
                // �����̵� ó��
                break;
            case POIType.End:
                break;
                // ...
        }
    }
}
