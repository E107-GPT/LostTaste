using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 아이템 근처에서 상호작용 UI가 나오도록 하는 클래스입니다.
/// </summary>
public class ItemInteraction : MonoBehaviour
{
    // 상호작용 UI
    [Header("[ 상호작용 UI ]")]
    public GameObject interactionUI;
    public TextMeshProUGUI itemName; // 아이템 이름

    private Item currentItem; // 현재 상호작용 중인 아이템

    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Update()
    {
        // 상호작용 UI 업데이트
        UpdateInteractionUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 캐릭터가 아이템 영역 안으로 들어옴
        {
            // 부딪힌 아이템의 정보를 가져옴
            currentItem = other.GetComponent<Item>();
            if (currentItem != null)
            {
                interactionUI.SetActive(true); // 상호작용 UI 활성화
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && currentItem != null) // 캐릭터가 아이템 영역을 벗어남
        {
            interactionUI.SetActive(false); // 상호작용 UI 비활성화
            currentItem = null; // 현재 상호작용 중인 아이템 정보 초기화
        }
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 인벤토리를 업데이트하는 메서드
    void UpdateInteractionUI()
    {
        if (currentItem != null)
        {
            itemName.text = $"<color={GetTierColor(currentItem.Tier)}>{currentItem.Name}</color>"; // 아이템 이름에 색상 적용
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
}
