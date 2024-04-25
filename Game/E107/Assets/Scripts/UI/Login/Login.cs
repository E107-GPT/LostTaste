using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// 로그인 및 회원가입을 관리하는 클래스입니다.
/// </summary>
public class Login : MonoBehaviour
{
    // 로그인 입력 필드
    [Header("[ 로그인 입력 필드 ]")]
    public TMP_InputField loginInputID; // 로그인 아이디 입력 필드
    public TMP_InputField loginInputPW; // 로그인 비밀번호 입력 필드

    // 회원가입 입력 필드
    [Header("[ 회원가입 입력 필드 ]")]
    public TMP_InputField signUpInputID; // 회원가입 아이디 입력 필드
    public TMP_InputField signUpInputNickname; // 회원가입 닉네임 입력 필드
    public TMP_InputField signUpInputPW; // 회원가입 비밀번호 입력 필드
    public TMP_InputField signUpInputPWConfirm; // 회원가입 비밀번호 확인 입력 필드

    // 인증 버튼
    [Header("[ 인증 버튼 ]")]
    public Button loginButton; // 로그인 버튼
    public Button signUpButton; // 회원가입 버튼

    // 패널 버튼
    [Header("[ 패널 버튼 ]")]
    public Button showLoginButton; // 로그인 패널 표시 버튼
    public Button showSignUpButton; // 회원가입 패널 표시 버튼

    // 패널
    [Header("[ 패널 ]")]
    public GameObject authenticationPanel; // 인증 패널
    public GameObject loginPanel; // 로그인 패널
    public GameObject signUpPanel; // 회원가입 패널
    public GameObject connectingPanel; // 연결 중 패널
    public GameObject nicknamePanel; // 닉네임 패널
    public GameObject serverPanel; // 서버 패널

    // 텍스트
    [Header("[ 텍스트 ]")]
    public TextMeshProUGUI warningText; // 경고 텍스트
    public TextMeshProUGUI nicknameText; // 닉네임 텍스트

    HTTPRequest request;

    // 객체가 초기화될 때 호출되는 메서드
    private void Awake()
    {
        // 버튼에 이벤트 추가
        if (loginButton != null)
            this.loginButton.onClick.AddListener(HandleLoginButtonClick);

        if (signUpButton != null)
            this.signUpButton.onClick.AddListener(HandleSignUpButtonClick);

        if (showLoginButton != null)
            this.showLoginButton.onClick.AddListener(ShowLoginPanel);

        if (showSignUpButton != null)
            this.showSignUpButton.onClick.AddListener(ShowSignupPanel);

        // 연결 중 패널 숨기기, 해당 패널은 나중에 표시됨
        if (connectingPanel != null)
            connectingPanel.SetActive(false);

        // HTTPRequest 컴포넌트를 찾아서 할당
        request = GameObject.Find("GameManager").GetComponent<HTTPRequest>();        
    }

    // 사용자 닉네임을 UI에 업데이트하는 메서드
    void UpdateUserNicknameDisplay()
    {
        // UserInfo 인스턴스 가져오기
        UserInfo userInfo = UserInfo.GetInstance();

        // 닉네임 정보 가져오기
        string nickname = userInfo.getNickName();

        // 가져온 정보를 TextMeshProUGUI에 적용
        nicknameText.text = nickname + "<color=white>님, 환영합니다!</color>";
    }

    // 로그인 버튼 클릭시 호출되는 메서드
    public void HandleLoginButtonClick()
    {
        // 입력된 아이디와 비밀번호 가져오기
        string id = loginInputID.text;
        string pw = loginInputPW.text;

        // 인증 정보 출력
        // Debug.LogFormat("인증 정보: id: {0}, pw={1}", id, pw);

        // 로그인 요청 보내기
        Dictionary<string, string> requestParam = new Dictionary<string, string>();
        requestParam.Add("accountId", id);
        requestParam.Add("password", pw);
        request.POSTCall("auth/login", requestParam);
    }

    // 회원가입 버튼 클릭시 호출되는 메서드
    public void HandleSignUpButtonClick()
    {
        // 입력된 정보 가져오기
        string id = signUpInputID.text;
        string nickname = signUpInputNickname.text;
        string pw = signUpInputPW.text;
        string pwConfirm = signUpInputPWConfirm.text;

        // 비밀번호와 비밀번호 확인이 일치하는지 확인
        if (pw != pwConfirm)
        {
            warningText.text = "비밀번호가 일치하지 않습니다.";
            warningText.gameObject.SetActive(true); // 경고 텍스트 표시
            return;
        }

        // 회원가입 정보 출력
        // Debug.LogFormat("회원가입 정보: id={0}, nickname={1}, pw={2}", id, nickname, pw);

        // 회원가입 요청 보내기
        Dictionary<string, string> requestParam = new Dictionary<string, string>();
        requestParam.Add("accountId", id);
        requestParam.Add("password", pw);
        requestParam.Add("nickname", nickname);
        request.POSTCall("user", requestParam);
    }

    // 로그인 실패시 호출되는 메서드
    public void LoginFailure()
    {
        warningText.text = "아이디 또는 비밀번호를 확인해주세요.";
        warningText.gameObject.SetActive(true); // 경고 텍스트 표시
        return;
    }

    // 회원가입 실패시 호출되는 메서드
    public void SignupFailure()
    {
        warningText.text = "회원가입에 실패했습니다.";
        warningText.gameObject.SetActive(true); // 경고 텍스트 표시
        return;
    }

    // 로그인 패널 표시 및 회원가입 패널 숨기는 메서드
    public void ShowLoginPanel()
    {
        // 입력 필드 초기화
        loginInputID.text = "";
        loginInputPW.text = "";

        // 패널 표시
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);

        // 경고 텍스트 숨기기
        warningText.gameObject.SetActive(false);
    }

    // 회원가입 패널 표시 및 로그인 패널 숨기는 메서드
    public void ShowSignupPanel()
    {
        // 입력 필드 초기화
        signUpInputID.text = "";
        signUpInputNickname.text = "";
        signUpInputPW.text = "";
        signUpInputPWConfirm.text = "";

        // 패널 표시
        signUpPanel.SetActive(true);
        loginPanel.SetActive(false);

        // 경고 텍스트 숨기기
        warningText.gameObject.SetActive(false);
    }

    // 연결 중 패널 표시하는 메서드
    public void ShowConnecting()
    {
        connectingPanel.SetActive(true);
    }

    // 연결 중 패널 숨기는 메서드
    public void CloseConnecting()
    {
        connectingPanel.SetActive(false);
    }

    // 경고 메시지를 표시하는 메서드
    public void ShowWarnMessage(string message)
    {
        if (message.Length > 0)
            warningText.text = message;
        else
            warningText.text = "입력에 문제가 발생했습니다.";
    }

    // 인증 패널을 닫는 메서드
    public void CloseAuthenticationPanel()
    {
        connectingPanel.SetActive(false);
        authenticationPanel.SetActive(false);
        nicknamePanel.SetActive(true);
        serverPanel.SetActive(true);

        // 사용자 닉네임 표시
        UpdateUserNicknameDisplay();
    }
}