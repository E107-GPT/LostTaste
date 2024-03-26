using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class PhotonChatManager : MonoBehaviour
{
    public TMP_InputField chatInputField;
    public GameObject chatContainer;
    public GameObject ChattingPanel;
    public GameObject[] ChatLabel;

    bool openChat;

    // Serialization to send to Photon
    [System.Serializable]
    public class ChatMessage
    {
        public string sender;
        public string message;
    }

    private void Start()
    {
        ChattingPanel.SetActive(false);
        openChat = false;
    }
    private void Update()
    {
        if(openChat != PhotonNetwork.InRoom)
        {
            openChat = PhotonNetwork.InRoom;
            ChattingPanel.SetActive(openChat);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendMessage();

            EventSystem.current.SetSelectedGameObject(chatInputField.gameObject, null);
            chatInputField.ActivateInputField(); // 입력 필드 활성화
            chatInputField.Select(); // 입력 필드 선택

        }
    }

    // rpc로 send -> get message로 전달
    public void SendMessage()
    {
        ChatMessage message = new ChatMessage();

        PhotonView view = gameObject.GetComponent<PhotonView>();//GameObject.Find("Player").GetComponent<PlayerController>().photonView;
        message.message = GameObject.Find("gm").GetComponent<PhotonUIManager>().GetChatMessage();
        

        // empty message
        if (message.message.Length < 1) return;

        message.sender = UserInfo.GetInstance().getNickName();

        string messageJson = JsonUtility.ToJson(message);
        view.RPC("ReceiveMessage", RpcTarget.Others, messageJson);

        GameObject chatPrefab = Instantiate(ChatLabel[1]);

        chatPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = message.sender;
        chatPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = message.message;

        chatPrefab.transform.SetParent(chatContainer.transform, false);

        chatInputField.text = "";
    }

    [PunRPC]
    public void ReceiveMessage(string message)
    {
        ChatMessage chatMessage = JsonUtility.FromJson<ChatMessage>(message);

        GameObject chatPrefab = Instantiate(ChatLabel[0]);

        chatPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chatMessage.sender;
        chatPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = chatMessage.message;
        
        chatPrefab.transform.SetParent(chatContainer.transform, false);

    }

    public void openChatting()
    {
        ChattingPanel.SetActive(true);
    }

}
