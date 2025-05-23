public class PlayerState
{
    // �̵� �� ���� ����
    public int moveDistance = 0;
    public bool canMove = false;
    public PlayerPiece piece; // �� �÷��̾��� ��

    // �� ��� ����
    public string currentYutResult = "";
    public int bonusThrowCount = 0; // ���� Ȥ�� ��/��

    // ���� ����
    public bool canStackPiece = false;
    public int nextMovePlus = 0;
    public bool nextBuffAutoSuccess = false;

    // ���� �ʱ�ȭ
    public void ResetTurn()
    {
        moveDistance = 0;
        canMove = false;
        currentYutResult = "";
        bonusThrowCount = 0;
        // selectedPiece�� piece�� �Ͽ�ȭ
    }

    // ���� �Ҹ� ó��
    public void ConsumeNextMovePlus()
    {
        nextMovePlus = 0;
    }
    public void ConsumeNextBuffAutoSuccess()
    {
        nextBuffAutoSuccess = false;
    }
}
