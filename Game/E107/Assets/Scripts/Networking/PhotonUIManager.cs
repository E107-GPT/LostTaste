using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PhotonUIManager : MonoBehaviour
{
    #region private constants
    string roomName = "Room Name";
    string password;
    bool ispassword = false;
    string description;
    string message;

    public TextMeshProUGUI chatField;


    [SerializeField]
    string userName = "player";
    #endregion

    #region public ui
    

    #endregion

    #region public Methods
    public void SetDescription(string description)
    {
        this.description = description;
    }
    public string GetDescription()
    {
        return description;
    }

    public void SetPassword(string pw)
    {
        if (pw.Length <= 0) return;
        //password = int.Parse(pw);
        password = pw;
        ispassword = true;
    }
    public string GetPassword()
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
        GameObject.Find("gm").GetComponent<PhotonManager>().PasswordValidation();
    }

    public void SetChatMessage(string message)
    {
        this.message = message;
    }
    public string GetChatMessage()
    {
        string msg = message;
        //chatField.text = "";

        return msg;
    }

    public void enterRoom()
    {
        GameObject.Find("gm").GetComponent<PhotonManager>().roomEnter();
    }
    public void TestPost()
    {
        Dictionary<string, string> request = new Dictionary<string, string>();

        request.Add("accountId", "newworld");
        request.Add("password", "qwe123!@#");
        request.Add("nickname", "newworld");

        gameObject.GetComponent<HTTPRequest>().POSTCall("user", request);
    }
    public void TestGet()
    {
        gameObject.GetComponent<HTTPRequest>().GetCall("user/profile");
    }
    public void Login()
    {
        Dictionary<string, string> request = new Dictionary<string, string>();

        request.Add("accountId", "newworld");
        request.Add("password", "qwe123!@#");

        gameObject.GetComponent<HTTPRequest>().POSTCall("auth/login", request);
    }

    public void OpenPartyWindow()
    {
        GetComponent<PhotonManager>().OpenPartyWindow();
    }

    public void ServerChange(int idx)
    {
        switch (idx)
        {

            case 1:
                PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime = "3cace9da-35aa-49cd-a454-f748a53ca1ef";
                break;
            case 2:
                PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime = "91851ade-708c-4a66-8f90-5b526eba80a2";
                break;
            case 3:
                PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime = "169642ab-8e8d-42f1-bb00-ffeffe4d038c";
                break;
        }

        Debug.Log(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdRealtime);
    }

    #endregion


}
