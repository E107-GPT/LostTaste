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
    public TextMeshProUGUI nameText; // 이름 텍스트


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
            // 상호작용 가능한 대상이 감지되면 UI 업데이트
            interactionUI.SetActive(true);
            DisplayInteractableInfo();
        }
        else
        {
            // 감지된 대상이 없으면 UI 비활성화 및 텍스트 초기화
            interactionUI.SetActive(false);
            nameText.text = "";
        }
    }

    // 상호작용 가능한 대상에 따라 UI에 정보를 표시
    void DisplayInteractableInfo()
    {
        if (_detectedInteractable is Item)
        {
            // 아이템일 경우 아이템 정보 표시
            DisplayItemInfo(_detectedInteractable as Item);
        }
        else if (_detectedInteractable is RandomItemChest)
        {
            // 아이템 상자일 경우 상자 정보 표시
            DisplayChestInfo(_detectedInteractable as RandomItemChest);
        }
    }

    // 아이템 정보를 UI에 표시
    void DisplayItemInfo(Item item)
    {
        string colorHex = "";

        switch (item.Tier)
        {
            case ItemTier.COMMON:
                colorHex = "#BFBFBF";
                break;
            case ItemTier.UNCOMMON:
                colorHex = "#1AAC9C";
                break;
            case ItemTier.RARE:
                colorHex = "#3498DB";
                break;
            case ItemTier.EPIC:
                colorHex = "#9B59B6";
                break;
            case ItemTier.LEGENDARY:
                colorHex = "#F1C40F";
                break;
            case ItemTier.BOSS:
                colorHex = "#E74C3C";
                break;
            default:
                colorHex = "#FFFFFF"; // 기본 값으로 흰색 반환
                break;
        }

        nameText.text = $"<color={colorHex}>{item.Name}</color>";
    }

    // 아이템 상자 정보를 UI에 표시
    void DisplayChestInfo(RandomItemChest itemChest)
    {
        string chestTypeName = "";
        string colorHex = "";

        switch (itemChest.ChestType)
        {
            case ItemChestType.WOODEN:
                chestTypeName = "허름한 나무 상자";
                colorHex = "#FFFFFF"; // 흰색
                break;
            case ItemChestType.BETTER:
                chestTypeName = "튼튼한 나무 상자";
                colorHex = "#4682B4"; // 청색
                break;
            case ItemChestType.GOLDEN:
                chestTypeName = "찬란한 황금 상자";
                colorHex = "#FFD700"; // 금색
                break;
            default:
                break;
        }

        nameText.text = $"<color={colorHex}>{chestTypeName}</color>";
    }
}
