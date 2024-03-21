using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// ����� ������ ������ Ŭ�����Դϴ�.
/// </summary>
public class Login : MonoBehaviour
{
    // �α��� �Է� �ʵ�
    [Header("[ �α��� �Է� �ʵ� ]")]
    public TMP_InputField loginInputID; // �α��� ���̵� �Է� �ʵ�
    public TMP_InputField loginInputPW; // �α��� ��й�ȣ �Է� �ʵ�

    // ȸ������ �Է� �ʵ�
    [Header("[ ȸ������ �Է� �ʵ� ]")]
    public TMP_InputField signUpInputID; // ȸ������ ���̵� �Է� �ʵ�
    public TMP_InputField signUpInputNickname; // ȸ������ �г��� �Է� �ʵ�
    public TMP_InputField signUpInputPW; // ȸ������ ��й�ȣ �Է� �ʵ�
    public TMP_InputField signUpInputPWConfirm; // ȸ������ ��й�ȣ Ȯ�� �Է� �ʵ�

    // ���� ��ư
    [Header("[ ���� ��ư ]")]
    public Button loginButton; // �α��� ��ư
    public Button signUpButton; // ȸ������ ��ư

    // �г� ��ü ��ư
    [Header("[ �г� ��ü ��ư ]")]
    public Button showLoginButton; // �α��� �г� Ȱ��ȭ ��ư
    public Button showSignUpButton; // ȸ������ �г� Ȱ��ȭ ��ư

    // �г�
    [Header("[ �г� ]")]
    public GameObject authenticationPanel; // ���� �г�
    public GameObject loginPanel; // �α��� �г�
    public GameObject signUpPanel; // ȸ������ �г�
    public GameObject connectingPanel; // ���� �� �г�
    // public GameObject nicknamePanel; // �г��� �г�

    // �ؽ�Ʈ
    [Header("[ �ؽ�Ʈ ]")]
    public TextMeshProUGUI warningText; // ��� �ؽ�Ʈ
    // public TextMeshProUGUI nicknameText; // �г��� �ؽ�Ʈ

    HTTPRequest request;

    // ��ũ��Ʈ�� Ȱ��ȭ�Ǿ��� �� ȣ��Ǵ� �޼���
    private void Awake()
    {
        // ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        if (loginButton != null)
            this.loginButton.onClick.AddListener(HandleLoginButtonClick);

        if (signUpButton != null)
            this.signUpButton.onClick.AddListener(HandleSignUpButtonClick);

        if (showLoginButton != null)
            this.showLoginButton.onClick.AddListener(ShowLoginPanel);

        if (showSignUpButton != null)
            this.showSignUpButton.onClick.AddListener(ShowSignupPanel);

        // ���� �� �г��� Ȱ��ȭ ������ ���, �ش� �г��� ��Ȱ��ȭ
        if (connectingPanel != null)
            connectingPanel.SetActive(false);

        // ���� �Ŵ������� HTTPRequest ������Ʈ�� ã�� ������
        request = GameObject.Find("GameManager").GetComponent<HTTPRequest>();        
    }

    // �α��� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void HandleLoginButtonClick()
    {
        // �Էµ� ���̵�� ��й�ȣ�� ������
        string id = loginInputID.text;
        string pw = loginInputPW.text;

        // �α��� ������ ����� �α׷� ���
        // Debug.LogFormat("�α��� ����: id: {0}, pw={1}", id, pw);

        // ������ �α��� ��û�� ����
        Dictionary<string, string> requestParam = new Dictionary<string, string>();
        requestParam.Add("accountId", id);
        requestParam.Add("password", pw);
        request.POSTCall("auth/login", requestParam);
    }

    // ȸ������ ��ư Ŭ�� �� ȣ��Ǵ� �޼���
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

    // �α��� ���� �� ȣ��Ǵ� �޼���
    public void LoginFailure()
    {
        warningText.text = "���̵�� ��й�ȣ�� Ȯ�����ּ���.";
        warningText.gameObject.SetActive(true); // ��� ���� Ȱ��ȭ
        return;
    }

    // ȸ������ ���� �� ȣ��Ǵ� �޼���
    public void SignupFailure()
    {
        warningText.text = "ȸ�����Կ� �����߽��ϴ�.";
        warningText.gameObject.SetActive(true); // ��� ���� Ȱ��ȭ
        return;
    }

    // �α��� �г��� Ȱ��ȭ�ϰ� ȸ������ �г��� ��Ȱ��ȭ�ϴ� �޼���
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

    // ȸ������ �г��� Ȱ��ȭ�ϰ� �α��� �г��� ��Ȱ��ȭ�ϴ� �޼���
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

    // ���� �� �г��� Ȱ��ȭ�ϴ� �޼���
    public void ShowConnecting()
    {
        connectingPanel.SetActive(true);
    }

    // ���� �� �г��� ��Ȱ��ȭ�ϴ� �޼���
    public void CloseConnecting()
    {
        connectingPanel.SetActive(false);
    }

    // ��� �޼����� ǥ���ϴ� �޼���
    public void ShowWarnMessage(string message)
    {
        // Debug.Log(message);

        if (message.Length > 0)
            warningText.text = message;
        else
            warningText.text = "�Է� ������ �߻��߽��ϴ�.";
    }

    // �α��� �� ���� �г��� �ݴ� �޼���
    public void CloseAuthenticationPanel()
    {
        connectingPanel.SetActive(false);
        authenticationPanel.SetActive(false);
        // nicknamePanel.SetActive(true);
    }
}
