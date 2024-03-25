using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ModifyRoomSettings : MonoBehaviourPunCallbacks
{
    // Æ÷Å» Å¸¸é ·ë »ý¼º È¤Àº »ý¼ºµÈ ·ë Ä¿½ºÅÒ

    bool makeRoom = false;
    public void MakePersonalRoom()
    {
        Debug.Log("CreateRoom");
        PhotonManager manager = GameObject.Find("gm").GetComponent<PhotonManager>();

        Debug.Log("makeroom");
        string roomName = UserInfo.GetInstance().getNickName() + "ÀÇ ¹æ";
        string captainName = UserInfo.GetInstance().getNickName();
        Debug.Log(captainName + " + " + roomName);

        if (captainName == null || roomName == null) return;


        RoomOptions room = new RoomOptions();
        room.MaxPlayers = 4;
        room.IsVisible = false;
        room.IsOpen = false;
        PhotonNetwork.NickName = UserInfo.GetInstance().getNickName();
        Debug.Log(room);

        PhotonNetwork.CreateRoom(roomName, room);
    }

    public override void OnJoinedRoom()
    {
    //    GameObject singlePlayer = GameObject.Find("Player");
    //    Vector3 position = Vector3.zero;
    //    Quaternion rotate = Quaternion.identity;

    //    if (singlePlayer != null)
    //    {
    //        position = singlePlayer.transform.position;
    //        rotate = singlePlayer.transform.rotation;

    //        Destroy(singlePlayer);
    //    }


    //    GameObject player2 = PhotonNetwork.Instantiate("Prefabs/Player/Player", position, rotate, 0);
    //    //Assets/Resources/Prefabs/Player/Player.prefab
    //    Debug.Log(player2);
    //    player2.name = "Player";
    //    HUDManager hud = GameObject.Find("HUD").GetComponent<HUDManager>();
    //    hud.playerController = player2.GetComponent<PlayerController>();
    //    GameObject.Find("Main Camera").GetComponent<CameraController>()._player = player2;

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
