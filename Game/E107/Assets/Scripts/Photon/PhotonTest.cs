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

    [Tooltip("�ǳ��� �̸��� ��ư ����")]
    [SerializeField]
    public GameObject createRoomPanel;

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
        createRoomPanel.SetActive(false);
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
            // ����Ʈ ����ϱ�

            //PhotonNetwork.JoinRandomRoom();
            Debug.Log("�κ�");
        }
        else
        {
            // ���� ���� ����
            PhotonNetwork.GameVersion = gameVersion;

            // ���� Ŭ���忡 ����Ǵ� ���� ����
            PhotonNetwork.ConnectUsingSettings();            
        }        
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("JoinLobby");
        
        //PhotonNetwork.GetCustomRoomList(Photon.Realtime.TypedLobbyInfo.Default, "ispassword");
    }

    public void makeRoom()
    {
        UIManager manager = GameObject.Find("GameManager").GetComponent<UIManager>();
        string roomName = manager.GetTitle();
        RoomOptions room = new RoomOptions();
        room.MaxPlayers = 4;
        room.IsVisible = true;
        room.IsOpen= true;
        bool ispassword = manager.GetIsPassword();
        int password = manager.GetPassword();
        Debug.Log("pw" +password);
        if(ispassword)
            room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "ispassword", ispassword},{ "password", password} };
        else
            room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "ispassword", false } };
        Debug.Log("pw" + room.CustomRoomProperties["ispassword"]);
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
            progressLabel.SetActive(false);
            createRoomPanel.SetActive(true);

            
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo rooom in roomList)
        {
            Debug.Log("room : " + rooom.Name);

            ExitGames.Client.Photon.Hashtable has = rooom.CustomProperties;

            
                Debug.Log("room : " + (bool)rooom.CustomProperties["ispassword"]);
                Debug.Log("room +: " + (int)rooom.CustomProperties["password"]);
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
