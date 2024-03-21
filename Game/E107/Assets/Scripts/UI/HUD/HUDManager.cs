using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// HUD(Head-Up Display)를 관리하는 클래스입니다.
/// </summary>
public class HUDManager : MonoBehaviour
{
    // 모험 상태
    [Header("[ 모험 상태 ]")]
    public TextMeshProUGUI gameTimeText; // 게임 시간 텍스트
    // public TextMeshProUGUI goldText;
    public TextMeshProUGUI jellyText; // 젤리 텍스트
    public TextMeshProUGUI nicknameText; // 닉네임 텍스트

    // 플레이어 상태
    [Header("[ 플레이어 상태 ]")]
    public TextMeshProUGUI playerHealthText; // 플레이어 체력 텍스트
    public Slider playerHealthSlider; // 플레이어 체력 바 슬라이더
    public PlayerController playerController; // 플레이어 컨트롤러

    // Drill Duck 상태
    [Header("[ Drill Duck 상태 ]")]
    public TextMeshProUGUI drillDuckNameText; // Drill Duck 이름 텍스트
    public TextMeshProUGUI drillDuckHealthText; // Drill Duck 체력 텍스트
    public Slider drillDuckHealthSlider; // Drill Duck  체력 바 슬라이더
    public DrillDuckController drillDuckController; // Drill Duck 컨트롤러

    // 팝업 창
    [Header("[ 팝업 창 ]")]
    public GameObject GameOverWindow; // 게임 오버 창

    // 게임 시작 시간 초기화
    private float gameTime = 0;

    // 시작 시 호출되는 Start 메서드
    void Start()
    {
        // Player GameObject를 찾아서 PlayerController 컴포넌트를 playerController 변수에 할당
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // 매 프레임마다 호출되는 Update 메서드
    void Update()
    {
        // 게임 시간 업데이트
        UpdateGameTime();

        // 골드 정보 업데이트
        // UpdateGoldDisplay();

        // 사용자 정보 업데이트
        UpdateUserInfoDisplay();

        // 플레이어 상태 업데이트
        UpdatePlayerStatus();

        // Drill Duck 상태 업데이트
        UpdateDrillDuckStatus();
    }

    // 게임 시간을 업데이트하는 메서드
    void UpdateGameTime()
    {
        gameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        gameTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 사용자 정보(닉네임, 젤리 수)를 UI에 업데이트하는 메서드
    void UpdateUserInfoDisplay()
    {
        // UserInfo 인스턴스 가져오기
        UserInfo userInfo = UserInfo.GetInstance();

        // 닉네임과 젤리 정보 가져오기
        string nickname = userInfo.getNickName();
        int jelly = userInfo.getJelly();

        // 가져온 정보를 TextMeshProUGUI에 적용
        nicknameText.text = nickname;
        jellyText.text = jelly.ToString();
    }

    // 플레이어 상태를 업데이트 하는 메서드
    void UpdatePlayerStatus()
    {
        // 플레이어의 현재 체력을 체력 바에 반영
        int Hp = playerController.Stat.Hp;
        int MaxHp = playerController.Stat.MaxHp;
        playerHealthSlider.value = (float)Hp / MaxHp;
        playerHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // 플레이어가 사망할 경우 게임 오버 창을 활성화하고 체력을 0으로 고정
        if (Hp < 0)
        {
            GameOverWindow.SetActive(true);
            playerHealthText.text = string.Format("0 / {0:0}", MaxHp);
        }
    }

    // Drill Duck 상태를 업데이트 하는 메서드
    void UpdateDrillDuckStatus()
    {
        // Drill Duck GameObject가 없는 경우 메서드를 종료
        if (GameObject.Find("DrillDuck(Clone)") == null) return;

        // Drill Duck GameObject를 찾아서 DrillDuckController 컴포넌트를 drillDuckController 변수에 할당
        drillDuckController = GameObject.Find("DrillDuck(Clone)").GetComponent<DrillDuckController>();

        // Drill Duck의 현재 체력을 체력 바에 반영
        int Hp = drillDuckController.Stat.Hp;
        int MaxHp = drillDuckController.Stat.MaxHp;
        drillDuckHealthSlider.value = (float)Hp / MaxHp;
        drillDuckHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // Drill Duck의 이름 정보를 TextMeshProUGUI에 적용
        drillDuckNameText.text = "Drill Duck";

        // Drill Duck이 사망할 경우 체력을 0으로 고정
        if (Hp < 0)
        {
            drillDuckHealthText.text = string.Format("0 / {0:0}", MaxHp);
        }
    }
}

