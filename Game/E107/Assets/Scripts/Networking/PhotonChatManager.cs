using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// <summary>
/// ä��â�� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class PhotonChatManager : MonoBehaviour
{
    // ------------------------------------------------ ���� ���� ------------------------------------------------
    // ä�� Background
    [Header("[ ä�� Background ]")]
    public GameObject chatBackground; // ä��â Ȱ��ȭ�� ĳ���� ���� �Ұ����ϰ� �ϱ� ���� ȭ�� ��ü�� ������ ������ GameObject

    // ä��â
    [Header("[ ä��â ]")]
    public GameObject ChattingPanel;

    // ä�� InputField
    [Header("[ ä�� InputField ]")]
    public TMP_InputField chatInputField;

    // ä�� ���� ��ư
    [Header("[ ä�� ���� ��ư ]")]
    public Button sendButton;

    // ä�� �����̳�
    [Header("[ ä�� �����̳� ]")]
    public GameObject chatContainer;

    // ä�� ������
    [Header("[ ä�� ������ ]")]
    public GameObject[] ChatItem;

    // ä��â ���� ����
    bool isChatOpen;

    // ä��â Ȱ��ȭ ����
    bool isChatActive;

    // Serialization to send to Photon
    [System.Serializable]
    public class ChatMessage
    {
        public string sender;
        public string message;
    }


    // ------------------------------------------------ Life Cylce ------------------------------------------------

    // private void Awake()
    // {
        // ��ư�� �̺�Ʈ �߰�
    //     if (sendButton != null)
    //         this.sendButton.onClick.AddListener(SendMessage);
    // }

    private void Start()
    {
        ChattingPanel.SetActive(false);
        isChatOpen = false;
        isChatActive = false;
    }

    private void Update()
    {
        if (isChatOpen != PhotonNetwork.InRoom)
        {
            isChatOpen = PhotonNetwork.InRoom;
            ChattingPanel.SetActive(isChatOpen);
        }

        // Enter Ű�� �ν�
        if (isChatOpen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            if (isChatActive)
            {
                SendMessage(); // ä��â Ȱ��ȭ �Ǿ� ���� �� �޼����� ����
            }
            else
            {
                DetectUserActivity(); // ä��â ��Ȱ��ȭ �Ǿ� ���� �� ä��â�� Ȱ��ȭ��
            }
        }
    }


    // ------------------------------------------------ ����� ���� �޼��� ------------------------------------------------

    // ä��â Ȱ��ȭ �Ǿ� ���� �� �޼����� ������ �޼���
    // rpc�� send -> get message�� ����
    public void SendMessage()
    {
        if (!isChatActive) return;

        ChatMessage message = new ChatMessage();

        PhotonView view = gameObject.GetComponent<PhotonView>();//GameObject.Find("Player").GetComponent<PlayerController>().photonView;
        message.message = GameObject.Find("gm").GetComponent<PhotonUIManager>().GetChatMessage();
        

        // empty message
        if (message.message.Length < 1)
        {
            // InputField �ʱ�ȭ
            chatInputField.text = "";

            // ä�� chatBackground ��Ȱ��ȭ
            chatBackground.SetActive(false);

            // �Է� �ʵ� ��Ȱ��ȭ
            chatInputField.DeactivateInputField();

            // ä��â ��Ȱ��ȭ
            isChatActive = false;

            return;
        }

        message.sender = UserInfo.GetInstance().getNickName();

        string messageJson = JsonUtility.ToJson(message);
        view.RPC("ReceiveMessage", RpcTarget.Others, messageJson);

        GameObject chatPrefab = Instantiate(ChatItem[1]);

        chatPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message.sender;
        chatPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = message.message;

        chatPrefab.transform.SetParent(chatContainer.transform, false);

        // InputField �ʱ�ȭ
        chatInputField.text = "";

        // ä�� chatBackground ��Ȱ��ȭ
        chatBackground.SetActive(false);

        // �Է� �ʵ� ��Ȱ��ȭ
        chatInputField.DeactivateInputField();

        // ä��â ��Ȱ��ȭ
        isChatActive = false;

    }

    [PunRPC]
    public void ReceiveMessage(string message)
    {
        ChatMessage chatMessage = JsonUtility.FromJson<ChatMessage>(message);

        GameObject chatPrefab = Instantiate(ChatItem[0]);

        chatPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chatMessage.sender;
        chatPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = chatMessage.message;
        
        chatPrefab.transform.SetParent(chatContainer.transform, false);
    }

    public void openChatting()
    {
        ChattingPanel.SetActive(true);
    }

    // ä��â ��Ȱ��ȭ �Ǿ� ���� �� ä��â�� Ȱ��ȭ�ϴ� �޼���
    void DetectUserActivity()
    {
        if (isChatActive) return;

        // InputField�� ��Ŀ��
        // EventSystem.current.SetSelectedGameObject(chatInputField.gameObject, null);

        // ä�� chatBackground Ȱ��ȭ
        chatBackground.SetActive(true);

        // ä�� InputField Ȱ��ȭ
        chatInputField.ActivateInputField(); // �Է� �ʵ� Ȱ��ȭ
        chatInputField.Select(); // �Է� �ʵ� ����

        // ä��â Ȱ��ȭ
        isChatActive = true;
    }
}
