using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 인벤토리 매니저는 플레이어의 인벤토리를 관리하고 그 내용을 확인하는 기능을 제공합니다.
/// 플레이어 컨트롤러를 찾아서 인벤토리에 접근하며, 첫 번째와 두 번째 아이템의 정보를 활용합니다.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 인벤토리 매니저가 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수
    private Item[] _playerInventory; // 플레이어의 인벤토리 배열
    private int _currentItemNum; // 현재 장착한 무기

    // 아이템 1
    [Header("[ 아이템 1 ]")]
    public Image firstItemIcon; // 아이템 1 아이콘
    public GameObject firstItemUsed; // 아이템 1 사용 여부
    public TextMeshProUGUI firstItemName; // 아이템 1 이름
    public TextMeshProUGUI firstItemRightSkillMana; // 아이템 1 오른쪽 스킬 마나
    public TextMeshProUGUI firstItemRightSkillCoolDown; // 아이템 1 오른쪽 스킬 쿨타임

    // 아이템 2
    [Header("[ 아이템 2 ]")]
    public Image secondItemIcon; // 아이템 2 아이콘
    public GameObject secondItemUsed; // 아이템 2 사용 여부
    public TextMeshProUGUI secondItemName; // 아이템 2 이름
    public TextMeshProUGUI secondItemRightSkillMana; // 아이템 2 오른쪽 스킬 마나
    public TextMeshProUGUI secondItemRightSkillCoolDown; // 아이템 2 오른쪽 스킬 쿨타임


    // ------------------------------------------------ Life Cylce ------------------------------------------------

    void Update()
    {
        // 인벤토리 업데이트
        UpdateInventory();

        // 사용중인 아이템 강조 표시
        ItemChange();
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 인벤토리를 업데이트하는 메서드
    void UpdateInventory()
    {
        // PlayerController 컴포넌트를 찾아서 참조
        _playerController = GameObject.FindObjectOfType<PlayerController>();

        if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때

        // PlayerController의 인벤토리에 접근
        _playerInventory = _playerController.Inventory;
        _currentItemNum = _playerController.CurrentItemNum;

        // 아이템 정보 업데이트
        UpdateItemUI(firstItemIcon, firstItemName, firstItemRightSkillMana, firstItemRightSkillCoolDown, _playerInventory[1]);
        UpdateItemUI(secondItemIcon, secondItemName, secondItemRightSkillMana, secondItemRightSkillCoolDown, _playerInventory[2]);
    }

    // 아이템 UI 업데이트 메서드
    void UpdateItemUI(Image itemIcon, TextMeshProUGUI itemName, TextMeshProUGUI skillMana, TextMeshProUGUI skillCoolDown, Item item)
    {
        // 아이템 아이콘 및 이름 업데이트
        itemIcon.sprite = item.Icon;
        itemName.text = $"<color={GetTierColor(item.Tier)}>{item.Name}</color>"; // 아이템 이름에 색상 적용

        // 스킬 정보 업데이트
        if (float.IsInfinity(item.RightSkill.SkillCoolDownTime))
        {
            skillMana.text = "-";
            skillCoolDown.text = "-";
        }
        else
        {
            skillMana.text = item.RightSkill.RequiredMp.ToString();
            skillCoolDown.text = $"{item.RightSkill.SkillCoolDownTime}s";
        }
    }

    // 티어에 따른 색상 반환하는 메서드
    string GetTierColor(ItemTier tier)
    {
        switch (tier)
        {
            case ItemTier.COMMON:
                return "#BFBFBF";
            case ItemTier.UNCOMMON:
                return "#1AAC9C";
            case ItemTier.RARE:
                return "#3498DB";
            case ItemTier.EPIC:
                return "#9B59B6";
            case ItemTier.LEGENDARY:
                return "#F1C40F";
            case ItemTier.BOSS:
                return "#E74C3C";
            default:
                return "#FFFFFF"; // 기본 값으로 흰색 반환
        }
    }

    // 사용 중인 아이템에 강조표시를 하는 메서드
    void ItemChange()
    {
        // 무기 교체
        if (_currentItemNum == 1)
        {
            firstItemUsed.SetActive(true);
            secondItemUsed.SetActive(false);
        }
        else if (_currentItemNum == 2)
        {
            firstItemUsed.SetActive(false);
            secondItemUsed.SetActive(true);
        }
    }
}