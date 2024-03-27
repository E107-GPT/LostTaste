using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 아이템 근처에서 상호작용 UI가 나오도록 하는 클래스입니다.
/// </summary>
public class ItemInteraction : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 아이템 인터렉션 클래스가 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수
    private IPlayerInteractable _detectedInteractable; // 플레이어 접촉 상호작용

    // 상호작용 UI
    [Header("[ 상호작용 UI ]")]
    public GameObject interactionUI;
    public TextMeshProUGUI itemName; // 아이템 이름


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Update()
    {
        // 상호작용 UI 업데이트
        UpdateInteractionUI();
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 인터렉션 UI를 업데이트하는 메서드
    void UpdateInteractionUI()
    {
        // PlayerController 컴포넌트를 찾아서 참조
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때

        // PlayerController의 인터렉션에 접근
        _detectedInteractable = _playerController.DetectedInteractable;

        if (_detectedInteractable != null)
        {
            // UI 활성화
            interactionUI.SetActive(true);

            // 접촉중인 아이템 가져옴
            Item item = _detectedInteractable as Item;

            if (item != null)
            {
                // 아이템의 상세 정보를 UI에 표시
                itemName.text = $"<color={GetTierColor(item.Tier)}>{item.Name}</color>"; // 아이템 이름에 색상 적용
            }
        }
        else
        {
            // UI 비활성화
            interactionUI.SetActive(false);
            itemName.text = "";
        }

        Debug.Log($"이름@@@@@@@@@@@@{_detectedInteractable}");
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
