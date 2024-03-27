using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

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
        public int index;
        public string sender;
        public string message;
    }


    public int playerIndex;

    // ------------------------------------------------ Life Cylce ------------------------------------------------

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
                SendMessage(); // �޼����� ����
                DisableChatWindow(); // ä��â ��Ȱ��ȭ
            }
            else
            {
                ActivateChatWindow(); // ä��â Ȱ��ȭ
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
        if (message.message.Length < 1) return;

        message.sender = UserInfo.GetInstance().getNickName();
        message.index = playerIndex;

        Debug.Log("adiasjdiojaiopd " + PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Number"));

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Number"))
        {
            message.index =  (int)PhotonNetwork.LocalPlayer.CustomProperties["Number"];
            Debug.Log("index : " + message.index);
        }
        string messageJson = JsonUtility.ToJson(message);
        view.RPC("ReceiveMessage", RpcTarget.All, messageJson);
    }

    [PunRPC]
    public void ReceiveMessage(string message)
    {
        ChatMessage chatMessage = JsonUtility.FromJson<ChatMessage>(message);

        GameObject chatPrefab = Instantiate(ChatItem[chatMessage.index]);

        chatPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chatMessage.sender;
        chatPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = chatMessage.message;
        
        chatPrefab.transform.SetParent(chatContainer.transform, false);
    }

    public void openChatting()
    {
        ChattingPanel.SetActive(true);
    }

    // ä��â�� Ȱ��ȭ�ϴ� �޼���
    void ActivateChatWindow()
    {
        if (isChatActive) return;

        // ä�� chatBackground Ȱ��ȭ
        chatBackground.SetActive(true);

        // ä�� InputField Ȱ��ȭ
        chatInputField.ActivateInputField(); // �Է� �ʵ� Ȱ��ȭ
        chatInputField.Select(); // �Է� �ʵ� ����

        // ä��â Ȱ��ȭ
        isChatActive = true;
    }

    // ä��â�� ��Ȱ��ȭ�ϴ� �޼���
    void DisableChatWindow()
    {
        if (!isChatActive) return;

        // InputField �ʱ�ȭ
        chatInputField.text = "";

        // ä�� chatBackground ��Ȱ��ȭ
        chatBackground.SetActive(false);

        // �Է� �ʵ� ��Ȱ��ȭ
        chatInputField.DeactivateInputField();

        // ä��â ��Ȱ��ȭ
        isChatActive = false;
    }
}
