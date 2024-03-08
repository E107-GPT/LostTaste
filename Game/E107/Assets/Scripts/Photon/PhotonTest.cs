using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonTest : MonoBehaviourPunCallbacks
{
    #region private serializable fields
    [Tooltip("�� �ִ� ���� ��")]
    [SerializeField]
    private byte maxplayersPerRoom = 4;
    #endregion

    #region private fields
    // Ŭ���̾�Ʈ ��ȣ
    string gameVersion = "1";
    bool isConnecting;

    #endregion

    #region public fields
    [Tooltip("�ǳ��� �̸��� ��ư ����")]
    [SerializeField]
    public  GameObject controlPanel;
    
    [Tooltip("�ǳ��� �̸��� ��ư ����")]
    [SerializeField]
    public  GameObject progressLabel;

    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        // �����Ͱ� ����� �ε��ϸ� ���� �濡 �ִ� ��� Ŭ���̾�Ʈ�� �ڵ����� ������ ����ȭ�ϵ��� ��
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }
    #endregion
    
    #region public Methods
    // Connect ���� ������ �����ϴ� �޼ҵ�
    public void Connect()
    {
        isConnecting = true;
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        // ���� ��Ʈ��ũ ���� ���� Ȯ��
        if (PhotonNetwork.IsConnected)
        {
            // ������ �� ����
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // ���� ���� ����
            PhotonNetwork.GameVersion = gameVersion;

            // ���� Ŭ���忡 ����Ǵ� ���� ����
            PhotonNetwork.ConnectUsingSettings();            
        }        
    }

    public void makeRoom()
    {
        string roomName = GameObject.Find("GameManager").GetComponent<UIManager>().GetTitle();
        RoomOptions room = new RoomOptions();
        room.MaxPlayers = 4;
        room.IsVisible = true;
        room.IsOpen= true;

        PhotonNetwork.CreateRoom(roomName, room);
    }
    #endregion

    #region MonoBehaviourPunCallbacks callbacks
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
        Debug.Log("OnConnectedToMaster");
        // �����Ϳ� ���� �� ���� �� ����
        PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("OnDisconnected {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxplayersPerRoom});
        Debug.Log("�� ���� ����");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        PhotonNetwork.LoadLevel("Room for 1");
    }
    #endregion
}
