using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// HUD(Head-Up Display)를 관리하는 클래스입니다.
/// </summary>
public class HUDManager : MonoBehaviour
{
    // 사용할 변수 선언
    private PhotonManager photonManager;
    private bool isGameMenuOpen = false;
    private bool isPartyInfoOpen = false;
    private ManualBoardUI manualBoardUI;

    // 모험 상태
    [Header("[ 모험 상태 ]")]
    public TextMeshProUGUI nicknameText; // 닉네임 텍스트
    public TextMeshProUGUI currentServerText; // 서버 텍스트

    // 팝업 창
    [Header("[ 팝업 창 ]")]
    public GameObject GameMenuWindow; // 게임 메뉴 창
    public GameObject PartyInfoWindow; // 파티 정보 창

    // 열기 버튼
    [Header("[ 열기 버튼 ]")]
    public Button GameMenuButton;
    public Button partyInfoButton;

    // 닫기 버튼
    [Header("[ 닫기 버튼 ]")]
    public Button comeBackToGameCampButton; // 게임으로 돌아가기 버튼
    public Button comeBackToGameDungeonButton; // 게임으로 돌아가기 버튼
    public Button partyListCloseButton; // 파티 정보 닫기 버튼
    public Button myPartyCloseButton; // 파티 정보 닫기 버튼
    public Button partyRecruitButton;

    // 플레이어 상태
    [Header("[ 플레이어 상태 ]")]
    public TextMeshProUGUI playerHealthText; // 플레이어 체력 텍스트
    public Slider playerHealthSlider; // 플레이어 체력 바 슬라이더
    public TextMeshProUGUI playerManaText; // 플레이어 마나 텍스트
    public Slider playerManaSlider; // 플레이어 마나 바 슬라이더
    public PlayerController playerController; // 플레이어 컨트롤러

    // 팝업 창
    [Header("[ 팝업 창 ]")]
    public GameObject AdventureResultWindow; // 모험 결과 창

    private bool gameOverDisplayed = false; // 게임 오버 창이 표시되었는지 여부

    // 시작 시 호출되는 Start 메서드
    void Start()
    {
        // 사용자 정보 업데이트
        UpdateUserInfoDisplay();

        // 플레이어 GameObject를 찾아서 PlayerController 컴포넌트를 playerController 변수에 할당
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        // 게임 메뉴 버튼에 클릭 이벤트 리스너 추가
        GameMenuButton.onClick.AddListener(OpenGameMenu);

        // 게임 메뉴 닫기 버튼에 클릭 이벤트 리스너 추가
        comeBackToGameCampButton.onClick.AddListener(CloseGameMenu);
        comeBackToGameDungeonButton.onClick.AddListener(CloseGameMenu);

        // 파티 정보 버튼에 클릭 이벤트 리스너 추가
        partyInfoButton.onClick.AddListener(OpenPartyInfo);

        // 파티 정보 닫기 버튼, 파티 모집 버튼에 클릭 이벤트 리스너 추가
        partyRecruitButton.onClick.AddListener(OnClickPartyMakeButton);
        partyListCloseButton.onClick.AddListener(ClosePartyInfo);
        myPartyCloseButton.onClick.AddListener(ClosePartyInfo);
    }

    // 매 프레임마다 호출되는 Update 메서드
    void Update()
    {
        // 플레이어 상태 업데이트
        UpdatePlayerStatus();

        manualBoardUI = GameObject.FindObjectOfType<ManualBoardUI>();

        if (manualBoardUI != null && !manualBoardUI.isManualOpen)
        {
            // ESC 눌렀을 때
            if (Input.GetKeyDown(KeyCode.Escape) && !isPartyInfoOpen && !isGameMenuOpen)
            {
                // 게임 메뉴를 여는 이벤트
                GameMenuButton.onClick.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !isPartyInfoOpen && isGameMenuOpen)
            {
                // 게임 메뉴를 닫는 이벤트
                comeBackToGameCampButton.onClick.Invoke();
                comeBackToGameDungeonButton.onClick.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && isPartyInfoOpen && !isGameMenuOpen)
            {
                // 파티 정보를 닫는 이벤트
                partyListCloseButton.onClick.Invoke();
                myPartyCloseButton.onClick.Invoke();
            }

            // Tab 눌렀을 때
            if (Input.GetKeyDown(KeyCode.Tab) && !isGameMenuOpen && !isPartyInfoOpen)
            {
                // 파티 정보를 여는 이벤트
                partyInfoButton.onClick.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && !isGameMenuOpen && isPartyInfoOpen)
            {
                // 파티 정보를 닫는 이벤트
                partyListCloseButton.onClick.Invoke();
                myPartyCloseButton.onClick.Invoke();
            }
        }
    }

    // 사용자 정보를 UI에 업데이트하는 메서드
    void UpdateUserInfoDisplay()
    {
        // UserInfo 인스턴스 가져오기
        UserInfo userInfo = UserInfo.GetInstance();

        // 닉네임 정보 가져오기
        string nickname = userInfo.getNickName();
        int currentServer = userInfo.GetCurrentServer();

        // 가져온 정보를 TextMeshProUGUI에 적용
        nicknameText.text = nickname;
        currentServerText.text = currentServer.ToString();

    }

    // 플레이어 상태를 업데이트 하는 메서드
    void UpdatePlayerStatus()
    {
        // 플레이어의 현재 체력을 체력 바에 반영
        int Hp = playerController.Stat.Hp;
        int MaxHp = playerController.Stat.MaxHp;
        playerHealthSlider.value = (float)Hp / MaxHp;
        playerHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // 플레이어의 현재 마나를 마나 바에 반영
        int Mp = playerController.Stat.Mp;
        int MaxMp = playerController.Stat.MaxMp;
        playerManaSlider.value = (float)Mp / MaxMp;
        playerManaText.text = string.Format("{0:0} / {1:0}", Mp, MaxMp);

        // 플레이어가 사망할 경우 게임 오버 창을 활성화
        if (Hp <= 0 && !gameOverDisplayed)
        {
            AdventureResultWindow.SetActive(true);
            gameOverDisplayed = true; // 게임 오버 창이 표시되었음을 표시
        }
    }

    // 게임 메뉴를 여는 메서드
    void OpenGameMenu()
    {
        isGameMenuOpen = true;
        GameMenuWindow.SetActive(true);
    }

    // 게임 메뉴를 닫는 메서드
    void CloseGameMenu()
    {
        isGameMenuOpen = false;
        GameMenuWindow.SetActive(false);
    }

    // 파티 정보를 여는 메서드
    void OpenPartyInfo()
    {
        isPartyInfoOpen = true;
        PartyInfoWindow.SetActive(true);
    }

    // 파티 정보를 닫는 메서드
    void ClosePartyInfo()
    {
        isPartyInfoOpen = false; // 창 닫기
        PartyInfoWindow.SetActive(false);
    }

    // 파티 모집 버튼이 클릭시 호출될 메서드
    void OnClickPartyMakeButton()
    {
        PhotonUIManager manager = GameObject.Find("gm").GetComponent<PhotonUIManager>();
        string roomName = manager.GetDescription();

        if (roomName.Length == 0) return;

        isPartyInfoOpen = false; // 창 닫기
    }
    
}

