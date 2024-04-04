using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


/// <summary>
/// 아이템 스킬 정보 UI가 나오도록 하는 클래스입니다.
/// </summary>
public class ClassSkillInfoOpen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수
    private PlayerClass _playerClass; // 플레이어의 직업

    // 직업스킬 UI
    [Header("[ 직업스킬 UI ]")]
    public GameObject classSkillInfoUI;

    // 직업 스킬 정보
    [Header("[ 직업 스킬 정보 ]")]
    public TextMeshProUGUI classSkillNameText; // 직업스킬 이름 텍스트
    public Image classSkillIcon; // 직업스킬 아이콘
    public Image classSkillIconInterface; // 직업스킬 아이콘 (인터페이스용)
    public TextMeshProUGUI classSkillDescriptionText; // 직업스킬 설명 텍스트

    // 스킬 스텟
    [Header("[ 스킬 스텟 ]")]
    public GameObject warriorSkillPanel; // 전사 스킬 패널
    public GameObject ninjaMageSkillPanel; // 닌자 마법사 스킬 패널
    public GameObject priestSkillPanel; // 프리스트 패널
    public TextMeshProUGUI classSkillDurationText; // 스킬 지속시간 텍스트
    public TextMeshProUGUI classSkillDamageText; // 스킬 데미지 텍스트
    public TextMeshProUGUI classSkillHpRecoveryText; // 스킬 Hp 회복 텍스트
    public TextMeshProUGUI classSkillManaText; // 직업스킬 마나 텍스트
    public TextMeshProUGUI classSkillCoolDownText; // 직업스킬 쿨타임 텍스트


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Update()
    {
        // 상호작용 정보 UI 업데이트
        UpdateClassSkillInfoUI();
    }

    // 마우스가 UI 요소 위에 올라왔을 때 호출될 메서드
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (classSkillInfoUI != null)
        {
            classSkillInfoUI.SetActive(true); // 게임 오브젝트 활성화
        }
    }

    // 마우스가 UI 요소에서 벗어났을 때 호출될 메서드
    public void OnPointerExit(PointerEventData eventData)
    {
        if (classSkillInfoUI != null)
        {
            classSkillInfoUI.SetActive(false); // 게임 오브젝트 비활성화
        }
    }

    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 상호작용 정보 UI를 업데이트하는 메서드
    void UpdateClassSkillInfoUI()
    {
        // PlayerController 컴포넌트를 찾아서 참조
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때

        // 현재 플레이어의 직업 정보 가져오기
        _playerClass = _playerController.PlayerClass;

        // 직업스킬 UI 업데이트
        if (_playerClass.ClassSkill is not EmptySkill)
            UpdateClassSkillInfo();
    }

    // 직업스킬 정보를 UI에 표시
    void UpdateClassSkillInfo()
    {
        // 사용할 변수 선언
        Skill classSkill = _playerClass.ClassSkill;

        // 직업스킬 이름 업데이트
        classSkillNameText.text = classSkill.Name;

        // 직업스킬 설명 업데이트
        classSkillDescriptionText.text = classSkill.Description;

        // 직업스킬 아이콘 업데이트
        classSkillIcon.sprite = classSkill.Icon;
        classSkillIconInterface.sprite = classSkill.Icon;

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
            classSkillHpRecoveryText.text = $"{healCount / 2}초간 초당 {healMount * 2} 회복";
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
