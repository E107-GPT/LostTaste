using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region private serializable fields
    [Tooltip("방 최대 입장 수")]
    [SerializeField]
    private byte maxplayersPerRoom = 4;


    #endregion

    #region private fields
    // 클라이언트 번호
    string gameVersion = "1";
    bool isConnecting;

    // 방 이름으로 룸 정보 관리
    // 중복 불가
    Dictionary<string, RoomInfo> roomlist = new Dictionary<string, RoomInfo>();

    // 선택한 방 정보
    private RoomInfo selectRoom;
    #endregion

    #region public fields


    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        // 마스터가 장면을 로드하면 같은 방에 있는 모든 클라이언트가 자동으로 레벨을 동기화하도록 함
        PhotonNetwork.AutomaticallySyncScene = true;
        // 같은 버전의 유저들만 접속 허용
    }
    void Start()
    {
        
    }
    #endregion

    #region public Methods
    // Connect 연결 과정을 시작하는 메소드
    public void Connect()
    {
        isConnecting = true;
        //progressLabel.SetActive(true);
        //controlPanel.SetActive(false);
        Debug.Log("sssssss" + PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime);
        Debug.Log(PhotonNetwork.IsConnected);


        // 포톤 네트워크 연결 여부 확인
        if (PhotonNetwork.IsConnected)
        {
            // 리스트 출력하기
            //progressLabel.SetActive(false);
            //controlPanel.SetActive(true);
            //createRoomPanel.SetActive(true);
            Debug.Log("로빙");
            PhotonNetwork.JoinLobby();
            PhotonNetwork.JoinRandomOrCreateRoom();
        }
        else
        {
            // 게임 버전 세팅
            Debug.Log("Connect?  " + PhotonNetwork.IsConnected);
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.NickName = UserInfo.GetInstance().getId();
            Debug.Log(PhotonNetwork.NickName);
            // 포톤 클라우드에 연결되는 시작 지점
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void DisconnectFromPhoton()
    {
        Transform pTrans = GameObject.Find("Player").GetComponent<Transform>();
        GameObject.Find("Player").GetComponent<ObjectPersist>().DestroyPlayer();
        GameObject player = Resources.Load<GameObject>("Player");
        player = Instantiate(player);
        player.name = "Player";
        player.GetComponent<ObjectPersist>().DestroyPlayer();
        player.GetComponent<ObjectPersist>().objectType = ObjectPersist.ObjectType.player;
        player.GetComponent<ObjectPersist>().Init();
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
        Debug.Log("JoinLobby");
        //progressLabel.SetActive(false);
        //createRoomPanel.SetActive(true);
    }


    //public void makeRoom()
    //{
    //    PhotonUIManager manager = GameObject.Find("GameManager").GetComponent<PhotonUIManager>();
    //    string roomName = manager.GetTitle();
    //    string captainName = manager.GetName();

    //    if (captainName == null || roomName == null) return;


    //    RoomOptions room = new RoomOptions();
    //    room.MaxPlayers = maxplayersPerRoom;
    //    room.IsVisible = true;
    //    room.IsOpen = true;
    //    PhotonNetwork.NickName = manager.GetName();
    //    bool ispassword = manager.GetIsPassword();
    //    int password = manager.GetPassword();
    //    Debug.Log("pw" + password);
    //    if (ispassword)
    //    {
    //        room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "captain", captainName }, { "ispassword", ispassword }, { "password", password } };
    //        room.CustomRoomPropertiesForLobby = new string[] { "captain", "ispassword", "password" };
    //    }
    //    else
    //    {
    //        room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "captain", captainName }, { "ispassword", ispassword } };
    //        room.CustomRoomPropertiesForLobby = new string[] { "captain", "ispassword" };
    //    }

    //    Debug.Log("pw" + (bool)room.CustomRoomProperties["ispassword"]);
    //    PhotonNetwork.CreateRoom(roomName, room);
    //}

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
            // 비번 검증 후 입장
            selectRoom = curRoom;
            //passwordPanel.SetActive(true);
        }
        else
        {
            // no password 바로 입장
            PhotonNetwork.JoinRoom(roomName);
        }
    }

    public void PasswordValidation(int pw)
    {
        // 비밀번호 맞으면 입장
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
            // 마스터에 들어갔을 때 랜덤 방 들어가기
            //PhotonNetwork.JoinLobby();

            PhotonNetwork.JoinRandomOrCreateRoom();
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
            else // 있으면 갱신
            {
                if (rooom.PlayerCount != 0)
                    roomlist[rooom.Name] = rooom;
                else // 유저가 없으면 방 삭제
                    roomlist.Remove(rooom.Name);
            }
        }
        printList();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        isConnecting = false;

        Debug.LogWarningFormat("OnDisconnected {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방 들어가기 실패");
    }

    public override void OnCreatedRoom()
    {
        // 본인 캠프로 들어가기
        //PhotonNetwork.LoadLevel("Room for 1");
    }

    public override void OnJoinedRoom()
    {
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
            //현재 유저가 아니면 소환

            if (PhotonNetwork.PlayerList[i].IsLocal)
            {
                GameObject singlePlayer = GameObject.Find("Player");
                if(singlePlayer != null)
                {
                    singlePlayer.GetComponent<ObjectPersist>().DestroyPlayer();
                    Destroy(singlePlayer);
                }
                Debug.Log("두두등장");
                Transform spawnpt = GameObject.Find("CampSpawn").GetComponent<Transform>();
                Debug.Log(i + " : " + PhotonNetwork.PlayerList[i].NickName);
                GameObject player2 = PhotonNetwork.Instantiate("Player", spawnpt.position, spawnpt.rotation, 0);
                Debug.Log(player2);
                player2.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.PlayerList[i].ActorNumber);
                player2.name = "Player";
                player2.GetComponent<ObjectPersist>().objectType = ObjectPersist.ObjectType.player;
                player2.GetComponent<ObjectPersist>().Init();
                player2.GetComponent<PlayerController>().Init();
                GameObject.Find("MainCamera").GetComponent<CameraController>()._player = player2;
            }
        }
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
