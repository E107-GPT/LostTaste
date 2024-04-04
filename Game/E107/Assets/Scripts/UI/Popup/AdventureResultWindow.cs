using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 모험이 종료되면 팝업되는 결과창을 관리하는 컴포넌트입니다.
/// </summary>
public class AdventureResultWindow : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 아이템 스킬 쿨타임 UI 매니저가 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수
    private Item[] _playerInventory; // 플레이어의 인벤토리 배열

    // 아이템
    [Header("[ 아이템 ]")]
    public Image firstItemIcon; // 아이템 1 아이콘
    public Image secondItemIcon; // 아이템 2 아이콘

    // 게임 시간
    [Header("[ 게임 시간 ]")]
    public TextMeshProUGUI gameTimeText; // 게임 시간 텍스트

    // 플레이 인원 수
    [Header("[ 플레이 인원 수 ]")]
    public TextMeshProUGUI playerCountText; // 플레이 인원 수 텍스트

    // ------------------------------------------------ Life Cycle ------------------------------------------------
    void Start()
    {
        int currentPartyCount = DungeonEntrance.Instance.currentPartyMemberCount;

        string colorHex = "";

        switch (currentPartyCount)
        {
            case 1:
                colorHex = "#E74C3C";
                playerCountText.text = $"<color={colorHex}>{currentPartyCount}인 플레이!!!</color>";
                break;
            case 2:
                colorHex = "#9B59B6";
                playerCountText.text = $"<color={colorHex}>{currentPartyCount}인 플레이!!</color>";
                break;
            case 3:
                colorHex = "#3498DB";
                playerCountText.text = $"<color={colorHex}>{currentPartyCount}인 플레이!</color>";
                break;
            case 4:
                colorHex = "#BFBFBF";
                playerCountText.text = $"<color={colorHex}>{currentPartyCount}인 플레이</color>";
                break;
            default:
                colorHex = "#FFFFFF"; // 기본 값으로 흰색 반환
                playerCountText.text = $"<color={colorHex}>몇명이서 한거에요??</color>";
                break;
        }
    }

    void OnEnable()
    {
        float gameTime = DungeonEntrance.Instance.GameTime;
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        gameTimeText.text = string.Format("{0:0}분 {1:00}초", minutes, seconds);
    }

    void Update()
    {
        UpdateInventory();
    }

    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 인벤토리를 업데이트하는 메서드
    void UpdateInventory()
    {
        // PlayerController 컴포넌트를 찾아서 참조
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때

        // PlayerController의 인벤토리에 접근
        _playerInventory = _playerController.Inventory;

        // 아이템 정보 업데이트
        UpdateItemIcon(firstItemIcon, _playerInventory[1]);
        UpdateItemIcon(secondItemIcon, _playerInventory[2]);

        // 플레이어의 HP가 0 이하일 경우 업데이트 중지
        if (_playerController.Stat.Hp <= 0) return;
    }

    // 아이템 아이콘 업데이트 메서드
    void UpdateItemIcon(Image itemIcon, Item item)
    {
        // 아이템 아이콘 업데이트
        itemIcon.sprite = item.Icon;
    }

}