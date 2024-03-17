using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject loginPanel; // �α��� �г�
    public GameObject signUpPanel; // ȸ������ �г�

    // ��� ����
    public TextMeshProUGUI warningText; // ��� �ؽ�Ʈ

    // ����ڰ� �α����� �õ��� �� �߻��ϴ� �̺�Ʈ
    public event Action<string, string> onClickLogin;

    // ------------------------------- ��ũ��Ʈ�� Ȱ��ȭ�Ǿ��� �� ȣ�� -------------------------------
    private void Awake()
    {
        // �α��� ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        if (loginButton != null)
        {
            this.loginButton.onClick.AddListener(HandleLoginButtonClick);
        }

        // ȸ������ ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        if (signUpButton != null)
        {
            this.signUpButton.onClick.AddListener(HandleSignUpButtonClick);
        }

        // �α��� â ǥ�� ��ư�� Ŭ�� �̺�Ʈ�� �߰��մϴ�.
        if (showLoginButton != null)
        {
            this.showLoginButton.onClick.AddListener(ShowLoginPanel);
        }

        // ȸ������ â ǥ�� ��ư�� Ŭ�� �̺�Ʈ�� �߰��մϴ�.
        if (showSignUpButton != null)
        {
            this.showSignUpButton.onClick.AddListener(ShowSignupPanel);
        }
    }

    // ------------------------------- �α��� ��ư Ŭ�� �� ȣ��Ǵ� �Լ� -------------------------------
    private void HandleLoginButtonClick()
    {
        // �Էµ� ���̵�� ��й�ȣ�� ������
        string id = loginInputID.text;
        string pw = loginInputPW.text;

        // �ߺ��� ���̵��� ���
        if (IsNotValidIDPW(id, pw))
        {
            warningText.text = "���̵�� ��й�ȣ�� Ȯ�����ּ���.";
            warningText.gameObject.SetActive(true); // ��� ���� Ȱ��ȭ
            return;
        }

        // ���̵�� ��й�ȣ�� ����� �α׷� ���
        Debug.LogFormat("�α��� ����: id: {0}, pw={1}", id, pw);

        // onClickLogin �̺�Ʈ�� ȣ���Ͽ� �α����� �õ�
        onClickLogin?.Invoke(id, pw);
    }

    // ------------------------------- ȸ������ ��ư Ŭ�� �� ȣ��Ǵ� �Լ� -------------------------------
    private void HandleSignUpButtonClick()
    {
        // �Էµ� ���̵�, �г���, ��й�ȣ, ��й�ȣ Ȯ���� ������
        string id = signUpInputID.text;
        string nickname = signUpInputNickname.text;
        string pw = signUpInputPW.text;
        string pwConfirm = signUpInputPWConfirm.text;

        // �ߺ��� ���̵��� ���
        if (IsDuplicateID(id))
        {
            warningText.text = "�ߺ��� ���̵� �Դϴ�.";
            warningText.gameObject.SetActive(true); // ��� ���� Ȱ��ȭ
            return;
        }

        // �ߺ��� �г����� ���
        if (IsDuplicateNickname(nickname))
        {
            warningText.text = "�ߺ��� �г��� �Դϴ�.";
            warningText.gameObject.SetActive(true); // ��� ���� Ȱ��ȭ
            return;
        }

        // ��й�ȣ�� ��й�ȣ Ȯ���� ��ġ���� �ʴ� ���
        if (pw != pwConfirm)
        {
            warningText.text = "��й�ȣ�� ��ġ���� �ʽ��ϴ�.";
            warningText.gameObject.SetActive(true); // ��� ���� Ȱ��ȭ
            return;
        }

        // ���⿡ ȸ������ ���� �߰� �ʿ� @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Debug.LogFormat("ȸ������ ����: id={0}, nickname={1}, pw={2}", id, nickname, pw);

        // ȸ������ ���� �� �α��� â�� ������
        ShowLoginPanel();
    }

    // �ùٸ� ���̵� �� ��й�ȣ ���θ� Ȯ���ϴ� �Լ�
    private bool IsNotValidIDPW(string id, string pw)
    {
        // ���⿡ ���̵�� ��й�ȣ�� ��ȿ���� Ȯ���ϴ� ���� �ۼ� �ʿ� @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        return true; // �ӽ÷� true�� ��ȯ�ϵ��� ����
    }

    // ���̵� �ߺ� ���θ� Ȯ���ϴ� �Լ�
    private bool IsDuplicateID(string id)
    {
        // ���⿡ ���̵� �ߺ� ���θ� Ȯ���ϴ� ������ �ۼ� �ʿ� @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        return false; // �ӽ÷� false�� ��ȯ�ϵ��� ����
    }

    // �г��� �ߺ� ���θ� Ȯ���ϴ� �Լ�
    private bool IsDuplicateNickname(string nickname)
    {
        // ���⿡ �г��� �ߺ� ���θ� Ȯ���ϴ� ������ �ۼ� �ʿ� @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        return false; // �ӽ÷� false�� ��ȯ�ϵ��� ����
    }

    // �α��� �г��� Ȱ��ȭ�ϰ� ȸ������ �г��� ��Ȱ��ȭ�ϴ� �Լ�
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

    // ȸ������ �г��� Ȱ��ȭ�ϰ� �α��� �г��� ��Ȱ��ȭ�ϴ� �Լ�
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
}
