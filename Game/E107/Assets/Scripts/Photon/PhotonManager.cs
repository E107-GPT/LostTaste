using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region private serializable fields
    #endregion


    #region private fields
    string gameVersion = "1";
    private byte maxPlayersInRoom = 4;
    bool isConnect;
    HashSet<RoomInfo> roomList = new HashSet<RoomInfo>();

    
    #endregion
}
