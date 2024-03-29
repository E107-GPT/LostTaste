using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using UnityEditor.Experimental.GraphView;

/// <summary>
/// 아이템 스킬 쿨타임 UI 매니저는 쿨타임이 있는 아이템 스킬의 쿨타임을 표시하는 기능을 제공합니다.
/// </summary>
public class ItemSkillCooldownUIManager : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 아이템 스킬 쿨타임 UI 매니저가 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수
    private Item[] _playerInventory; // 플레이어의 인벤토리 배열
    private int _currentItemNum; // 현재 장착한 무기

    // 아이템 1
    [Header("[ 아이템 1 ]")]
    public GameObject firstItemRightSkillCoolDownPanel; // 아이템 1 오른쪽 스킬 쿨타임 패널
    public Image firstItemCoolDownImage; // 아이템 1 오른쪽 스킬 쿨타임 이미지
    public Image firstItemKeyImage; // 아이템 1 오른쪽 스킬 키 이미지
    public TextMeshProUGUI firstItemRightSkillCoolDownText; // 아이템 1 오른쪽 스킬 쿨타임

    // 아이템 2
    [Header("[ 아이템 2 ]")]
    public GameObject secondItemRightSkillCoolDownPanel; // 아이템 2 오른쪽 스킬 쿨타임 패널
    public Image secondItemCoolDownImage; // 아이템 2 오른쪽 스킬 쿨타임 이미지
    public Image secondItemKeyImage; // 아이템 2 오른쪽 스킬 키 이미지
    public TextMeshProUGUI secondItemRightSkillCoolDownText; // 아이템 2 오른쪽 스킬 쿨타임

    // 남은 쿨타임 숫자 변수 선언
    private float firstItemRightSkillCoolDown; // 아이템 1 오른쪽 스킬 현재 쿨타임
    private float secondItemRightSkillCoolDown; // 아이템 2 오른쪽 스킬 현재 쿨타임

    private bool _isCurrentItemCoolingDownPrev = false;


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Start()
    {
        // 초기 Fill Amount를 0으로 설정
        firstItemCoolDownImage.fillAmount = 0;
        secondItemCoolDownImage.fillAmount = 0;
        firstItemKeyImage.fillAmount = 0;
        secondItemKeyImage.fillAmount = 0;

        // 초기 쿨타임 텍스트 빈 문자열로 설정
        firstItemRightSkillCoolDownText.text = "";
        secondItemRightSkillCoolDownText.text = "";
    }

    void Update()
    {
        // 쿨타임 패널 업데이트
        UpdateItemCoolDownPanel();
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    void UpdateItemCoolDownPanel()
    {
        // PlayerController 컴포넌트를 찾아서 참조
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때

        // PlayerController의 인벤토리에 접근
        _playerInventory = _playerController.Inventory;

        Item currentItem = _playerController.Inventory[_playerController.CurrentItemNum];

        // 스킬 존재 여부 확인
        bool isCurrentItemSkillExists = currentItem.RightSkill != null && !(currentItem.RightSkill is EmptySkill);

        bool isCurrentItemCoolingDown = !currentItem.RightSkill.IsPlayerCastable(_playerController);

        if (isCurrentItemCoolingDown && isCurrentItemSkillExists)
        {
            UpdateItemSkillCoolDown(currentItem);
        }
        else if (!isCurrentItemCoolingDown && _isCurrentItemCoolingDownPrev)
        {
            ResetCoolDownUI(currentItem);
        }

        // 무기 교체에 따른 스킬 쿨타임 패널 업데이트
        ToggleSkillCoolDownPanels(_currentItemNum);

        // 아이템 변경 또는 버림 감지
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.B))
        {
            // 캐릭터가 '대기 상태' 또는 '걷기 상태'가 아닐 경우 함수를 빠져나감
            if (!(_playerController.CurState is IdleState || _playerController.CurState is MoveState)) return;
        }

        _isCurrentItemCoolingDownPrev = isCurrentItemCoolingDown;
    }

    void UpdateItemSkillCoolDown(Item item)
    {
        float remainingTime = item.RightSkill.SkillCoolDownTime - (Time.time - item.RightSkill.LastCastTime);
        float percentage = remainingTime / item.RightSkill.SkillCoolDownTime;
        string remainingTimeString;
        if (remainingTime > 1.0f)
        {
            remainingTimeString = Mathf.Ceil(remainingTime).ToString() + "s";
        }
        else
        {
            remainingTimeString = string.Format("{0:0.0}", remainingTime) + "s";
        }

        UpdateCoolDownUI(item, percentage, remainingTimeString);
    }

    // 쿨다운 UI 업데이트 메서드 (새로 추가)
    void UpdateCoolDownUI(Item item, float fillAmount, string text)
    {
        if (item == _playerInventory[1])
        {
            firstItemRightSkillCoolDownText.text = text;
            firstItemCoolDownImage.fillAmount = fillAmount;
            firstItemKeyImage.fillAmount = fillAmount;
        }
        else if (item == _playerInventory[2])
        {
            secondItemRightSkillCoolDownText.text = text;
            secondItemCoolDownImage.fillAmount = fillAmount;
            secondItemKeyImage.fillAmount = fillAmount;
        }
    }

    // 코루틴이 끝난 뒤 쿨타임 패널을 초기화 하는 메서드
    void ResetCoolDownUI(Item item)
    {
        if (item == _playerInventory[1])
        {
            firstItemRightSkillCoolDownText.text = "";
            firstItemCoolDownImage.fillAmount = 0;
            firstItemKeyImage.fillAmount = 0;
        }
        else if (item == _playerInventory[2])
        {
            secondItemRightSkillCoolDownText.text = "";
            secondItemCoolDownImage.fillAmount = 0;
            secondItemKeyImage.fillAmount = 0;
        }
    }

    // 현재 선택된 아이템에 따라 쿨타임 패널을 토글하는 메서드
    void ToggleSkillCoolDownPanels(int currentItemNum)
    {
        if (currentItemNum == 1)
        {
            firstItemRightSkillCoolDownPanel.SetActive(true);
            secondItemRightSkillCoolDownPanel.SetActive(false);
        }
        else if (currentItemNum == 2)
        {
            firstItemRightSkillCoolDownPanel.SetActive(false);
            secondItemRightSkillCoolDownPanel.SetActive(true);
        }
    }
}