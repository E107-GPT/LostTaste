using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI firstItemRightSkillCoolDownText; // 아이템 1 오른쪽 스킬 쿨타임

    // 아이템 2
    [Header("[ 아이템 2 ]")]
    public GameObject secondItemRightSkillCoolDownPanel; // 아이템 2 오른쪽 스킬 쿨타임 패널
    public Image secondItemCoolDownImage; // 아이템 2 오른쪽 스킬 쿨타임 이미지
    public TextMeshProUGUI secondItemRightSkillCoolDownText; // 아이템 2 오른쪽 스킬 쿨타임

    // 쿨타임 숫자 변수 선언
    private float firstItemRightSkillCoolDown; // 아이템 1 오른쪽 스킬 쿨타임
    private float secondItemRightSkillCoolDown; // 아이템 2 오른쪽 스킬 쿨타임


    // ------------------------------------------------ Life Cylce ------------------------------------------------

    void Start()
    {
        firstItemCoolDownImage.fillAmount = 1; // 초기 Fill Amount를 1로 설정
        secondItemCoolDownImage.fillAmount = 1; // 초기 Fill Amount를 1로 설정
        // currentCoolDownTime = coolDownTime; // 현재 쿨다운 시간 초기화
    }

    void Update()
    {
        // 쿨타임 패널 업데이트
        UpdateItemCoolDownPanel();
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 쿨타임 패널을 업데이트하는 메서드
    void UpdateItemCoolDownPanel()
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

        // 쿨타임 정보 업데이트
        UpdateItemCoolDown(firstItem, ref firstItemRightSkillCoolDown, firstItemRightSkillCoolDownText, firstItemCoolDownImage, isFirstItemSkillExists);
        UpdateItemCoolDown(secondItem, ref secondItemRightSkillCoolDown, secondItemRightSkillCoolDownText, secondItemCoolDownImage, isSecondItemSkillExists);

        // 무기 교체에 따른 스킬 쿨타임 패널 업데이트
        ToggleSkillCoolDownPanels(_currentItemNum);
    }

    // 아이템 쿨타임 업데이트 메서드
    void UpdateItemCoolDown(Item item, ref float skillCoolDown, TextMeshProUGUI skillCoolDownText, Image coolDownImage, bool isSkillExists)
    {
        if (isSkillExists)
        {
            if (skillCoolDown > 0)
            {
                skillCoolDown -= Time.deltaTime;
                coolDownImage.fillAmount = skillCoolDown / item.RightSkill.SkillCoolDownTime;
                skillCoolDownText.text = Mathf.Ceil(skillCoolDown).ToString();
            }
            else
            {
                skillCoolDown = 0;
                coolDownImage.fillAmount = 0;
            }
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