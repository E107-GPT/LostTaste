using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 아이템 근처에서 아이템 정보 UI가 나오도록 하는 클래스입니다.
/// </summary>
public class ItemInteractionInfoOpen : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 아이템 인터렉션 클래스가 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수
    private IPlayerInteractable _detectedInteractable; // 플레이어 접촉 상호작용

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


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 상호작용 정보 UI를 업데이트하는 메서드
    void UpdateInteractionInfoUI()
    {
        // PlayerController 컴포넌트를 찾아서 참조
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때

        // PlayerController의 인터렉션에 접근
        _detectedInteractable = _playerController.DetectedInteractable;

        if (_detectedInteractable != null && _detectedInteractable is Item)
        {
            // 상호작용 가능한 대상이 감지되면 UI 업데이트
            itemInfoUI.SetActive(true);
            UpdateItemInfo(_detectedInteractable as Item);
            UpdateItemSkillInfo(_detectedInteractable as Item);
        }
        else
        {
            // 감지된 대상이 없으면 UI 비활성화
            itemInfoUI.SetActive(false);
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
            // 회복 패널 활성화
            damagePanel.SetActive(false);
            hpRecoveryPanel.SetActive(true);
            mpRecoveryPanel.SetActive(true);

            Debug.Log($"회복 아이템임 {item.RightSkill}");
            //itemSkillHpRecoveryText.text = item.RightSkill.HpRecoveryAmount.ToString();
            //itemSkillMpRecoveryText.text = item.RightSkill.MpRecoveryAmount.ToString();
        }
        else
        {
            // 데미지 패널 활성화
            damagePanel.SetActive(true);
            hpRecoveryPanel.SetActive(false);
            mpRecoveryPanel.SetActive(false);

            Debug.Log($"공격 아이템임 {item.RightSkill}");
            //itemSkillDamageText.text = item.RightSkill.Damage.ToString();
        }

        // 아이템 스킬 마나 텍스트 업데이트
        itemSkillManaText.text = item.RightSkill.RequiredMp.ToString();

        // 아이템 스킬 쿨타임 텍스트 업데이트
        itemSkillCoolDownText.text = $"{item.RightSkill.SkillCoolDownTime}s";
    }
}
