using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

/// <summary>
/// 두번째 아이템 스킬 위에 마우스를 올려서 아이템 정보 UI가 나오도록 하는 클래스입니다.
/// </summary>
public class secondItemSkillInfoOpen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 클래스가 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수
    private Item[] _playerInventory; // 플레이어의 인벤토리 배열
    public bool isSecondItemMouseHover = false;

    // 상호작용 UI
    [Header("[ 상호작용 UI ]")]
    public GameObject itemInfoUI;

    // 아이템 정보
    [Header("[ 아이템 정보 ]")]
    public TextMeshProUGUI itemNameText; // 아이템 이름 텍스트
    public TextMeshProUGUI itemTierText; // 아이템 등급 텍스트
    public TextMeshProUGUI itemFlavorText; // 아이템 플레이버 텍스트
    public Image itemIcon; // 아이템 아이콘

    // 스킬 정보
    [Header("[ 스킬 정보 ]")]
    public TextMeshProUGUI itemSkillNameText; // 스킬 이름 텍스트
    public TextMeshProUGUI itemSkillDescriptionText; // 스킬 등급 텍스트
    public Image itemSkillIcon; // 스킬 스킬 아이콘
    public GameObject skillIcon; // 스킬 아이콘
    public GameObject skillNoneIcon; // 스킬 없음 아이콘

    // 스킬 스텟
    [Header("[ 스킬 스텟 ]")]
    public GameObject damagePanel; // 데미지 패널
    public GameObject hpRecoveryPanel; // 회복 패널
    public GameObject mpRecoveryPanel; // 회복 패널
    public TextMeshProUGUI itemSkillDamageText; // 스킬 데미지 텍스트
    public TextMeshProUGUI itemSkillHpRecoveryText; // 스킬 회복 텍스트
    public TextMeshProUGUI itemSkillMpRecoveryText; // 스킬 회복 텍스트
    public TextMeshProUGUI itemSkillManaText; // 스킬 마나 텍스트
    public TextMeshProUGUI itemSkillCoolDownText; // 스킬 쿨타임 텍스트


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Update()
    {
        // 상호작용 정보 UI 업데이트
        UpdateInteractionInfoUI();
    }

    // 마우스가 UI 요소 위에 올라왔을 때 호출될 메서드
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemInfoUI != null)
        {
            itemInfoUI.SetActive(true); // 게임 오브젝트 활성화
            isSecondItemMouseHover = true;
        }
    }

    // 마우스가 UI 요소에서 벗어났을 때 호출될 메서드
    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemInfoUI != null)
        {
            itemInfoUI.SetActive(false); // 게임 오브젝트 비활성화
            isSecondItemMouseHover = false;
        }
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 상호작용 정보 UI를 업데이트하는 메서드
    void UpdateInteractionInfoUI()
    {
        // PlayerController 컴포넌트를 찾아서 참조
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때

        // PlayerController의 인벤토리에 접근
        _playerInventory = _playerController.Inventory;

        // 두번째 아이템 정보를 가져옴
        Item secondtItem = _playerInventory[2];

        // 스킬 존재 여부 확인
        bool isCurrentItemSkillExists = secondtItem.RightSkill != null && !(secondtItem.RightSkill is EmptySkill);

        if (isSecondItemMouseHover)
        {
            UpdateItemInfo(secondtItem);

            if (isCurrentItemSkillExists)
            {
                skillIcon.SetActive(true);
                skillNoneIcon.SetActive(false);
                UpdateItemSkillInfo(secondtItem);
            }
            else
            {
                skillIcon.SetActive(false);
                skillNoneIcon.SetActive(true);
                UpdateNoneSkillInfo();
            }
        }
    }

    // 아이템 정보를 UI에 표시
    void UpdateItemInfo(Item item)
    {
        // 아이템 등급 업데이트 및 아이템 등급에 따라 아이템 이름 색 변화
        string colorHex = "";
        switch (item.Tier)
        {
            case ItemTier.COMMON:
                colorHex = "#BFBFBF";
                itemTierText.text = "등급: 일반";
                break;
            case ItemTier.UNCOMMON:
                colorHex = "#1AAC9C";
                itemTierText.text = "등급: 고급";
                break;
            case ItemTier.RARE:
                colorHex = "#3498DB";
                itemTierText.text = "등급: 레어";
                break;
            case ItemTier.EPIC:
                colorHex = "#9B59B6";
                itemTierText.text = "등급: 희귀";
                break;
            case ItemTier.LEGENDARY:
                colorHex = "#F1C40F";
                itemTierText.text = "등급: 전설";
                break;
            case ItemTier.BOSS:
                colorHex = "#E74C3C";
                itemTierText.text = "등급: 보스";
                break;
            default:
                colorHex = "#FFFFFF"; // 기본 값으로 흰색 반환
                break;
        }

        // 아이템 이름 업데이트
        itemNameText.text = $"<color={colorHex}>{item.Name}</color>";

        // 아이템 플레이버 텍스트 업데이트
        itemFlavorText.text = item.FlavorText.ToString();

        // 아이템 아이콘 업데이트
        itemIcon.sprite = item.Icon;
    }

    // 아이템 스킬 정보를 UI에 표시
    void UpdateItemSkillInfo(Item item)
    { 
        // 아이템 스킬 이름 업데이트
        itemSkillNameText.text = item.RightSkill.Name.ToString();

        // 아이템 스킬 설명 업데이트
        itemSkillDescriptionText.text = item.RightSkill.Description.ToString();

        // 아이템 스킬 아이콘 업데이트
        itemSkillIcon.sprite = item.RightSkill.Icon;

        // 아이템 스킬 데미지/회복 패널 및 텍스트 업데이트
        if (item.RightSkill is ConsumingSkill)
        {
            // 데미지 패널 비활성화
            damagePanel.SetActive(false);

            Debug.Log($"회복 아이템임 {item.RightSkill}");

            if (item.RightSkill is BibimbapSkill)
            {
                // Hp 회복 패널 활성화
                hpRecoveryPanel.SetActive(true);
                mpRecoveryPanel.SetActive(false);

                // 회복량 업데이트
                BibimbapSkill bibimbapSkill = (BibimbapSkill)item.RightSkill;
                int hpRecoveryAmount = bibimbapSkill.HpRecoveryAmount;
                itemSkillHpRecoveryText.text = hpRecoveryAmount.ToString();
            }
            else if (item.RightSkill is RareSteakSkill)
            {
                // Hp 회복 패널 활성화
                hpRecoveryPanel.SetActive(true);
                mpRecoveryPanel.SetActive(false);

                // 회복량 업데이트
                RareSteakSkill rareSteakSkill = (RareSteakSkill)item.RightSkill;
                int hpRecoveryAmount = rareSteakSkill.HpRecoveryAmount;
                itemSkillHpRecoveryText.text = hpRecoveryAmount.ToString();
            }
            else if (item.RightSkill is BoredAppleSkill)
            {
                // Mp 회복 패널 활성화
                hpRecoveryPanel.SetActive(false);
                mpRecoveryPanel.SetActive(true);

                // 회복량 업데이트
                BoredAppleSkill boredAppleSkill = (BoredAppleSkill)item.RightSkill;
                float recoveryPeriod = boredAppleSkill.RecoveryPeriod;
                int mpRecoveryAmountPerPeriod = boredAppleSkill.MpRecoveryAmountPerPeriod;
                itemSkillMpRecoveryText.text = $"{mpRecoveryAmountPerPeriod / recoveryPeriod}/s";
            }
            else if (item.RightSkill is CucumberSkill)
            {
                // Mp 회복 패널 활성화
                hpRecoveryPanel.SetActive(false);
                mpRecoveryPanel.SetActive(true);

                // 회복량 업데이트
                CucumberSkill cucumberSkill = (CucumberSkill)item.RightSkill;
                int mpRecoveryAmount = cucumberSkill.MpRecoveryAmount;
                itemSkillMpRecoveryText.text = mpRecoveryAmount.ToString();
            }
        }
        else if (item.RightSkill is HealWandSkill)
        {
            // Hp 회복 패널 활성화
            damagePanel.SetActive(false);
            hpRecoveryPanel.SetActive(true);
            mpRecoveryPanel.SetActive(false);

            // 회복량 업데이트
            HealWandSkill healWandSkill = (HealWandSkill)item.RightSkill;
            int hpRecoveryAmount = healWandSkill.HpRecoveryAmount;
            itemSkillHpRecoveryText.text = hpRecoveryAmount.ToString();
        }
        else if (item.RightSkill is IAttackSkill)
        {
            // 데미지 패널 활성화
            damagePanel.SetActive(true);
            hpRecoveryPanel.SetActive(false);
            mpRecoveryPanel.SetActive(false);

            IAttackSkill attackSkill = (IAttackSkill)item.RightSkill;
            int damage = attackSkill.Damage;
            itemSkillDamageText.text = damage.ToString();
        }

        // 아이템 스킬 마나 텍스트 업데이트
        itemSkillManaText.text = item.RightSkill.RequiredMp.ToString();

        // 아이템 스킬 쿨타임 텍스트 업데이트
        itemSkillCoolDownText.text = $"{item.RightSkill.SkillCoolDownTime}s";
    }

    // 스킬 없는 아이템 정보 업데이트
    void UpdateNoneSkillInfo()
    {
        // 아이템 스킬 이름 업데이트
        itemSkillNameText.text = "스킬 없음";

        // 아이템 스킬 설명 업데이트
        itemSkillDescriptionText.text = "눈에 띄는 스킬은 없을지 몰라도, 이 아이템에는 숨겨진 재능이 있을지도 몰라요. 마치 당신처럼 말이죠.";

        // 아이템 스킬 아이콘 업데이트
        itemSkillIcon.sprite = null;

        // 데미지 패널 활성화
        damagePanel.SetActive(true);
        hpRecoveryPanel.SetActive(false);
        mpRecoveryPanel.SetActive(false);

        itemSkillDamageText.text = "-";

        // 아이템 스킬 마나 텍스트 업데이트
        itemSkillManaText.text = "-";

        // 아이템 스킬 쿨타임 텍스트 업데이트
        itemSkillCoolDownText.text = "-";
    }
}
