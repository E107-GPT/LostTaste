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
    public TextMeshProUGUI playerManaText; // 플레이어 마나 텍스트
    public Slider playerManaSlider; // 플레이어 마나 바 슬라이더
    public PlayerController playerController; // 플레이어 컨트롤러

    // 보스 상태
    [Header("[ 보스 상태 ]")]
    public GameObject bossStatus;
    public TextMeshProUGUI bossNameText; // 보스 이름 텍스트
    public TextMeshProUGUI bossHealthText; // 보스 체력 텍스트
    public Slider bossHealthSlider; // 보스 체력 바 슬라이더

    // 보스 컨트롤러
    [Header("[ 보스 컨트롤러 ]")]
    public DrillDuckController bossController; // 드릴덕

    // 팝업 창
    [Header("[ 팝업 창 ]")]
    public GameObject GameOverWindow; // 게임 오버 창

    // 게임 시작 시간 초기화
    private float gameTime = 0;

    // 시작 시 호출되는 Start 메서드
    void Start()
    {
        // 플레이어 GameObject를 찾아서 PlayerController 컴포넌트를 playerController 변수에 할당
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // 매 프레임마다 호출되는 Update 메서드
    void Update()
    {
        // 게임 시간 업데이트
        UpdateGameTime();

        // 골드 정보 업데이트
        // UpdateGoldDisplay();

        // 사용자 정보 업데이트w
        UpdateUserInfoDisplay();

        // 플레이어 상태 업데이트
        UpdatePlayerStatus();

        // 보스 상태 업데이트
        UpdateBossStatus();
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

        // 플레이어의 현재 마나를 마나 바에 반영
        int Mp = playerController.Stat.Mp;
        int MaxMp = playerController.Stat.MaxMp;
        playerManaSlider.value = (float)Mp / MaxMp;
        playerManaText.text = string.Format("{0:0} / {1:0}", Mp, MaxMp);

        // 플레이어가 사망할 경우 게임 오버 창을 활성화
        if (Hp <= 0)
        {
            GameOverWindow.SetActive(true);
        }
    }

    // 보스 상태를 업데이트 하는 메서드
    void UpdateBossStatus()
    {
        // 보스 GameObject가 없는 경우 메서드를 종료
        if (GameObject.Find("DrillDuck(Clone)") == null) return;

        // 보스 GameObject를 찾아서 BossController 컴포넌트를 bossController 변수에 할당
        bossController = GameObject.Find("DrillDuck(Clone)").GetComponent<DrillDuckController>();

        // 보스의 이름 정보를 TextMeshProUGUI에 적용
        bossNameText.text = "Drill Duck";

        // 보스의 현재 체력을 체력 바에 반영
        int Hp = bossController.Stat.Hp;
        int MaxHp = bossController.Stat.MaxHp;
        bossHealthSlider.value = (float)Hp / MaxHp;
        bossHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // 보스가 사망할 경우 보스 상태 창을 비활성화
        if (Hp <= 0)
        {
            bossStatus.SetActive(false);
        }
    }
}

