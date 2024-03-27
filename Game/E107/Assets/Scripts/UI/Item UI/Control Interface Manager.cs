using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 컨트롤 인터페이스 매니저는 아이템 스킬을 표시하는 기능을 제공합니다.
/// </summary>
public class ControlInterfaceManager : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 컨트롤 인터페이스 매니저가 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수
    private Item[] _playerInventory; // 플레이어의 인벤토리 배열
    private int _currentItemNum; // 현재 장착한 무기

    // 아이템 1
    [Header("[ 아이템 1 ]")]
    public GameObject firstItemRightSkillPanel; // 아이템 1 오른쪽 스킬 패널
    public Image firstItemRightSkillIcon; // 아이템 1 오른쪽 스킬 아이콘

    // 아이템 2
    [Header("[ 아이템 2 ]")]
    public GameObject secondItemRightSkillPanel; // 아이템 2 오른쪽 스킬 패널
    public Image secondItemRightSkillIcon; // 아이템 2 오른쪽 스킬 아이콘

    // 스킬 없음 아이콘
    [Header("[ 스킬 없음]")]
    public GameObject skillNonePanel; // 스킬 없음 패널


    // ------------------------------------------------ Life Cylce ------------------------------------------------

    void Update()
    {
        // 컨트롤 인터페이스 업데이트
        UpdateControlInterface();
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 컨트롤 인터페이스를 업데이트하는 메서드
    void UpdateControlInterface()
    {
        // PlayerController 컴포넌트를 찾아서 참조
        _playerController = GameObject.FindObjectOfType<PlayerController>();

        if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때

        // PlayerController의 인벤토리에 접근
        _playerInventory = _playerController.Inventory;
        _currentItemNum = _playerController.CurrentItemNum;

        // PlayerController의 인벤토리와 현재 아이템 번호를 가져옴
        Item firstItem = _playerInventory[1];
        Item secondItem = _playerInventory[2];

        // 스킬 존재 여부 확인
        bool isFirstItemSkillExists = !float.IsInfinity(firstItem.RightSkill.SkillCoolDownTime);
        bool isSecondItemSkillExists = !float.IsInfinity(secondItem.RightSkill.SkillCoolDownTime);

        // 스킬 아이콘 업데이트
        UpdateSkillIcon(firstItem, firstItemRightSkillIcon, isFirstItemSkillExists);
        UpdateSkillIcon(secondItem, secondItemRightSkillIcon, isSecondItemSkillExists);

        // 무기 교체에 따른 스킬 패널 업데이트
        ToggleSkillPanels(_currentItemNum, isFirstItemSkillExists, isSecondItemSkillExists);
    }

    // 스킬 아이콘을 업데이트하는 메서드
    void UpdateSkillIcon(Item item, Image skillIcon, bool isSkillExists)
    {
        if (isSkillExists) skillIcon.sprite = item.RightSkill.Icon;
    }

    // 현재 선택된 아이템에 따라 패널을 토글하는 메서드, 스킬 존재 여부를 고려
    void ToggleSkillPanels(int currentItemNum, bool isFirstItemSkillExists, bool isSecondItemSkillExists)
    {
        bool isSkillPanelActive = false;

        if (currentItemNum == 1 && isFirstItemSkillExists)
        {
            firstItemRightSkillPanel.SetActive(true);
            secondItemRightSkillPanel.SetActive(false);
            isSkillPanelActive = true;
        }
        else if (currentItemNum == 2 && isSecondItemSkillExists)
        {
            firstItemRightSkillPanel.SetActive(false);
            secondItemRightSkillPanel.SetActive(true);
            isSkillPanelActive = true;
        }

        skillNonePanel.SetActive(!isSkillPanelActive);
    }
}