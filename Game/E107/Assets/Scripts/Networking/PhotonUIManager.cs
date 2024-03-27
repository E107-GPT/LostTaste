using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        GameObject.Find("GameManager").GetComponent<PhotonManager>().PasswordValidation();
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

    #endregion


}
