using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    // �α��� �Է� �ʵ�
    public TMP_InputField loginInputID; // �α��� ���̵� �Է� �ʵ�
    public TMP_InputField loginInputPW; // �α��� ��й�ȣ �Է� �ʵ�

    // ȸ������ �Է� �ʵ�
    public TMP_InputField signUpInputID; // ȸ������ ���̵� �Է� �ʵ�
    public TMP_InputField signUpInputNickname; // ȸ������ �г��� �Է� �ʵ�
    public TMP_InputField signUpInputPW; // ȸ������ ��й�ȣ �Է� �ʵ�
    public TMP_InputField signUpInputPWConfirm; // ȸ������ ��й�ȣ Ȯ�� �Է� �ʵ�

    // ��ư
    public Button loginButton; // �α��� ��ư
    public Button signUpButton; // ȸ������ ��ư
    public Button showLoginButton; // �α��� â ǥ�� ��ư
    public Button showSignUpButton; // ȸ������ â ǥ�� ��ư

    // �г�
    public GameObject popupWindowPanel; // �˾� â �г�
    public GameObject loginPanel; // �α��� �г�
    public GameObject signUpPanel; // ȸ������ �г�
    public GameObject connectingPanel; // Ŀ��Ʈ �г�

    // ��� ����
    public TextMeshProUGUI warningText; // ��� �ؽ�Ʈ

    // ����ڰ� �α����� �õ��� �� �߻��ϴ� �̺�Ʈ
    public event Action<string, string> onClickLogin;

    HTTPRequest request;

    /// <summary>
    /// ------------------------------- ��ũ��Ʈ�� Ȱ��ȭ�Ǿ��� �� ȣ�� -------------------------------
    /// </summary>
    private void Awake()
    {
        // �α��� ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        if (loginButton != null)
            this.loginButton.onClick.AddListener(HandleLoginButtonClick);

        // ȸ������ ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        if (signUpButton != null)
            this.signUpButton.onClick.AddListener(HandleSignUpButtonClick);

        // �α��� â ǥ�� ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        if (showLoginButton != null)
            this.showLoginButton.onClick.AddListener(ShowLoginPanel);

        // ȸ������ â ǥ�� ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        if (showSignUpButton != null)
            this.showSignUpButton.onClick.AddListener(ShowSignupPanel);

        // connecting �г��� Ȱ��ȭ ������ ���, �ش� �г��� ��Ȱ��ȭ �ϰ� �˾� â�� Ȱ��ȭ
        if (connectingPanel != null)
            connectingPanel.SetActive(false);

        // ���� �Ŵ������� HTTPRequest ������Ʈ�� ã�� ������
        request = GameObject.Find("GameManager").GetComponent<HTTPRequest>();        
    }

    /// <summary>
    /// ------------------------------- �α��� ��ư Ŭ�� �� ȣ��Ǵ� �Լ� -------------------------------
    /// </summary>
    public void HandleLoginButtonClick()
    {
        // �Էµ� ���̵�� ��й�ȣ�� ������
        string id = loginInputID.text;
        string pw = loginInputPW.text;
        Debug.Log(id);
        Debug.Log(pw);

        // �α��� ������ ����� �α׷� ���
        // Debug.LogFormat("�α��� ����: id: {0}, pw={1}", id, pw);

        // ������ �α��� ��û�� ����
        Dictionary<string, string> requestParam = new Dictionary<string, string>();
        requestParam.Add("accountId", id);
        requestParam.Add("password", pw);
        request.POSTCall("auth/login", requestParam);
    }

    /// <summary>
    /// ------------------------------- ȸ������ ��ư Ŭ�� �� ȣ��Ǵ� �Լ� -------------------------------
    /// </summary>
    public void HandleSignUpButtonClick()
    {
        // �Էµ� ���̵�, �г���, ��й�ȣ, ��й�ȣ Ȯ���� ������
        string id = signUpInputID.text;
        string nickname = signUpInputNickname.text;
        string pw = signUpInputPW.text;
        string pwConfirm = signUpInputPWConfirm.text;

        // ��й�ȣ�� ��й�ȣ Ȯ���� ��ġ���� �ʴ� ���
        if (pw != pwConfirm)
        {
            warningText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            warningText.gameObject.SetActive(true); // ��� ���� Ȱ��ȭ
            return;
        }

        // ȸ������ ������ ����� �α׷� ���
        // Debug.LogFormat("ȸ������ ����: id={0}, nickname={1}, pw={2}", id, nickname, pw);

        // ȸ������ ������ ������ ����
        Dictionary<string, string> requestParam = new Dictionary<string, string>();
        requestParam.Add("accountId", id);
        requestParam.Add("password", pw);
        requestParam.Add("nickname", nickname);
        request.POSTCall("user", requestParam);
    }

    /// <summary>
    /// �α��� ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
    public void LoginFailure()
    {
        warningText.text = "���̵�� ��й�ȣ�� Ȯ�����ּ���.";
        warningText.gameObject.SetActive(true); // ��� ���� Ȱ��ȭ
        return;
    }

    /// <summary>
    /// ȸ������ ���� �� ȣ��Ǵ� �Լ�
    /// </summary>
    public void SignupFailure()
    {
        warningText.text = "ȸ�����Կ� �����߽��ϴ�.";
        warningText.gameObject.SetActive(true); // ��� ���� Ȱ��ȭ
        return;
    }

    /// <summary>
    /// �α��� �г��� Ȱ��ȭ�ϰ� ȸ������ �г��� ��Ȱ��ȭ�ϴ� �Լ�
    /// </summary>
    public void ShowLoginPanel()
    {
        // �Է� �ʵ� �ʱ�ȭ
        loginInputID.text = "";
        loginInputPW.text = "";

        // �г� ����
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);

        // ��� ���� ��Ȱ��ȭ
        warningText.gameObject.SetActive(false);
    }

    /// <summary>
    /// ȸ������ �г��� Ȱ��ȭ�ϰ� �α��� �г��� ��Ȱ��ȭ�ϴ� �Լ�
    /// </summary>
    public void ShowSignupPanel()
    {
        // �Է� �ʵ� �ʱ�ȭ
        signUpInputID.text = "";
        signUpInputNickname.text = "";
        signUpInputPW.text = "";
        signUpInputPWConfirm.text = "";

        // �г� ����
        signUpPanel.SetActive(true);
        loginPanel.SetActive(false);

        // ��� ���� ��Ȱ��ȭ
        warningText.gameObject.SetActive(false);
    }

    /// <summary>
    /// connecting �г��� Ȱ��ȭ�ϴ� �Լ�
    /// </summary>
    public void ShowConnecting()
    {
        connectingPanel.SetActive(true);
    }

    /// <summary>
    /// connecting �г��� ��Ȱ��ȭ�ϴ� �Լ�
    /// </summary>
    public void CloseConnecting()
    {
        connectingPanel.SetActive(false);
    }

    public void ShowWarnMessage(string message)
    {
        Debug.Log(message);
        if(message.Length > 0)
            warningText.text = message;
        else
        {
            warningText.text = "�Է� ������ �߻��߽��ϴ�.";
        }
    }

    /// <summary>
    /// Scene�� �̵��ϴ� �Լ�
    /// </summary>
    public void MoveScene()
    {
        connectingPanel.SetActive(false);
        popupWindowPanel.SetActive(false);

        // Press To Start

        // SceneManager.LoadScene(0);
    }
}
