using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region private constants
    string roomName = "Room Name";
    int password = 0;
    bool ispassword = false;
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

    public void SetPassword(string pw)
    {
        password = int.Parse(pw);
        ispassword = true;
    }
    public int GetPassword()
    {
        return password;
    }
    public bool GetIsPassword()
    {
        return ispassword;
    }

    public void SetEnterPassword()
    {
        Debug.Log((password));
        GameObject.Find("Launcher").GetComponent<PhotonTest>().PasswordValidation(password);
    }
    #endregion
}
