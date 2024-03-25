using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ModifyRoomSettings : MonoBehaviourPunCallbacks
{
    // 포탈 타면 룸 생성 혹은 생성된 룸 커스텀

    bool makeRoom = false;
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

                if (!PhotonNetwork.InRoom)
                {
                    GameObject.Find("gm").GetComponent<PhotonManager>().MakePersonalRoom();
                }
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
