using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
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

    // �� �̸����� �� ���� ����
    // �ߺ� �Ұ�
    Dictionary<string, RoomInfo> roomlist = new Dictionary<string, RoomInfo>();

    // ������ �� ����
    private RoomInfo selectRoom;
    #endregion

    #region public fields
    [Tooltip("�ǳ��� �̸��� ��ư ����")]
    [SerializeField]
    public GameObject controlPanel;

    [Tooltip("�ǳ��� �̸��� ��ư ����")]
    [SerializeField]
    public GameObject progressLabel;

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
    }

    public void makeRoom()
    {
        PhotonUIManager manager = GameObject.Find("GameManager").GetComponent<PhotonUIManager>();
        string roomName = manager.GetTitle();
        string captainName = manager.GetName();

        if (captainName == null || roomName == null) return;


        RoomOptions room = new RoomOptions();
        room.MaxPlayers = 4;
        room.IsVisible = true;
        room.IsOpen = true;
        PhotonNetwork.NickName = manager.GetName();
        bool ispassword = manager.GetIsPassword();
        int password = manager.GetPassword();
        Debug.Log("pw" + password);
        if (ispassword)
        {
            room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "captain", captainName }, { "ispassword", ispassword }, { "password", password } };
            room.CustomRoomPropertiesForLobby = new string[] { "captain", "ispassword", "password" };
        }
        else
        {
            room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "captain", captainName }, { "ispassword", ispassword } };
            room.CustomRoomPropertiesForLobby = new string[] { "captain", "ispassword" };
        }

        Debug.Log("pw" + (bool)room.CustomRoomProperties["ispassword"]);
        PhotonNetwork.CreateRoom(roomName, room);
    }

    public void roomEnter(string roomName)
    {
        if (roomlist.Count < 1) return;
        string nickname = GameObject.Find("GameManager").GetComponent<PhotonUIManager>().GetName();
        if (nickname == null) return;
        PhotonNetwork.NickName = nickname;

        RoomInfo curRoom = null;
        printList();
        foreach (KeyValuePair<string, RoomInfo> room in roomlist)
        {
            curRoom = room.Value;
            break;
        }

        if ((bool)curRoom.CustomProperties["ispassword"])
        {
            //password panel open
            // ��� ���� �� ����
            selectRoom = curRoom;
            passwordPanel.SetActive(true);
        }
        else
        {
            // no password �ٷ� ����
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public void PasswordValidation(int pw)
    {
        // ��й�ȣ ������ ����
        if ((int)selectRoom.CustomProperties["password"] == pw)
        {
            PhotonNetwork.JoinRoom(selectRoom.Name);
        }
    }

    public void printList()
    {
        foreach (KeyValuePair<string, RoomInfo> room in roomlist)
        {
            ExitGames.Client.Photon.Hashtable has = room.Value.CustomProperties;
            string roomInfo = "room : " + room.Value.Name + " \n" + room.Value.PlayerCount + " / " + room.Value.MaxPlayers + "\n" + "isvisible : " + room.Value.IsVisible + "\n" + "isopen : " + room.Value.IsOpen
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
        foreach (RoomInfo rooom in roomList)
        {
            if (!roomlist.ContainsKey(rooom.Name))
            {
                roomlist.Add(rooom.Name, rooom);
            }
            else // ������ ����
            {
                if (rooom.PlayerCount != 0)
                    roomlist[rooom.Name] = rooom;
                else // ������ ������ �� ����
                    roomlist.Remove(rooom.Name);
            }
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
        // ���� ķ���� ����
        PhotonNetwork.LoadLevel("Room for 1");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

    }


    #endregion

    private void OnGUI()
    {
        foreach (KeyValuePair<string, RoomInfo> room in roomlist)
        {
            if (GUILayout.Button(room.Value.Name))
            {
                roomEnter(room.Value.Name);
            }
        }
    }
}
