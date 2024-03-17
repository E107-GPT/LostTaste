using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonUIManager : MonoBehaviour
{
    #region private constants
    string roomName = "Room Name";
    int password = 0;
    bool ispassword = false;

    [SerializeField]
    string userName = "player";
    #endregion

    #region public ui
    

    #endregion

    #region public Methods

    public void SetName(string uname)
    {
        userName = uname;
    }

    public string GetName()
    {
        return userName;
    }

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
        if (pw.Length <= 0) return;
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


    public void TestPost()
    {
        Dictionary<string, string> request = new Dictionary<string, string>();

        request.Add("accountId", "helloworld");
        request.Add("password", "qwe123!@#");
        request.Add("nickname", "helloworld");

        gameObject.GetComponent<HTTPRequest>().POSTCall("user", request);
    }
    public void TestGet()
    {
        gameObject.GetComponent<HTTPRequest>().GetCall("user/profile");
    }

    #endregion


}
