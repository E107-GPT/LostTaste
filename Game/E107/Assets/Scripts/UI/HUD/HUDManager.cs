using UnityEngine;
using UnityEngine.UI;
using TMPro;

// HUD(Head-Up Display)를 관리하는 클래스
public class HUDManager : MonoBehaviour
{
    // UI에 표시될 게임 시간, 골드, 닉네임, 젤리 정보를 위한 TextMeshProUGUI 변수 선언
    public TextMeshProUGUI gameTimeText;
    // public TextMeshProUGUI goldText;
    public TextMeshProUGUI nicknameText;
    public TextMeshProUGUI jellyText;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI drillDuckHealthText;

    public Slider playerHealthSlider;
    public Slider drillDuckHealthSlider;

    // 플레이어와 보스 몬스터 변수 선언
    public PlayerController playerController;
    // public DrillDuckController drillDuckController;

    private float gameTime = 0;

    // 시작 시 호출되는 Start 메소드
    void Start()
    {
        playerHealthSlider.value = 1;
        drillDuckHealthSlider.value = 1;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        // drillDuckController = GameObject.Find("DrillDuck").GetComponent<DrillDuckController>();
    }

    // 매 프레임마다 호출되는 Update 메소드
    void Update()
    {
        UpdateGameTime(); // 게임 시간 업데이트
        // UpdateGoldDisplay(); // 골드 정보 업데이트
        UpdateUserInfoDisplay(); // 사용자 정보 업데이트
        UpdatePlayerHealthBar(); // 체력 바 업데이트
        // UpdateDrillDuckHealthBar();
    }

    // 게임 시간을 업데이트하는 메소드
    void UpdateGameTime()
    {
        gameTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);
        gameTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 사용자 정보(닉네임, 젤리 수)를 UI에 업데이트하는 메소드
    void UpdateUserInfoDisplay()
    {
        // UserInfo 인스턴스 가져오기
        UserInfo userInfo = UserInfo.GetInstance();

        // 닉네임과 젤리 정보 가져오기
        string nickname = userInfo.getNickName();
        int jelly = userInfo.getJelly();

        // 가져온 정보를 TextMeshProUGUI에 설정
        nicknameText.text = nickname;
        jellyText.text = jelly.ToString();
    }

    // 플레이어 체력 바를 업데이트 하는 메소드
    void UpdatePlayerHealthBar()
    {
        // 플레이어의 현재 체력을 체력 바에 반영
        int Hp = playerController.Stat.Hp;
        int MaxHp = playerController.Stat.MaxHp;
        playerHealthSlider.value = (float)Hp / MaxHp;
        playerHealthText.text = string.Format("{0:00} / {1:00}", Hp, MaxHp);
    }

    // 드릴 덕 체력 바를 업데이트 하는 메소드
    // void UpdateDrillDuckHealthBar()
    // {
    //     // 플레이어의 현재 체력을 체력 바에 반영
    //     int Hp = drillDuckController.Stat.Hp;
    //     int MaxHp = drillDuckController.Stat.MaxHp;
    //     drillDuckHealthSlider.value = (float)Hp / MaxHp;
    //     drillDuckHealthText.text = string.Format("{0:00} / {1:00}", Hp, MaxHp);
    // }
}

