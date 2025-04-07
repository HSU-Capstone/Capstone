using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;
using ExitGames.Client.Photon;

public class ChatManager : MonoBehaviour, IChatClientListener
{
    [Header("UI Reference")]
    public GameObject chatPanel;                         // Layout ������Ʈ
    public TMP_InputField chatInputField;                // Input/InputField
    public TMP_Text chatText;                            // Output/Scroll View/Viewport/Content �� TMP_Text
    public ScrollRect scrollRect;                        // Output/Scroll View

    private ChatClient chatClient;
    private string currentChannel = "Global";
    private bool isChatActive = false;

    void Start()
    {
        // �г��� �ڵ� ����
        if (string.IsNullOrEmpty(PhotonNetwork.NickName))
            PhotonNetwork.NickName = "User" + Random.Range(1000, 9999);

        chatPanel.SetActive(false);

        chatClient = new ChatClient(this);
        chatClient.Connect(
            PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat,
            "1.0",
            new AuthenticationValues(PhotonNetwork.NickName)
        );
    }

    void Update()
    {
        chatClient?.Service();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isChatActive)
            {
                OpenChat(); // ä��â ����
            }
            else
            {
                string message = chatInputField.text.Trim();

                if (!string.IsNullOrEmpty(message))
                {
                    SendChatMessage(message);
                    chatInputField.text = ""; // �޽��� ���� �� �Է�â�� �ʱ�ȭ
                    chatInputField.ActivateInputField(); // ��Ŀ�� ����
                }
                else
                {
                    CloseChat(); // �ƹ� �Է� ���� ��쿡�� �ݱ�
                }
            }
        }

        if (isChatActive && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseChat();
        }
    }


    void OpenChat()
    {
        chatPanel.SetActive(true);
        chatInputField.text = "";
        chatInputField.ActivateInputField();
        isChatActive = true;
    }

    void CloseChat()
    {
        chatInputField.DeactivateInputField();
        chatPanel.SetActive(false);
        isChatActive = false;
    }

    void SendChatMessage(string message)
    {
        chatClient.PublishMessage(currentChannel, message);
        chatInputField.text = "";
    }

    public void OnConnected()
    {
        Debug.Log("Connected to Photon Chat");
        chatClient.Subscribe(new string[] { currentChannel });
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < messages.Length; i++)
        {
            chatText.text += $"\n<color=yellow>{senders[i]}</color>: {messages[i]}";
        }

        ScrollToBottom();
    }

    void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    // �ʼ� �������̽� �޼����
    public void OnChatStateChange(ChatState state) { }
    public void OnDisconnected() => Debug.Log("Disconnected from Photon Chat");
    public void OnSubscribed(string[] channels, bool[] results) { }
    public void OnUnsubscribed(string[] channels) { }
    public void OnPrivateMessage(string sender, object message, string channelName) { }
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message) { }
    public void OnUserSubscribed(string channel, string user) { }
    public void OnUserUnsubscribed(string channel, string user) { }
    public void DebugReturn(DebugLevel level, string message) => Debug.Log(message);
}
