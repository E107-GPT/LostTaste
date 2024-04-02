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
/// 채팅창을 관리하는 클래스입니다.
/// </summary>
public class PhotonChatManager : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 채팅 Background
    [Header("[ 채팅 Background ]")]
    public GameObject chatBackground; // 채팅창 활성화시 캐릭터 조작 불가능하게 하기 위해 화면 전체를 가리는 투명한 GameObject

    // 채팅창
    [Header("[ 채팅창 ]")]
    public GameObject ChattingPanel;

    // 채팅 InputField
    [Header("[ 채팅 InputField ]")]
    public TMP_InputField chatInputField;

    // 채팅 컨테이너
    [Header("[ 채팅 컨테이너 ]")]
    public GameObject chatContainer;

    // 채팅 아이템
    [Header("[ 채팅 아이템 ]")]
    public GameObject[] ChatItem;

    // 채팅창 오픈 여부
    bool isChatOpen;

    // 채팅창 활성화 여부
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

        // Enter 키를 인식
        if (isChatOpen && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            if (!isChatActive && chatInputField.isFocused == false)
            {
                ActivateChatWindow(); // 채팅창 활성화
            }
            else if (isChatActive)
            {
                SendMessage(); // 메세지를 보냄
                DisableChatWindow(); // 채팅창 비활성화
            }
        }
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 채팅창 활성화 되어 있을 시 메세지를 보내는 메서드
    // rpc로 send -> get message로 전달
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

        DisableChatWindow();
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

    // 채팅창을 활성화하는 메서드
    void ActivateChatWindow()
    {
        if (isChatActive) return;

        // 채팅 chatBackground 활성화
        chatBackground.SetActive(true);

        // 채팅 InputField 활성화
        chatInputField.ActivateInputField(); // 입력 필드 활성화
        chatInputField.Select(); // 입력 필드 선택

        // 채팅창 활성화
        isChatActive = true;
    }

    // 채팅창을 비활성화하는 메서드
    void DisableChatWindow()
    {
        if (!isChatActive) return;

        // InputField 초기화
        chatInputField.text = "";

        // 채팅 chatBackground 비활성화
        chatBackground.SetActive(false);

        // 입력 필드 비활성화
        chatInputField.DeactivateInputField();

        // 채팅창 비활성화
        isChatActive = false;
    }
}
