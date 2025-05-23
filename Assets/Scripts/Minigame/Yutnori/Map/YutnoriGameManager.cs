using UnityEngine;

public enum GameStage { Start, Throw, Move, Interact, End }

public class YutnoriGameManager : MonoBehaviour
{
    public GameStage stage;
    [SerializeField] private NodeManager nodeManager;
    [SerializeField] private ShortcutDialogUI shortcutDialogUI;
    [SerializeField] private EndPanelUI endPanelUI;
    [SerializeField] private PlayerPiece[] playerPieces;
    private QuizManager quizManager;
    [SerializeField] private GameUIManager gameUIManager; // �ϼ� ȭ�鿡 ���� ��

    // ���� ���� �÷��̾�� ���� ����(�� �� ������ ��)
    public PlayerState[] playerStates;
    private int currentPlayerIndex = 0;
    public PlayerState CurrentPlayer => playerStates[currentPlayerIndex];

    // �� ���̳� ��������
    private int turnCount = 1;
    public int TurnCount => turnCount;

    public bool isDraggingYut { get; private set; }
    public void SetYutDragState(bool state) => isDraggingYut = state;

    void Start()
    {
        // PlayerState �迭�� null�̰ų� ũ�Ⱑ 0�̸� playerPieces.Length�� ���� ����
        if (playerStates == null || playerStates.Length != playerPieces.Length)
            playerStates = new PlayerState[playerPieces.Length];

        gameUIManager.UpdateTurn(turnCount); // ui �ؽ�Ʈ �ʱ�ȭ

        quizManager = GetComponent<QuizManager>();

        // �� ��Ұ� null�̸� new PlayerState()�� ����
        for (int i = 0; i < playerStates.Length; i++)
        {
            if (playerStates[i] == null)
                playerStates[i] = new PlayerState();

            playerStates[i].piece = playerPieces[i];
            playerPieces[i].playerState = playerStates[i];
        }
        setGameStage(GameStage.Throw);
    }


    void Update() { }

    public void setGameStage(GameStage stage)
    {
        this.stage = stage;
        switch (stage)
        {
            case GameStage.Start:
                break;
            case GameStage.Throw:
                HighlightAllPieces(false);
                break;
            case GameStage.Move:
                startMoveStage();
                break;
            case GameStage.End:
                endPanelUI.Show();
                Debug.Log("END");
                break;
        }
    }

    public void ProcessYutResult(string result)
    {
        CurrentPlayer.currentYutResult = result;
        switch (result)
        {
            case "��": CurrentPlayer.moveDistance = 1; break;
            case "��": CurrentPlayer.moveDistance = 2; break;
            case "��": CurrentPlayer.moveDistance = 3; break;
            case "��": CurrentPlayer.moveDistance = 4; CurrentPlayer.bonusThrowCount++; break;
            case "��": CurrentPlayer.moveDistance = 5; CurrentPlayer.bonusThrowCount++; break;
            case "����": CurrentPlayer.moveDistance = -1; break;
            default: CurrentPlayer.moveDistance = 0; break;
        }
    }

    public void startMoveStage()
    {
        HighlightAllPieces(true);
    }

    public void HighlightAllPieces(bool highlight)
    {
        foreach (var piece in playerPieces)
        {
            piece.Highlight(highlight);
        }
    }

    public void SelectPiece(PlayerPiece piece)
    {
        CurrentPlayer.piece = piece;
        HighlightAllPieces(false);
        piece.Highlight(true);
        nodeManager.HighlightReachableNodes(piece.currentNode, CurrentPlayer.moveDistance);
    }

    public void MoveSelectedPieceTo(PointOfInterest node)
    {
        if (CurrentPlayer.piece != null)
        {
            nodeManager.ClearHighlights();
            CurrentPlayer.piece.MoveTo(node);
            // CurrentPlayer.piece = null; // �ʿ�� ����
        }
    }

    private void UseShortcut(PlayerPiece piece, PointOfInterest currentPoi)
    {
        PointOfInterest nextShortcut = FindNextShortcut(currentPoi, 10);
        Debug.Log(nextShortcut != null ? nextShortcut.name : "No shortcut found");

        if (nextShortcut != null)
        {
            piece.SetShortcutUsed(true);
            CurrentPlayer.piece = piece;
            MoveSelectedPieceTo(nextShortcut);
        }
    }

    private PointOfInterest FindNextShortcut(PointOfInterest start, int maxStep = 10)
    {
        var current = start;
        for (int i = 0; i < maxStep; i++)
        {
            if (current.Type == POIType.Shortcut && i != 0)
                return current;
            if (current.NextPointsOfInterest == null || current.NextPointsOfInterest.Count == 0)
                break;
            current = current.NextPointsOfInterest[0];
        }
        return null;
    }
    public void EndTurn()
    {
        // ���ʽ� ������(��/��/���� ��) ��ȸ�� ���� ������ �� ī��Ʈ ���� ���� �߰� ������
        if (CurrentPlayer.bonusThrowCount > 0)
        {
            CurrentPlayer.bonusThrowCount--;
            setGameStage(GameStage.Throw);
        }
        else
        {
            turnCount++;
            gameUIManager.UpdateTurn(turnCount);
            // (���� �÷��̾��� ���� �÷��̾�� �ε��� ����)
            // currentPlayerIndex = (currentPlayerIndex + 1) % playerStates.Length;
            setGameStage(GameStage.Throw);
        }

        // (���⼭ �� ī��Ʈ UI ���� �� �߰� ó��)
    }

    public void interactByPOI(PlayerPiece piece, PointOfInterest poi)
    {
        switch (poi.Type)
        {
            case POIType.Start:
                break;
            case POIType.Component:
                setGameStage(GameStage.Throw);
                break;
            case POIType.Upgrade:
                setGameStage(GameStage.Throw);
                break;
            case POIType.Buff:
                // ���� �ڵ� ���� ���� �Ҹ� ó��
                if (piece.playerState.nextBuffAutoSuccess)
                {
                    // ���� ������ ���� ó��
                    piece.playerState.ConsumeNextBuffAutoSuccess();
                }
                quizManager.ShowRandomQuiz((isCorrect) =>
                {
                    // ���� ����� ���� ���ʽ� ������ ȹ�� �� ó����
                    EndTurn(); // setGameStage(GameStage.Throw) ��� EndTurn ȣ��
                }, piece.playerState);
                break;

            case POIType.Shortcut:
                if (!piece.HasUsedShortcut())
                {
                    var nextShortcut = FindNextShortcut(poi);
                    bool isLastShortcut = (nextShortcut == null);

                    shortcutDialogUI.Show(
                        () => UseShortcut(piece, poi),
                        () => { EndTurn(); }, // setGameStage(GameStage.Throw) ��� EndTurn ȣ��
                        isLastShortcut
                    );
                }
                else EndTurn();
                break;
            case POIType.End:
                setGameStage(GameStage.End);
                break;
        }
    }
}
