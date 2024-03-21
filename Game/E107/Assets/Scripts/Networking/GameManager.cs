using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    #region Photon Callbacks
    public override void OnLeftRoom()
    {
        // �κ�� �̵�
        PhotonNetwork.JoinLobby();
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("OnPlayerEnteredRoom : {0}", other.NickName);
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("IsMasterClient : {0}", PhotonNetwork.IsMasterClient);
            LoadArena();
        }
    }

    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom : {0}", other.NickName);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.LogFormat("IsMasterClient : {0}", PhotonNetwork.IsMasterClient);
            LoadArena();
        }   
    }

    public override void OnJoinedRoom()
    {
        Transform spawnpt = GameObject.Find("CampSpawn").transform;

        PhotonNetwork.Instantiate("", spawnpt.position, spawnpt.rotation, 0);
    }
    #endregion

    #region public method
    public void LeaveRoom()
    {
        Debug.Log("������");
        PhotonNetwork.LeaveRoom();
    }
    #endregion

    #region private methods
    void LoadArena()
    {
        // ������ Ŭ���̾�Ʈ�� ��쿡�� ȣ��
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.Log("PhotonNetwork: ������ �ε��Ϸ��� ������ ������ Ŭ���̾�Ʈ�� �ƴմϴ�.");
            return;
        }

        Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);

        // ���ϴ� ���� ȣ��
        //PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
    }
    #endregion

    private void Start()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name);

        if (!PhotonNetwork.IsMasterClient) PhotonNetwork.AutomaticallySyncScene = true;
        //PhotonNetwork.CurrentRoom.SetCustomProperties(PhotonNetwork.CurrentRoom.CustomProperties);

        Debug.Log(PhotonNetwork.CurrentRoom.Players.Keys);

        List<Player> player = new List<Player>();
        foreach(Player playerId in PhotonNetwork.CurrentRoom.Players.Values)
        {
            player.Add(playerId);
            Debug.Log(playerId.ToString());
        }

        for(int i = 0; i<PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            string name = player[i].NickName;
            if (player[i].IsLocal) name += " its me!";
            Debug.Log(name);
        }
    }
}
