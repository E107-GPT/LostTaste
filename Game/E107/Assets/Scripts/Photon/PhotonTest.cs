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

            //PhotonNetwork.JoinRandomRoom();
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
            // 마스터에 들어갔을 때 랜덤 방 들어가기
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
        Debug.Log("방 들어가기 실패");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        PhotonNetwork.LoadLevel("Room for 1");
    }
    #endregion
}
