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
    List<RoomInfo> roomlist = new List<RoomInfo>();
    // ������ �� ����
    private RoomInfo selectRoom;
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
    
    
    [Tooltip("�ǳ��� �̸��� ��ư ����")]
    [SerializeField]
    public GameObject passwordPanel;


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
        passwordPanel.SetActive(false);
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
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            createRoomPanel.SetActive(true);
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
        progressLabel.SetActive(false);
        createRoomPanel.SetActive(true);
        //PhotonNetwork.GetCustomRoomList(Photon.Realtime.TypedLobbyInfo.Default, "ispassword");
    }

    public void makeRoom()
    {
        UIManager manager = GameObject.Find("GameManager").GetComponent<UIManager>();
        string roomName = manager.GetTitle();
        string captainName = manager.GetName();
        RoomOptions room = new RoomOptions();
        room.MaxPlayers = 4;
        room.IsVisible = true;
        room.IsOpen= true;
        bool ispassword = manager.GetIsPassword();
        int password = manager.GetPassword();
        Debug.Log("pw" +password);
        if (ispassword)
        {
            room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "captain", captainName }, { "ispassword", ispassword},{ "password", password} };
            room.CustomRoomPropertiesForLobby = new string[] { "captain","ispassword", "password" };
        }
        else
        {
            room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "captain", captainName }, { "ispassword", ispassword } };
            room.CustomRoomPropertiesForLobby = new string[] { "captain", "ispassword" };
        }

        Debug.Log("pw" + (bool)room.CustomRoomProperties["ispassword"]);
        PhotonNetwork.CreateRoom(roomName, room);
    }

    public void roomEnter()
    {
        if (roomlist.Count < 1) return;
        
        RoomInfo room = roomlist[0];
        if ((bool)room.CustomProperties["ispassword"])
        {
            //password panel open
            // ��� ���� �� ����
            selectRoom = room;
            passwordPanel.SetActive(true);
        }
        else
        {
            // no password �ٷ� ����
            PhotonNetwork.JoinRoom(room.Name);
        }  
    }

    public void PasswordValidation(int pw)
    {
        // ��й�ȣ ������ ����
        if((int)selectRoom.CustomProperties["password"] == pw)
        {
            PhotonNetwork.JoinRoom(selectRoom.Name);
        }
    }

    public void printList()
    {
        foreach(RoomInfo rooom in roomlist)
        {
            ExitGames.Client.Photon.Hashtable has = rooom.CustomProperties;
            string roomInfo = "room : " + rooom.Name + " \n" + rooom.PlayerCount + " / " + rooom.MaxPlayers + "\n" + "isvisible : " + rooom.IsVisible + "\n" + "isopen : " + rooom.IsOpen
                + "\n captain : " + has["captain"] + "\n" + has["ispassword"] + " / " + has["password"];
            Debug.Log(roomInfo);
        }
    }
    #endregion

    #region MonoBehaviourPunCallbacks callbacks
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            Debug.Log("OnConnectedToMaster");
            // �����Ϳ� ���� �� ���� �� ����
            
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo rooom in roomList)
        {
            int idx = 0;
            bool changed= false;
            if (roomlist.Count > 0)
            {
                foreach(RoomInfo newroom in roomlist)
                {
                    if(rooom.Name == newroom.Name)
                    {
                        changed = true;
                        break;
                    }
                    idx++;
                }

                   roomlist[idx] = rooom;

            }
            if(!changed || roomlist.Count <1)
                roomlist .Add(rooom);

            //ExitGames.Client.Photon.Hashtable has = rooom.CustomProperties;
            //string roomInfo = "room : " + rooom.Name + " \n" + rooom.PlayerCount + " / " + rooom.MaxPlayers +"\n" + "isvisible : " +rooom.IsVisible+ "\n" + "isopen : " + rooom.IsOpen 
            //    + "\n captain : " + has["captain"] + "\n" + has["ispassword"] + " / " + has["password"];
            //Debug.Log(roomInfo);
        }
        printList();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        isConnecting = false;
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("OnDisconnected {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("�� ���� ����");
    }

    public override void OnCreatedRoom()
    {
        
        PhotonNetwork.LoadLevel("Room for 1");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

    }

    
    #endregion
}
