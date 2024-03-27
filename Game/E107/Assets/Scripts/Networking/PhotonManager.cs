using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region private fields
    string gameVersion = "2";
    private byte maxplayersPerRoom = 4;
    bool isConnecting;

    // room list -use> update, print room
    List<RoomInfo> roomlist = new List<RoomInfo>();

    // current room
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
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect();
    }
    void Start()
    {
        partyDescription = new TextMeshProUGUI[20];
        partyLeader = new TextMeshProUGUI[20];
        partyMember = new TextMeshProUGUI[20];
        roomDescription.text = UserInfo.GetInstance().getNickName() + "'s Party";

        // 20개의 파티에 대한 정보 설정
        for (int i = 0; i < 20; i++)
        {
            string partyName = "Party " + (i + 1);
            GameObject party = GameObject.Find(partyName);

            partySelectButton[i] = party;
            partyDescription[i] = party.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            partyLeader[i] = party.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            partyMember[i] = party.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            partySelectButton[i].SetActive(false);

            Button partyConnect = partySelectButton[i].GetComponent<Button>();

            // 람다식에서 사용하기 위해 현재 인덱스를 변수에 할당.
            int index = i;
            // 파티 접속 버튼에 클릭 이벤트 리스너를 추가. 클릭 시 roomEnter 함수를 호출.
            partyConnect.onClick.AddListener(()=> ClickRoom(index));
        }

        roomListPanel.SetActive(false);
    }

    #endregion

    #region public Methods
    // 네트워크 연결을 위한 Connect 함수
    public void Connect()
    {
        isConnecting = true;
        if (PhotonNetwork.IsConnected)
        {
            // 이미 연결된 경우
            Debug.Log("PhotonNetwork: 이미 연결되어 있습니다.");
        }
        else
        {
            // setting
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.NickName = UserInfo.GetInstance().getNickName();
            
            // 포톤 연결 서버를 kr로 고정하여 한국 서버에만 연결되도록 설정
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "kr";
            
            // start connect
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void ExitRoom()
    {
        // 방이 아니면 탈퇴 불가
        if (!PhotonNetwork.InRoom) return;
        // room -> Lobby
        PhotonNetwork.LeaveRoom();

        GameObject player = Managers.Resource.Instantiate("Player/Player");
        Transform pTrans = GameObject.Find("Player").GetComponent<Transform>();
        
        // Player Setting
        player.name = "Player";
        player.GetComponent<PlayerController>().Agent.Warp(pTrans.position);
        player.transform.rotation = pTrans.rotation;
        
        GameObject.Find("Main Camera").GetComponent<CameraController>()._player = player;
        roomDescription.text = UserInfo.GetInstance().getNickName() + "'s Party";
        roomListPanel.SetActive(false);
    }

    // Enter the dungeon portal without a room
    public void MakePersonalRoom()
    {
        Debug.Log("CreatePersonalRoom");
        //PhotonManager manager = GameObject.Find("gm").GetComponent<PhotonManager>();

        string roomName = UserInfo.GetInstance().getNickName() + "의 방";
        string captainName = UserInfo.GetInstance().getNickName();

        RoomOptions room = new RoomOptions();
        room.MaxPlayers = 4;
        room.IsVisible = false;
        room.IsOpen = false;

        // 시드 생성
        int seed = (int)System.DateTime.Now.Ticks;

        // set custom properties
        room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "captain", captainName }, { "seed", seed } };
        room.CustomRoomPropertiesForLobby = new string[] { "captain", "seed" };

        PhotonNetwork.NickName = UserInfo.GetInstance().getNickName();
        PhotonNetwork.CreateRoom(roomName, room);
    }

    // Make multy Rroom
    public void makeRoom()
    {
        PhotonUIManager manager = GameObject.Find("gm").GetComponent<PhotonUIManager>();
        string roomName = manager.GetDescription();
        string captainName = UserInfo.GetInstance().getNickName();
        
        if(captainName == null)
            captainName = "player";

        if (captainName == null || roomName == null) return;

        RoomOptions room = new RoomOptions();
        room.MaxPlayers = maxplayersPerRoom;
        room.IsVisible = true;
        room.IsOpen = true;

        roomDescription.text = roomName;
        bool ispassword = manager.GetIsPassword();
        string password = manager.GetPassword();

        // 시드 생성
        int seed = (int)System.DateTime.Now.Ticks;

        // Register in lobby
        if (ispassword)
        {
            room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "captain", captainName }, { "ispassword", ispassword }, { "password", password }, { "seed", seed } };
            room.CustomRoomPropertiesForLobby = new string[] { "captain", "ispassword", "password", "seed" };
        }
        else
        {
            room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "captain", captainName }, { "ispassword", ispassword }, { "seed", seed } };
            room.CustomRoomPropertiesForLobby = new string[] { "captain", "ispassword", "seed" };
        }
        roomMakePanel.SetActive(false);
        PhotonNetwork.CreateRoom(roomName, room);
    }

    public void ClickRoom(int roomNumber)
    {
        selectRoom = roomlist[roomNumber];

        if ((bool)selectRoom.CustomProperties["ispassword"])
        {
            //password panel open
            GameObject.Find("Party Joining Window").SetActive(false);
            passwordPanel.SetActive(true);
        }
    }

    public void roomEnter()
    {
        // 나중에 수정
        string nickname = "Player";//UserInfo.GetInstance().getNickName();
        
        if (nickname == null) return;

        // no password enter
        PhotonNetwork.JoinRoom(selectRoom.Name);
        GameObject.Find("Party Joining Window").SetActive(false);
    }

    public void PasswordValidation()
    {
        string pw = gameObject.GetComponent<PhotonUIManager>().GetPassword();

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
        }
    }
    #endregion

    #region MonoBehaviourPunCallbacks callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster"); 
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo rooom in roomList)
        {
            bool change = false;
            for(int i = 0; i< roomlist.Count; i++)
            {
                if(roomlist[i].Name == rooom.Name)
                {
                    if (rooom.PlayerCount != 0)
                        roomlist[i] = rooom;
                    // no player, no open, no multy
                    else if (rooom.PlayerCount == 0 || !rooom.IsOpen || !rooom.IsVisible)
                    {
                        roomlist.Remove(roomlist[i]);
                    }
                    change = true;
                }
            }

            if (!change)
            {
                if(rooom.PlayerCount != 0)
                    roomlist.Add(rooom);
            }
        }
        printList();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        isConnecting = false;

        if(roomListPanel!=null)
            roomListPanel.SetActive(false);
        Debug.LogWarningFormat("OnDisconnected {0}", cause);
    }

    public override void OnJoinedRoom()
    {
        roomListPanel.SetActive(false);
        passwordPanel.SetActive(false);
        PhotonNetwork.NickName = UserInfo.GetInstance().getNickName();
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            // my player -> spawn
            // Debug.Log($"{PhotonNetwork.PlayerList[i].IsLocal}");
            if (PhotonNetwork.PlayerList[i].IsLocal)
            {
                GameObject singlePlayer = GameObject.Find("Player");
                Vector3 position = Vector3.zero;
                Quaternion rotate = Quaternion.identity;

                if (PhotonNetwork.IsMasterClient)
                {
                    position = singlePlayer.transform.position;
                    rotate = singlePlayer.transform.rotation;
                }
                else
                {
                    GameObject spawnPoint = GameObject.Find("CampSpawn");
                    position = spawnPoint.transform.position;
                    rotate = spawnPoint.transform.rotation;
                }

                if (singlePlayer != null)
                {
                    Destroy(singlePlayer);
                }

                //Assets/Resources/Prefabs/Player/Player.prefab
                GameObject player = PhotonNetwork.Instantiate("Prefabs/Player/Player", position, rotate, 0);
                
                player.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.PlayerList[i].ActorNumber);
                HUDManager hud = GameObject.Find("HUD").GetComponent<HUDManager>();
                hud.playerController = player.GetComponent<PlayerController>();
                player.name = "Player";
                GameObject.Find("Main Camera").GetComponent<CameraController>()._player = player;

                Managers.Player.SetLocalPlayerInfo(Define.ClassType.Warrior);
                Managers.Player.LoadPlayersInfoInCurrentRoom();

            }
        }

        ExitGames.Client.Photon.Hashtable info = PhotonNetwork.CurrentRoom.CustomProperties;
        Debug.Log("seed : " + info["seed"]);
        
        Managers.Random.SetSeed((int)info["seed"]);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"JoinLobby   {PhotonNetwork.CurrentLobby}");
    }

    public override void OnLeftRoom()
    {
        Managers.Player.Clear();
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Managers.Player.AddPlayer(newPlayer);
        Managers.Player.LoadPlayersInfoInCurrentRoom();


    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        Managers.Player.LoadPlayersInfoInCurrentRoom();

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Managers.Player.RemovePlayer(otherPlayer);
        Managers.Player.LoadPlayersInfoInCurrentRoom();
        PhotonNetwork.JoinLobby();

    }




    #endregion
}
