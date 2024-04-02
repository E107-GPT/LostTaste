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
    public GameObject partyUI;
    public GameObject passwordPanel;
    public GameObject partyListPanel;
    public GameObject partyInfoPanel;
    public GameObject partyMakePanel;
    public GameObject partyCaptain;

    public GameObject[] partySelectButton = new GameObject[20];
    public TextMeshProUGUI[] partyDescription = new TextMeshProUGUI[20];
    public TextMeshProUGUI[] partyLeader = new TextMeshProUGUI[20];
    public TextMeshProUGUI[] partyMember = new TextMeshProUGUI[20];
    
    public TextMeshProUGUI[] partyMemberInfo = new TextMeshProUGUI[4];

    public TextMeshProUGUI roomDescription;

    public GameObject JoiningWarning;
    public GameObject PartyStatusPanel;
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


        //GameObject infoPartyMemeber = GameObject.Find("Party Member Layout Group");
        //for(int i = 0; i<4; i++)
        //{
        //    string partyName = "Party Member " + (i + 1);
        //    GameObject party = GameObject.Find(partyName);
        //    Transform parentTransform = party.transform;
        //    Transform childTransform = parentTransform.Find("Party Memeber Nickname Text " +(i+1));

        //    partyMemberInfo[i] = childTransform.GetComponent<TextMeshProUGUI>();
        //}

        partyUI.SetActive(false);
        PartyStatusPanel.SetActive(true);
        PartyStatusPanel.SetActive(false);
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
        partyUI.SetActive(false);
    }

    // Enter the dungeon portal without a room
    public void MakePersonalRoom()
    {
        Debug.Log("CreatePersonalRoom");
        //PhotonManager manager = GameObject.Find("gm").GetComponent<PhotonManager>();

        string roomName = UserInfo.GetInstance().getNickName() + "의 방";
        string captainName = UserInfo.GetInstance().getNickName();

        RoomOptions room = new RoomOptions();
        room.MaxPlayers = maxplayersPerRoom;
        room.IsVisible = false;
        room.IsOpen = false;
        room.CleanupCacheOnLeave = false;

        // 시드 생성
        int seed = (int)System.DateTime.Now.Ticks;

        // set custom properties
        room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "captain", captainName }, { "seed", seed } };
        room.CustomRoomPropertiesForLobby = new string[] { "captain", "seed" };

        PhotonNetwork.NickName = UserInfo.GetInstance().getNickName();
        roomName = roomName + "`" + seed;
        PhotonNetwork.CreateRoom(roomName, room);

    }

    // Make multy Rroom
    public void makeRoom()
    {
        PhotonUIManager manager = GameObject.Find("gm").GetComponent<PhotonUIManager>();
        string roomName = manager.GetDescription();
        string captainName = UserInfo.GetInstance().getNickName();

        if (roomName.Length == 0) return;
        
        if(captainName == null)
            captainName = "player";

        if (captainName == null || roomName == null) return;

        RoomOptions room = new RoomOptions();
        room.MaxPlayers = maxplayersPerRoom;
        room.IsVisible = true;
        room.IsOpen = true;
        room.CleanupCacheOnLeave = false;

        bool ispassword = manager.GetIsPassword();
        string password = manager.GetPassword();

        // 시드 생성
        int seed = (int)System.DateTime.Now.Ticks;

        // 생성된 방 이름 + ` + 시드 값
        roomDescription.text = roomName;
        roomName  = roomName+ "`" + seed;

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
        partyMakePanel.SetActive(false);
        partyUI.SetActive(false);
        PhotonNetwork.CreateRoom(roomName, room);
    }

    public void ClickRoom(int roomNumber)
    {
        selectRoom = roomlist[roomNumber];
        string printRoomName = selectRoom.Name;
        int lastIndex = printRoomName.LastIndexOf("`");
        if (lastIndex != -1)
            printRoomName = printRoomName.Substring(0, lastIndex);
        if ((bool)selectRoom.CustomProperties["ispassword"])
        {
            passwordPanel.SetActive(true);
            Debug.Log(GameObject.Find("Party Joining PW Content Text"));
            GameObject.Find("Party Joining PW Content Text").GetComponent<TextMeshProUGUI>().text = printRoomName + "파티에 참여하시겠습니까?";
            
            //password panel open
            GameObject.Find("Party Joining Window").SetActive(false);
        }
        else
            GameObject.Find("Party Joining Content Text").GetComponent<TextMeshProUGUI>().text = printRoomName + "파티에 참여하시겠습니까?";
    }

    public void roomEnter()
    {
        // 나중에 수정
        string nickname = "Player";//UserInfo.GetInstance().getNickName();
        
        if (nickname == null) return;

        if (selectRoom.MaxPlayers <= selectRoom.PlayerCount)
        {
            JoiningWarning.SetActive(true);
            return;
        }

        // no password enter
        PhotonNetwork.JoinRoom(selectRoom.Name);
        GameObject.Find("Party Joining Window").SetActive(false);
        partyUI.SetActive(false);
    }

    public void PasswordValidation()
    {
        string pw = gameObject.GetComponent<PhotonUIManager>().GetPassword();

        if ((string)selectRoom.CustomProperties["password"] == pw)
        {
            PhotonNetwork.JoinRoom(selectRoom.Name);
            partyUI.SetActive(false);
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


            // 마지막 ` 이후 시드값을 제외하고 출력
            string printRoomName = room.Name;
            int lastIndex = printRoomName.LastIndexOf("`");
            if (lastIndex != -1)
                printRoomName = printRoomName.Substring(0, lastIndex);


            partyDescription[idx].text = printRoomName;
            partyLeader[idx].text = (string)has["captain"];
            partyMember[idx].text = room.PlayerCount + " / 4";

            idx ++;

            //string roomInfo = "room : " + room.Value.Name + " \n" + room.Value.PlayerCount + " / " + room.Value.MaxPlayers + "\n" + "isvisible : " + room.Value.IsVisible + "\n" + "isopen : " + room.Value.IsOpen
            //    + "\n captain : " + has["captain"] + "\n" + has["ispassword"] + " / " + has["password"];
        }
    }

    public void OpenPartyWindow()
    {
        partyUI.SetActive(true);
        if (PhotonNetwork.InRoom)
        {
            PrintPlayer();
            partyInfoPanel.SetActive(true);
            partyListPanel.SetActive(false);
        }
        else
        {
            partyInfoPanel.SetActive(false);
            partyListPanel.SetActive(true);
        }
    }

    public void PrintPlayer()
    {
        for(int i = 0; i<4; i++)
        {
            Debug.Log(partyMemberInfo[i]);
            partyMemberInfo[i].text = "파티원 모집중...";
        }
        int idx = 0;
        // 이거 체크해보기
        //Debug.Log(GameObject.Find("Party Leader Nickname"));
        //GameObject.Find("Party Leader Nickname").GetComponent<TextMeshProUGUI>().text = (string)PhotonNetwork.CurrentRoom.CustomProperties["captain"];

        partyCaptain.GetComponent<TextMeshProUGUI>().text = (string)PhotonNetwork.MasterClient.NickName;

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(player.Key);
            Debug.Log((int)player.Value.CustomProperties["Number"]);
            partyMemberInfo[(int)player.Value.CustomProperties["Number"]].text = player.Value.NickName;
            //idx++;
        }
    }

    public int GetCurrentPartyMemberCount()
    {
        int idx = 0;
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            if (player.Value.CustomProperties.ContainsKey("Number"))
            {
                idx++;
            }
        }
        return idx;
    }

    public void PrintPartyStatus()
    {
        Debug.Log("Party Status Update");
        PartyStatusPanel.SetActive(true);
        GameObject[] playerStatus = new GameObject[4];

        for(int i = 0; i<4; i++)
        {
            playerStatus[i] = PartyStatusPanel.transform.Find("Party Member " + (i + 1)).gameObject;
            playerStatus[i].SetActive(false);
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log(player.Value.CustomProperties);
            Debug.Log(player.Value.CustomProperties["Number"]);
            int index = player.Value.CustomProperties.ContainsKey("Number") ? (int)player.Value.CustomProperties["Number"] : 0;
            playerStatus[index].SetActive(true);
            playerStatus[index].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = player.Value.NickName;
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

        if(partyUI!=null)
            partyUI.SetActive(false);
        Debug.LogWarningFormat("OnDisconnected {0}", cause);
    }

    public override void OnJoinedRoom()
    {
        partyListPanel.SetActive(false);
        passwordPanel.SetActive(false);
        PhotonNetwork.NickName = UserInfo.GetInstance().getNickName();
        string printRoomName = PhotonNetwork.CurrentRoom.Name;
        int lastIndex = printRoomName.LastIndexOf("`");
        if (lastIndex != -1)
            printRoomName = printRoomName.Substring(0, lastIndex);

        roomDescription.text = printRoomName;

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

    
                Managers.Player.SetLocalPlayerInfo(player.GetComponent<PhotonView>().ViewID, Define.ClassType.None);
                Managers.Player.LoadPlayersInfoInCurrentRoom();

                PrintPartyStatus();
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
        
        Debug.Log("방에서 나갔습니다.");
        PlayerController[] list = GameObject.FindObjectsOfType<PlayerController>();

        //object viewIDObj;
        //PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("ViewID", out viewIDObj);

        //foreach (PlayerController p in list)
        //{

        //    if (p.GetComponent<PhotonView>().ViewID != (int)viewIDObj)
        //    {
        //        PhotonNetwork.Destroy(p.gameObject);
        //    }
        //}
        Managers.Scene.LoadScene(Define.Scene.Dungeon);

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
        PrintPartyStatus();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Managers.Player.RemovePlayer(otherPlayer);
        Managers.Player.LoadPlayersInfoInCurrentRoom();

        PlayerController[] list = GameObject.FindObjectsOfType<PlayerController>();
        object viewIDObj;
        otherPlayer.CustomProperties.TryGetValue("ViewID", out viewIDObj);

        foreach (PlayerController p in list)
        {
            
            if (p.GetComponent<PhotonView>().ViewID == (int)viewIDObj)
            {
                // 마스터가 지워라.
                if(PhotonNetwork.IsMasterClient) PhotonNetwork.Destroy(p.gameObject);
            }
        }

        PrintPartyStatus();
        Debug.Log("anyone left");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(PhotonNetwork.LocalPlayer == newMasterClient)
        {
            MonsterManager.Instance.ReStartManage();
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode + " : " + message);
        // 파티 다 찼을 때 경고 줘야함
        //JoiningWarning.SetActive(true);
    }

    







    #endregion

    // 마스터 클라이언트가 변경되었을 때 호출되는 메소드
}
