using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region private constants
    string roomName = "Room Name";
    #endregion


    #region public ui
    

    #endregion

    #region public Methods
    public void SetTitle(string newRoomName)
    {
        roomName = newRoomName;
        Debug.Log(roomName);
    }

    public string GetTitle()
    {
        return roomName;
    }
    #endregion
}
