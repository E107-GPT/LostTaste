using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonTest : MonoBehaviourPunCallbacks
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
    List<RoomInfo> roomlist = new List<RoomInfo>();
    // 선택한 방 정보
    private RoomInfo selectRoom;
    #endregion

    #region public fields
    [Tooltip("판넬은 이름과 버튼 가짐")]
    [SerializeField]
    public  GameObject controlPanel;
    
    [Tooltip("판넬은 이름과 버튼 가짐")]
    [SerializeField]
    public  GameObject progressLabel;

    [Tooltip("판넬은 이름과 버튼 가짐")]
    [SerializeField]
    public GameObject createRoomPanel;
    
    
    [Tooltip("판넬은 이름과 버튼 가짐")]
    [SerializeField]
    public GameObject passwordPanel;


    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        // 마스터가 장면을 로드하면 같은 방에 있는 모든 클라이언트가 자동으로 레벨을 동기화하도록 함
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
    // Connect 연결 과정을 시작하는 메소드
    public void Connect()
    {
        isConnecting = true;
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        // 포톤 네트워크 연결 여부 확인
        if (PhotonNetwork.IsConnected)
        {
            // 리스트 출력하기
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
            createRoomPanel.SetActive(true);
            Debug.Log("로빙");
        }
        else
        {
            // 게임 버전 세팅
            PhotonNetwork.GameVersion = gameVersion;

            // 포톤 클라우드에 연결되는 시작 지점
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
            // 비번 검증 후 입장
            selectRoom = room;
            passwordPanel.SetActive(true);
        }
        else
        {
            // no password 바로 입장
            PhotonNetwork.JoinRoom(room.Name);
        }  
    }

    public void PasswordValidation(int pw)
    {
        // 비밀번호 맞으면 입장
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
            // 마스터에 들어갔을 때 랜덤 방 들어가기
            
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
        Debug.Log("방 들어가기 실패");
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
