using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// 직업 선택 UI가 나오도록 하는 클래스입니다.
/// </summary>
public class ClassSelectionWindow : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 사용할 변수 선언
    private int selectedClassNum = 1; // 현재 선택 된 직업 번호
    private Skill WarriorClassSkill;
    private Skill MageClassSkill;
    private Skill PriestClassSkill;
    private Skill NinjaClassSkill;

    // 직업선택 UI
    [Header("[ 직업선택 UI ]")]
    public GameObject classSelectionUI;

    // 버튼
    [Header("[ 버튼 ]")]
    public Button warriorButton; // 워리어
    public Button mageButton; // 메이지
    public Button priestButton; // 프리스트
    public Button ninjaButton; // 닌자

    // 선택 여부 테두리
    [Header("[ 선택 여부 테두리 ]")]
    public GameObject warriorSelected; // 워리어
    public GameObject mageSelected; // 메이지
    public GameObject priestSelected; // 프리스트
    public GameObject ninjaSelected; // 닌자

    // 직업 스탯
    [Header("[ 직업 스탯 ]")]
    public TextMeshProUGUI classHealthText; // 체력
    public TextMeshProUGUI classManaText; // 마나
    public TextMeshProUGUI classSpeedText; // 이동 속도

    // 직업 스킬 정보
    [Header("[ 직업 스킬 정보 ]")]
    public TextMeshProUGUI classSkillNameText; // 직업스킬 이름 텍스트
    public Image classSkillIcon; // 직업스킬 아이콘
    public TextMeshProUGUI classSkillDescriptionText; // 직업스킬 설명 텍스트

    // 스킬 스탯
    [Header("[ 스킬 스탯 ]")]
    public GameObject warriorSkillPanel; // 전사 스킬 패널
    public GameObject ninjaMageSkillPanel; // 닌자 마법사 스킬 패널
    public GameObject priestSkillPanel; // 프리스트 패널
    public TextMeshProUGUI classSkillDurationText; // 스킬 지속시간 텍스트
    public TextMeshProUGUI classSkillDamageText; // 스킬 데미지 텍스트
    public TextMeshProUGUI classSkillHpRecoveryText; // 스킬 Hp 회복 텍스트
    public TextMeshProUGUI classSkillManaText; // 직업스킬 마나 텍스트
    public TextMeshProUGUI classSkillCoolDownText; // 직업스킬 쿨타임 텍스트


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    // 스크립트가 활성화되었을 때 호출되는 메서드
    private void Awake()
    {
        // 버튼에 워리어 선택 클릭 이벤트를 추가
        if (warriorButton != null)
            this.warriorButton.onClick.AddListener(HandleWarriorButtonClick);

        // 버튼에 메이지 선택 클릭 이벤트를 추가
        if (mageButton != null)
            this.mageButton.onClick.AddListener(HandleMageButtonClick);

        // 버튼에 프리스트 선택 클릭 이벤트를 추가
        if (priestButton != null)
            this.priestButton.onClick.AddListener(HandlePriestButtonClick);

        // 버튼에 닌자 선택 클릭 이벤트를 추가
        if (ninjaButton != null)
            this.ninjaButton.onClick.AddListener(HandleNinjaButtonClick);
    }

    void Update()
    {
        // 상호작용 정보 UI 업데이트
        UpdateClassSkillInfo();
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 워리어 선택 시 호출되는 메서드
    public void HandleWarriorButtonClick()
    {
        // 현재 선택 직업 업데이트
        selectedClassNum = 1;

        // 선택 테두리 활성화
        warriorSelected.SetActive(true);
        mageSelected.SetActive(false);
        priestSelected.SetActive(false);
        ninjaSelected.SetActive(false);
    }

    // 메이지 선택 시 호출되는 메서드
    public void HandleMageButtonClick()
    {
        // 현재 선택 직업 업데이트
        selectedClassNum = 2;

        // 선택 테두리 활성화
        warriorSelected.SetActive(false);
        mageSelected.SetActive(true);
        priestSelected.SetActive(false);
        ninjaSelected.SetActive(false);
    }

    // 프리스트 선택 시 호출되는 메서드
    public void HandlePriestButtonClick()
    {
        // 현재 선택 직업 업데이트
        selectedClassNum = 3;

        // 선택 테두리 활성화
        warriorSelected.SetActive(false);
        mageSelected.SetActive(false);
        priestSelected.SetActive(true);
        ninjaSelected.SetActive(false);
    }

    // 닌자 선택 시 호출되는 메서드
    public void HandleNinjaButtonClick()
    {
        // 현재 선택 직업 업데이트
        selectedClassNum = 4;

        // 선택 테두리 활성화
        warriorSelected.SetActive(false);
        mageSelected.SetActive(false);
        priestSelected.SetActive(false);
        ninjaSelected.SetActive(true);
    }

    // 직업스킬 정보를 UI에 표시
    void UpdateClassSkillInfo()
    {
        // 사용할 변수 선언
        Skill classSkill;

        // 선택된 직업에 따라 해당 클래스의 스킬 정보를 classSkill에 할당
        switch (selectedClassNum)
        {
            case 1: // 전사
                classSkill = WarriorClassSkill;
                break;
            case 2: // 메이지
                classSkill = MageClassSkill;
                break;
            case 3: // 사제
                classSkill = PriestClassSkill;
                break;
            case 4: // 닌자
                classSkill = NinjaClassSkill;
                break;
            default:
                classSkill = null;
                break;
        }

        Debug.Log($"{classSkill}@@@@@@@@@@@@@@@@@@@@@@@");

        // 직업스킬 이름 업데이트
        classSkillNameText.text = classSkill.Name;

        // 직업스킬 설명 업데이트
        classSkillDescriptionText.text = classSkill.Description;

        // 직업스킬 아이콘 업데이트
        classSkillIcon.sprite = classSkill.Icon;

        // 직업스킬 마나 텍스트 업데이트
        classSkillManaText.text = classSkill.RequiredMp.ToString();

        // 직업스킬 쿨타임 텍스트 업데이트
        classSkillCoolDownText.text = $"{classSkill.SkillCoolDownTime}s";

        if (classSkill is WarriorClassSkill)
        {
            // 전사 스킬 패널 활성화
            warriorSkillPanel.SetActive(true);
            ninjaMageSkillPanel.SetActive(false);
            priestSkillPanel.SetActive(false);

            // 지속 시간 업데이트
            WarriorClassSkill warriorClassSkill = (WarriorClassSkill)classSkill;
            float duration = warriorClassSkill.Duration;
            classSkillDurationText.text = duration.ToString();
        }
        else if (classSkill is PriestClassSkill)
        {
            // 프리스트 스킬 패널 활성화
            warriorSkillPanel.SetActive(false);
            ninjaMageSkillPanel.SetActive(false);
            priestSkillPanel.SetActive(true);

            // 회복량 업데이트
            PriestClassSkill priestClassSkill = (PriestClassSkill)classSkill;
            int healCount = priestClassSkill.HealCount;
            int healMount = priestClassSkill.HealMount;
            classSkillHpRecoveryText.text = $"{healCount}초간 초당 {healMount * 2} 회복";
        }
        else if (classSkill is NinjaClassSkill)
        {
            // 닌자, 마법사 스킬 패널 활성화
            warriorSkillPanel.SetActive(false);
            ninjaMageSkillPanel.SetActive(true);
            priestSkillPanel.SetActive(false);

            // 데미지 업데이트
            NinjaClassSkill ninjaClassSkill = (NinjaClassSkill)classSkill;
            int damage = ninjaClassSkill.Damage;
            classSkillDamageText.text = damage.ToString();
        }
        else if (classSkill is MageClassSkill)
        {
            // 닌자, 마법사 스킬 패널 활성화
            warriorSkillPanel.SetActive(false);
            ninjaMageSkillPanel.SetActive(true);
            priestSkillPanel.SetActive(false);

            // 데미지 업데이트
            MageClassSkill mageClassSkill = (MageClassSkill)classSkill;
            int damage = mageClassSkill.Damage;
            classSkillDamageText.text = damage.ToString();
        }
    }
}
