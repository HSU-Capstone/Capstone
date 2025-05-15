using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class NPCUI : MonoBehaviour
{
    public List<Button> choiceButtons; // Inspector���� ����Ʒ� ������ �Ҵ�
    public GameObject dialoguePanel;
    public Text dialogueText;

    private NPCController currentNPC;
    private int currentChoiceIndex = 0;
    private bool isChoiceActive = false;

    private bool inputBuffer = false; // ��ȣ�ۿ� + ��ȭ ������ Ȯ�� + ��ȭ Ȯ�� �� ��� �� fŰ�� �ϱ� ������ fŰ �� ���� ������ ��� �� ���� �Ź����� �� ������

    void Start()
    {

    }
    void Update()
    {
        // ��ȭ �г��� �� ���� �� FŰ�� ��ȭ ����
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            currentNPC?.EndDialogue();
        }

        // ��ȭ ������ ���� -----------------------------------
        if (!isChoiceActive) return;

        // ������ UI�� Ȱ��ȭ�� ���� ù �������� FŰ �Է� ���� > FŰ �� �� �����µ� ���� �� ó���Ǵ°� ����
        if (inputBuffer)
        {
            if (!Input.GetKey(KeyCode.F)) // FŰ���� ���� �� ������ ���
                inputBuffer = false;
            return;
        }

        // ��ȭ ������ ����
        // ���� �̵� (W)
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentChoiceIndex = (currentChoiceIndex - 1 + choiceButtons.Count) % choiceButtons.Count;
            HighlightChoice(currentChoiceIndex);
        }
        // �Ʒ��� �̵� (S)
        if (Input.GetKeyDown(KeyCode.S))
        {
            currentChoiceIndex = (currentChoiceIndex + 1) % choiceButtons.Count;
            HighlightChoice(currentChoiceIndex);
        }
        // ���� (F)
        if (Input.GetKeyDown(KeyCode.F))
        {
            inputBuffer = true;
            choiceButtons[currentChoiceIndex].onClick.Invoke();
        }
    }

    // NPCController�� �̺�Ʈ ����
    public void ConnectToNPC(NPCController npc)
    {
        // ���� NPC �̺�Ʈ ����
        if (currentNPC != null)
        {
            currentNPC.OnInteractionStarted -= ShowChoices;
            currentNPC.OnInteractionCanceled -= HideChoices;
            currentNPC.OnDialogueStarted -= ShowDialogueUI;
            currentNPC.OnDialogueEnded -= HideDialogueUI;
        }

        currentNPC = npc;

        // ���ο� NPC �̺�Ʈ ����
        currentNPC.OnInteractionStarted += ShowChoices;
        currentNPC.OnInteractionCanceled += HideChoices;
        currentNPC.OnDialogueStarted += ShowDialogueUI;
        currentNPC.OnDialogueEnded += HideDialogueUI;
    }

    // ��ȭ ������ UI ǥ��
    private void ShowChoices(NPCController npc)
    {
        HideDialogueUI();
        foreach (var btn in choiceButtons)
            btn.gameObject.SetActive(true);

        isChoiceActive = true;
        inputBuffer = true; // ������ UI�� �ߴ� ���� FŰ �Է� ����
        currentChoiceIndex = 0;
        HighlightChoice(currentChoiceIndex);
    }

    // ������ UI ����
    private void HideChoices()
    {
        foreach (var btn in choiceButtons)
            btn.gameObject.SetActive(false);

        isChoiceActive = false;
    }

    // ��ȭâ ǥ��
    private void ShowDialogueUI()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = "�ȳ��ϼ���";
        HideChoices();
    }

    // ��ȭâ ����
    private void HideDialogueUI()
    {
        dialoguePanel.SetActive(false);
    }

    // ���̶���Ʈ(����) ǥ��
    private void HighlightChoice(int idx)
    {
        // EventSystem�� �̿��� ��ư ���̶���Ʈ
        EventSystem.current.SetSelectedGameObject(choiceButtons[idx].gameObject);
    }

    public void OnTalkBtnClicked()
    {
        currentNPC?.StartDialogue();
    }

    public void OnCancelBtnClicked()
    {
        currentNPC?.CancelInteraction();
    }

    public bool getIsChoiceActive()
    {
        return isChoiceActive;
    }
}
