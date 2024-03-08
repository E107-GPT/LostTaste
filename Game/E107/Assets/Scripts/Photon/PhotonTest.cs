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
            // 랜덤한 방 진입
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // 게임 버전 세팅
            PhotonNetwork.GameVersion = gameVersion;

            // 포톤 클라우드에 연결되는 시작 지점
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
        // 마스터에 들어갔을 때 랜덤 방 들어가기
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
        Debug.Log("방 들어가기 실패");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");

        PhotonNetwork.LoadLevel("Room for 1");
    }
    #endregion
}
