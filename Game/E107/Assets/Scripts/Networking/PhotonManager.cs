using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region private serializable fields
    [Tooltip("�� �ִ� ���� ��")]
    [SerializeField]
    private byte maxplayersPerRoom = 4;


    #endregion

    #region private fields
    // Ŭ���̾�Ʈ ��ȣ
    string gameVersion = "2";
    bool isConnecting;

    // �� �̸����� �� ���� ����
    // �ߺ� �Ұ�
    List<RoomInfo> roomlist = new List<RoomInfo>();

    // ������ �� ����
    private RoomInfo selectRoom;
    #endregion

    #region public fields
    public GameObject passwordPanel;
    public GameObject roomListPanel;
    public GameObject roomMakePanel;

    public GameObject[] partySelectButton = new GameObject[20];
    public TextMeshProUGUI[] partyDescription = new TextMeshProUGUI[20];
    public TextMeshProUGUI[] partyLeader = new TextMeshProUGUI[20];
    public TextMeshProUGUI[] partyMember = new TextMeshProUGUI[20];

    public TextMeshProUGUI roomDescription;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        // �����Ͱ� ����� �ε��ϸ� ���� �濡 �ִ� ��� Ŭ���̾�Ʈ�� �ڵ����� ������ ����ȭ�ϵ��� ��
        PhotonNetwork.AutomaticallySyncScene = true;
        // ���� ������ �����鸸 ���� ���
    }
    void Start()
    {
        partyDescription = new TextMeshProUGUI[20];
        partyLeader = new TextMeshProUGUI[20];
        partyMember = new TextMeshProUGUI[20];
        for (int i = 0; i<20; i++)
        {
            string partyName = "Party " + (i+1);
            GameObject party = GameObject.Find(partyName);

            partySelectButton[i] = party;
            partyDescription[i] = party.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            partyLeader[i] = party.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            partyMember[i] = party.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            partySelectButton[i].SetActive(false);
            //Debug.Log($"???? : {i}, {partyName}, {party}");
            Button partyConnect = party.GetComponent<Button>();
            int index = i;
            partyConnect.onClick.AddListener(() => roomEnter(index) );
        }

        roomListPanel.SetActive(false);
    }
    #endregion

    #region public Methods
    // Connect ���� ������ �����ϴ� �޼ҵ�
    public void Connect()
    {
        isConnecting = true;
        //progressLabel.SetActive(true);
        //controlPanel.SetActive(false);
        Debug.Log("sssssss" + PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime);
        Debug.Log(PhotonNetwork.IsConnected);


        // ���� ��Ʈ��ũ ���� ���� Ȯ��
        if (PhotonNetwork.IsConnected)
        {
            // ����Ʈ ����ϱ�
            Debug.Log(PhotonNetwork.CountOfRooms);
            Debug.Log(roomlist.ToArray().ToString());
        }
        else
        {
            // ���� ���� ����
            Debug.Log("Connect?  " + PhotonNetwork.IsConnected);
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.NickName = UserInfo.GetInstance().getId();
            Debug.Log(PhotonNetwork.NickName);
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "kr";
            // ���� Ŭ���忡 ����Ǵ� ���� ����
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void DisconnectFromPhoton()
    {
        Transform pTrans = GameObject.Find("Player").GetComponent<Transform>();
        GameObject player = Resources.Load<GameObject>("Player");
        player = Managers.Resource.Instantiate("Player/Player");
        player.name = "Player";
        player.GetComponent<PlayerController>().Init();
        player.transform.position = pTrans.position;
        player.transform.rotation = pTrans.rotation;

        PhotonNetwork.Disconnect();
    }
    public void LoadMasterScene()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"JoinLobby   {PhotonNetwork.CurrentLobby}");
        
        //progressLabel.SetActive(false);
        //createRoomPanel.SetActive(true);
    }


    public void makeRoom()
    {
        Debug.Log("makeroom");
        PhotonUIManager manager = GameObject.Find("gm").GetComponent<PhotonUIManager>();
        string roomName = manager.GetDescription();
        string captainName = "player";//UserInfo.GetInstance().getNickName();
        Debug.Log(captainName + " + " + roomName);

        if (captainName == null || roomName == null) return;


        RoomOptions room = new RoomOptions();
        room.MaxPlayers = maxplayersPerRoom;
        room.IsVisible = true;
        room.IsOpen = true;
        roomDescription.text = roomName;
        PhotonNetwork.NickName = manager.GetName();
        bool ispassword = manager.GetIsPassword();
        string password = manager.GetPassword();
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
        Debug.Log(room);
        
        roomMakePanel.SetActive(false);
        PhotonNetwork.CreateRoom(roomName, room);
        
        
    }

    public void roomEnter(int roomNumber)
    {
        Debug.Log($"Room Num : {roomNumber}");
        if (roomlist.Count < 1) return;
        string nickname = GameObject.Find("gm").GetComponent<PhotonUIManager>().GetName();
        if (nickname == null) return;
        PhotonNetwork.NickName = nickname;
        
        RoomInfo curRoom = roomlist[roomNumber];
        printList();

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
            PhotonNetwork.JoinRoom(roomlist[roomNumber].Name);

        }
    }

    public void PasswordValidation()
    {
        string pw = gameObject.GetComponent<PhotonUIManager>().GetPassword();
        // ��й�ȣ ������ ����
        if ((string)selectRoom.CustomProperties["password"] == pw)
        {
            PhotonNetwork.JoinRoom(selectRoom.Name);
        }
    }

    public void printList()
    {

        for(int i = 0; i<20; i++)
        {
            partySelectButton[i].SetActive(false);
        }

        int idx = 0;
        Debug.Log(roomlist.ToArray());
        foreach (RoomInfo room in roomlist)
        {
            partySelectButton[idx].SetActive(true);

            ExitGames.Client.Photon.Hashtable has = room.CustomProperties;
            partyDescription[idx].text = room.Name;
            partyLeader[idx].text = (string)has["captain"];
            partyMember[idx].text = room.PlayerCount + " / 4";

            idx ++;


            //string roomInfo = "room : " + room.Value.Name + " \n" + room.Value.PlayerCount + " / " + room.Value.MaxPlayers + "\n" + "isvisible : " + room.Value.IsVisible + "\n" + "isopen : " + room.Value.IsOpen
            //    + "\n captain : " + has["captain"] + "\n" + has["ispassword"] + " / " + has["password"];
            Debug.Log(room);

        }
    }
    #endregion

    #region MonoBehaviourPunCallbacks callbacks
    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            Debug.Log("OnConnectedToMaster");
            PhotonNetwork.JoinLobby();
            
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log(roomList);
        foreach (RoomInfo rooom in roomList)
        {
            Debug.Log(rooom);
            bool change = false;
            for(int i = 0; i< roomlist.Count; i++)
            {
                if(roomlist[i].Name == rooom.Name)
                {
                    if (rooom.PlayerCount != 0)
                        roomlist[i] = rooom;
                    else // ������ ������ �� ����
                        roomlist.Remove(roomlist[i]);
                    change = true;
                }
                
            }

            if (!change)
            {
                Debug.Log(rooom);
                // ���ο� �� �߰�
                roomlist.Add(rooom);
                //for(int i = 0; i<roomlist.Count; i++)
                //{
                //    if (roomlist[i] == null)
                //    {
                //        //roomlist[i] = rooom;
                //    }
                //}
            }
        }
        printList();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        isConnecting = false;

        roomListPanel.SetActive(false);
        Debug.LogWarningFormat("OnDisconnected {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("�� ���� ����");
    }

    public override void OnCreatedRoom()
    {
        // ���� ���������

    }

    public override void OnJoinedRoom()
    {
        roomListPanel.SetActive(false);
        passwordPanel.SetActive(false);
        PhotonNetwork.NickName = UserInfo.GetInstance().getNickName();
        List<Player> player = new List<Player>();
        foreach (KeyValuePair<int,  Player > playerId in PhotonNetwork.CurrentRoom.Players)
        {            
            player.Add(playerId.Value);
            Debug.Log(playerId.Key);
        }
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);

        
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            //���� ������ �ƴϸ� ��ȯ
            Debug.Log($"{PhotonNetwork.PlayerList[i].IsLocal}");
            if (PhotonNetwork.PlayerList[i].IsLocal)
            {
                Debug.Log($"{PhotonNetwork.PlayerList[i].ActorNumber}");
                GameObject singlePlayer = GameObject.Find("Player");
                Vector3 position = Vector3.zero;
                Quaternion rotate = Quaternion.identity;

                //if (PhotonNetwork.IsMasterClient)
                //{
                //    position = singlePlayer.transform.position;
                //    rotate = singlePlayer.transform.rotation;
                //}
                //else
                //{
                GameObject spawnPoint = GameObject.Find("CampSpawn");
                position = spawnPoint.transform.position;
                rotate = spawnPoint.transform.rotation;
                //}

                if (singlePlayer != null)
                {
                    Destroy(singlePlayer);
                }


                GameObject player2 = PhotonNetwork.Instantiate("Player", position, rotate, 0);

                
                player2.name = "MyCharacter";
                Debug.Log(player2);
                player2.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.PlayerList[i].ActorNumber);
                //player2.GetCo   photonView = GetComponent<PhotonView>();

                GameObject.Find("Main Camera").GetComponent<CameraController>()._player = player2;
            }
        }
    }
    

    #endregion

}
