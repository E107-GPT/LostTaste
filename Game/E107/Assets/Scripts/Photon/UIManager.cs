using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region private constants
    string roomName = "Room Name";
    #endregion
    int password = 0;
    bool ispassword = false;

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

    public void SetPassword(string pw)
    {
        password = int.Parse(pw);
        ispassword = true;
        Debug.Log(pw + " : " + password);
    }
    public int GetPassword()
    {
        return password;
    }
    public bool GetIsPassword()
    {
        return ispassword;
    }
    #endregion
}
