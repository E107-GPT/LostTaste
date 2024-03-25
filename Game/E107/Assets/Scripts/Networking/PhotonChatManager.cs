using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PhotonChatManager : MonoBehaviour
{
    public GameObject chatting;
    public GameObject chatContainer;
    public GameObject ChattingPanel;

    // Serialization to send to Photon
    [System.Serializable]
    public class ChatMessage
    {
        public string sender;
        public string message;
        public DateTime date;
    }

    private void Start()
    {
        ChattingPanel.SetActive(false);
    }

    // rpc로 send -> get message로 전달
    public void SendMessage()
    {
        ChatMessage message = new ChatMessage();

        PhotonView view = gameObject.GetComponent<PhotonView>();//GameObject.Find("Player").GetComponent<PlayerController>().photonView;
        message.message = GameObject.Find("gm").GetComponent<PhotonUIManager>().GetChatMessage();
        message.sender = UserInfo.GetInstance().getNickName();
        message.date = DateTime.Now;

        string messageJson = JsonUtility.ToJson(message);
        view.RPC("ReceiveMessage", RpcTarget.All, messageJson);
    }

    [PunRPC]
    public void ReceiveMessage(string message)
    {
        ChatMessage chatMessage = JsonUtility.FromJson<ChatMessage>(message);

        GameObject chatPrefab = Instantiate( Resources.Load<GameObject>("Chat"));

        chatPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = chatMessage.sender;
        chatPrefab.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = chatMessage.message;
        chatPrefab.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = chatMessage.date.ToString("HH:mm:ss");
        
        chatPrefab.transform.SetParent(chatContainer.transform, false);

    }

    public void openChatting()
    {
        ChattingPanel.SetActive(true);
    }

}
