using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 보스 상태를 관리하는 클래스입니다.
/// </summary>
public class BossStatusManager : MonoBehaviour
{
    // 드릴덕 상태
    [Header("[ 드릴덕 상태 ]")]
    public GameObject drillDuckStatus;
    public TextMeshProUGUI drillDuckNameText; // 이름 텍스트
    public TextMeshProUGUI drillDuckHealthText; // 체력 텍스트
    public Slider drillDuckHealthSlider; // 체력 바 슬라이더

    // 크로커다일 상태
    [Header("[ 크로커다일 상태 ]")]
    public GameObject crocodileStatus;
    public TextMeshProUGUI crocodileNameText; // 이름 텍스트
    public TextMeshProUGUI crocodileHealthText; // 체력 텍스트
    public Slider crocodileHealthSlider; // 체력 바 슬라이더

    // 아이스킹 상태
    [Header("[ 아이스킹 상태 ]")]
    public GameObject iceKingStatus;
    public TextMeshProUGUI iceKingNameText; // 이름 텍스트
    public TextMeshProUGUI iceKingHealthText; // 체력 텍스트
    public Slider iceKingHealthSlider; // 체력 바 슬라이더

    // 몬스터킹 상태
    [Header("[ 몬스터킹 상태 ]")]
    public GameObject monsterKingStatus;
    public TextMeshProUGUI monsterKingNameText; // 이름 텍스트
    public TextMeshProUGUI monsterKingHealthText; // 체력 텍스트
    public Slider monsterKingHealthSlider; // 체력 바 슬라이더

    // 모험 결과 창
    [Header("[ 모험 결과 창 ]")]
    public GameObject adventureResultsWindow;
    public GameObject finalStageIcon; // Final Stage 클리어 아이콘
    public TextMeshProUGUI stageClearText; // 스테이지 클리어 텍스트

    // 보스 컨트롤러
    [Header("[ 보스 컨트롤러 ]")]
    public DrillDuckController drillDuckController; // 드릴덕
    public CrocodileController crocodileController; // 크로커다일
    public IceKingController iceKingController; // 아이스킹
    public MonsterKingController monsterKingController; // 아이스킹

    // 매 프레임마다 호출되는 Update 메서드
    void Update()
    {
        // 드릴덕 상태 업데이트
        UpdateDrillDuckStatus();

        // 크로커다일 상태 업데이트
        UpdateCrocodileStatus();

        // 아이스킹 상태 업데이트
        UpdateIceKingStatus();

        // 몬스터킹 상태 업데이트
        UpdateMonsterKingStatus();
    }

    // 드릴덕 상태를 업데이트 하는 메서드
    void UpdateDrillDuckStatus()
    {
        // 보스 GameObject가 없는 경우 메서드를 종료
        if (GameObject.Find("DrillDuck(Clone)") == null) return;

        // 보스 GameObject를 찾아서 BossController 컴포넌트를 bossController 변수에 할당
        drillDuckController = GameObject.Find("DrillDuck(Clone)").GetComponent<DrillDuckController>();

        // 보스의 이름 정보를 TextMeshProUGUI에 적용
        drillDuckNameText.text = "Drill Duck";

        // 보스의 현재 체력을 체력 바에 반영
        int Hp = drillDuckController.Stat.Hp;
        int MaxHp = drillDuckController.Stat.MaxHp;
        drillDuckHealthSlider.value = (float)Hp / MaxHp;
        drillDuckHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // 보스가 사망할 경우 보스 상태 창을 비활성화
        if (Hp <= 0)
        {
            drillDuckStatus.SetActive(false);
        }
    }

    // 크로커다일 상태를 업데이트 하는 메서드
    void UpdateCrocodileStatus()
    {
        // 보스 GameObject가 없는 경우 메서드를 종료
        if (GameObject.Find("Crocodile(Clone)") == null) return;

        // 보스 GameObject를 찾아서 BossController 컴포넌트를 bossController 변수에 할당
        crocodileController = GameObject.Find("Crocodile(Clone)").GetComponent<CrocodileController>();

        // 보스의 이름 정보를 TextMeshProUGUI에 적용
        crocodileNameText.text = "Crocodile";

        // 보스의 현재 체력을 체력 바에 반영
        int Hp = crocodileController.Stat.Hp;
        int MaxHp = crocodileController.Stat.MaxHp;
        crocodileHealthSlider.value = (float)Hp / MaxHp;
        crocodileHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // 보스가 사망할 경우 보스 상태 창을 비활성화
        if (Hp <= 0)
        {
            crocodileStatus.SetActive(false);
        }
    }

    // 아이스킹 상태를 업데이트 하는 메서드
    void UpdateIceKingStatus()
    {
        // 보스 GameObject가 없는 경우 메서드를 종료
        if (GameObject.Find("IceKing(Clone)") == null) return;

        // 보스 GameObject를 찾아서 BossController 컴포넌트를 bossController 변수에 할당
        iceKingController = GameObject.Find("IceKing(Clone)").GetComponent<IceKingController>();

        // 보스의 이름 정보를 TextMeshProUGUI에 적용
        iceKingNameText.text = "Ice King";

        // 보스의 현재 체력을 체력 바에 반영
        int Hp = iceKingController.Stat.Hp;
        int MaxHp = iceKingController.Stat.MaxHp;
        iceKingHealthSlider.value = (float)Hp / MaxHp;
        iceKingHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // 보스가 사망할 경우 보스 상태 창을 비활성화
        if (Hp <= 0)
        {
            iceKingStatus.SetActive(false);
        }
    }

    // 몬스터킹 상태를 업데이트 하는 메서드
    void UpdateMonsterKingStatus()
    {
        // 보스 GameObject가 없는 경우 메서드를 종료
        if (GameObject.Find("MonsterKing(Clone)") == null) return;

        // 보스 GameObject를 찾아서 BossController 컴포넌트를 bossController 변수에 할당
        monsterKingController = GameObject.Find("MonsterKing(Clone)").GetComponent<MonsterKingController>();

        // 보스의 이름 정보를 TextMeshProUGUI에 적용
        monsterKingNameText.text = "Monster King";

        // 보스의 현재 체력을 체력 바에 반영
        int Hp = monsterKingController.Stat.Hp;
        int MaxHp = monsterKingController.Stat.MaxHp;
        monsterKingHealthSlider.value = (float)Hp / MaxHp;
        monsterKingHealthText.text = string.Format("{0:0} / {1:0}", Hp, MaxHp);

        // 보스가 사망할 경우 보스 상태 창을 비활성화
        if (Hp <= 0)
        {
            monsterKingStatus.SetActive(false);

            // 모험 결과 창 업데이트 및 활성화
            adventureResultsWindow.SetActive(true);
            finalStageIcon.SetActive(true);
            stageClearText.text = "모든 스테이지를 클리어했습니다!";
        }
    }
}

