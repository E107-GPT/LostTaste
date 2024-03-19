using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    // 로그인 입력 필드
    public TMP_InputField loginInputID; // 로그인 아이디 입력 필드
    public TMP_InputField loginInputPW; // 로그인 비밀번호 입력 필드

    // 회원가입 입력 필드
    public TMP_InputField signUpInputID; // 회원가입 아이디 입력 필드
    public TMP_InputField signUpInputNickname; // 회원가입 닉네임 입력 필드
    public TMP_InputField signUpInputPW; // 회원가입 비밀번호 입력 필드
    public TMP_InputField signUpInputPWConfirm; // 회원가입 비밀번호 확인 입력 필드

    // 버튼
    public Button loginButton; // 로그인 버튼
    public Button signUpButton; // 회원가입 버튼
    public Button showLoginButton; // 로그인 창 표시 버튼
    public Button showSignUpButton; // 회원가입 창 표시 버튼

    // 패널
    public GameObject loginPanel; // 로그인 패널
    public GameObject signUpPanel; // 회원가입 패널
    public GameObject connectingPanel; // 커넥트 패널

    // 경고 문구
    public TextMeshProUGUI warningText; // 경고 텍스트

    // 사용자가 로그인을 시도할 때 발생하는 이벤트
    public event Action<string, string> onClickLogin;

    HTTPRequest request;

    // ------------------------------- 스크립트가 활성화되었을 때 호출 -------------------------------
    private void Awake()
    {
        // 로그인 버튼에 클릭 이벤트를 추가
        if (loginButton != null)
        {
            this.loginButton.onClick.AddListener(HandleLoginButtonClick);
        }

        // 회원가입 버튼에 클릭 이벤트를 추가
        if (signUpButton != null)
        {
            this.signUpButton.onClick.AddListener(HandleSignUpButtonClick);
        }

        // 로그인 창 표시 버튼에 클릭 이벤트를 추가합니다.
        if (showLoginButton != null)
        {
            this.showLoginButton.onClick.AddListener(ShowLoginPanel);
        }

        // 회원가입 창 표시 버튼에 클릭 이벤트를 추가합니다.
        if (showSignUpButton != null)
        {
            this.showSignUpButton.onClick.AddListener(ShowSignupPanel);
        }

        if(connectingPanel!= null)
        connectingPanel.SetActive(false);

        request = GameObject.Find("GameManager").GetComponent<HTTPRequest>();        
    }

    // ------------------------------- 로그인 버튼 클릭 시 호출되는 함수 -------------------------------
    public void HandleLoginButtonClick()
    {
        // 입력된 아이디와 비밀번호를 가져옴
        string id = loginInputID.text;
        string pw = loginInputPW.text;
        Debug.Log(id);
        Debug.Log(pw);

        // 유효하지 않은 아이디 및 비밀번호인 경우
        if (!IsNotValidIDPW(id, pw))
        {
            warningText.text = "아이디와 비밀번호를 확인해주세요.";
            warningText.gameObject.SetActive(true); // 경고 문구 활성화
            return;
        }

        // 아이디와 비밀번호를 디버그 로그로 출력
        Debug.LogFormat("로그인 정보: id: {0}, pw={1}", id, pw);


        // onClickLogin 이벤트를 호출하여 로그인을 시도
        //onClickLogin?.Invoke(id, pw);

        Dictionary<string, string> requestParam = new Dictionary<string, string>();

        requestParam.Add("accountId", id);
        requestParam.Add("password", pw);

        request.POSTCall("auth/login", requestParam);
    }

    // ------------------------------- 회원가입 버튼 클릭 시 호출되는 함수 -------------------------------
    public void HandleSignUpButtonClick()
    {
        // 입력된 아이디, 닉네임, 비밀번호, 비밀번호 확인을 가져옴
        string id = signUpInputID.text;
        string nickname = signUpInputNickname.text;
        string pw = signUpInputPW.text;
        string pwConfirm = signUpInputPWConfirm.text;

        // 중복된 아이디인 경우
        if (!IsDuplicateID(id))
        {
            warningText.text = "중복된 아이디 입니다.";
            warningText.gameObject.SetActive(true); // 경고 문구 활성화
            return;
        }

        // 중복된 닉네임인 경우
        if (!IsDuplicateNickname(nickname))
        {
            warningText.text = "중복된 닉네임 입니다.";
            warningText.gameObject.SetActive(true); // 경고 문구 활성화
            return;
        }

        // 비밀번호와 비밀번호 확인이 일치하지 않는 경우
        if (pw != pwConfirm)
        {
            warningText.text = "비밀번호가 일치하지 않습니다.";
            warningText.gameObject.SetActive(true); // 경고 문구 활성화
            return;
        }

        // 여기에 회원가입 로직 추가 필요 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        Debug.LogFormat("회원가입 정보: id={0}, nickname={1}, pw={2}", id, nickname, pw);

        // 회원가입 성공 후 로그인 창을 보여줌
        //ShowLoginPanel();

        Dictionary<string, string> requestParam = new Dictionary<string, string>();

        requestParam.Add("accountId", id);
        requestParam.Add("password", pw);
        requestParam.Add("nickname", nickname);

        request.POSTCall("user", requestParam);
    }

    public void SignupFailure()
    {
        warningText.text = "회원가입에 실패했습니다.";
        warningText.gameObject.SetActive(true); // 경고 문구 활성화
        return;
    }
    public void LoginFailure()
    {
        warningText.text = "로그인에 실패했습니다.";
        warningText.gameObject.SetActive(true); // 경고 문구 활성화
        return;
    }

    // 올바른 아이디 및 비밀번호 여부를 확인하는 함수
    private bool IsNotValidIDPW(string id, string pw)
    {
        // 여기에 아이디와 비밀번호의 유효성을 확인하는 로직 작성 필요 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        return true; // 임시로 true를 반환하도록 설정
    }

    // 아이디 중복 여부를 확인하는 함수
    private bool IsDuplicateID(string id)
    {
        // 여기에 아이디 중복 여부를 확인하는 로직을 작성 필요 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        return true; // 임시로 false를 반환하도록 설정
    }

    // 닉네임 중복 여부를 확인하는 함수
    private bool IsDuplicateNickname(string nickname)
    {
        // 여기에 닉네임 중복 여부를 확인하는 로직을 작성 필요 @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        return true; // 임시로 false를 반환하도록 설정
    }

    // 로그인 패널을 활성화하고 회원가입 패널을 비활성화하는 함수
    public void ShowLoginPanel()
    {
        // 입력 필드 초기화
        loginInputID.text = "";
        loginInputPW.text = "";

        // 패널 변경
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);

        // 경고 문구 비활성화
        warningText.gameObject.SetActive(false);
    }

    public void ShowConnecting()
    {
        connectingPanel.SetActive(true);
    }
    public void CloseConnecting()
    {
        connectingPanel.SetActive(false);
    }

    // 회원가입 패널을 활성화하고 로그인 패널을 비활성화하는 함수
    public void ShowSignupPanel()
    {
        // 입력 필드 초기화
        signUpInputID.text = "";
        signUpInputNickname.text = "";
        signUpInputPW.text = "";
        signUpInputPWConfirm.text = "";

        // 패널 변경
        signUpPanel.SetActive(true);
        loginPanel.SetActive(false);

        // 경고 문구 비활성화
        warningText.gameObject.SetActive(false);
    }

    public void MoveScene()
    {
        SceneManager.LoadScene(3);
    }
}
