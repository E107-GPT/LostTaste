using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ModifyRoomSettings : MonoBehaviourPunCallbacks
{
    // 포탈 타면 룸 생성 혹은 생성된 룸 커스텀

    bool makeRoom = false;
    public void MakePersonalRoom()
    {
        Debug.Log("CreateRoom");
        PhotonManager manager = GameObject.Find("gm").GetComponent<PhotonManager>();

        string roomName = UserInfo.GetInstance().getNickName() + "의 방";
        string captainName = UserInfo.GetInstance().getNickName();
        Debug.Log(captainName + " + " + roomName);

        if (captainName == null || roomName == null) return;


        RoomOptions room = new RoomOptions();
        room.MaxPlayers = 4;
        room.IsVisible = false;
        room.IsOpen = false;
        PhotonNetwork.NickName = UserInfo.GetInstance().getNickName();
        Debug.Log(room);

        room.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "captain", captainName }, { "ispassword", false } };
        room.CustomRoomPropertiesForLobby = new string[] { "captain", "ispassword" };
        
        bool wht = PhotonNetwork.CreateRoom(roomName, room);

        Debug.Log(wht);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Join Room");
        PhotonNetwork.NickName = UserInfo.GetInstance().getNickName();
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


                GameObject player2 = PhotonNetwork.Instantiate("Player", position, rotate, 0);
                //Assets/Resources/Prefabs/Player/Player.prefab
                Debug.Log(player2);
                player2.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.PlayerList[i].ActorNumber);
                HUDManager hud = GameObject.Find("HUD").GetComponent<HUDManager>();
                hud.playerController = player2.GetComponent<PlayerController>();
                player2.name = "Player";
                GameObject.Find("Main Camera").GetComponent<CameraController>()._player = player2;
            }
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("만들어지");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!makeRoom)
        {
            makeRoom = true;

            if (other.gameObject.CompareTag("Player"))
            {

                if(!PhotonNetwork.InRoom)
                    MakePersonalRoom();
                else
                {
                    PhotonNetwork.CurrentRoom.IsVisible = false;
                    PhotonNetwork.CurrentRoom.IsOpen = false;
                }

                Debug.Log(PhotonNetwork.CurrentRoom);
            }
        }
    }
}
