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

    // 쿨타임 진행 상태를 추적하는 변수 추가
    private bool isFirstItemCoolingDown = false;
    private bool isSecondItemCoolingDown = false;


    // ------------------------------------------------ Life Cylce ------------------------------------------------

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

        firstItemRightSkillCoolDown = firstItem.RightSkill.SkillCoolDownTime;
        secondItemRightSkillCoolDown = secondItem.RightSkill.SkillCoolDownTime;

        // 쿨타임 정보 업데이트
        if (Input.GetMouseButton(1))
        {
            if (_currentItemNum == 1 && !isFirstItemCoolingDown)
            {
                StartCoroutine(UpdateItemCoolDown(firstItem, firstItemRightSkillCoolDown, firstItemRightSkillCoolDownText, firstItemCoolDownImage, firstItemKeyImage, isFirstItemSkillExists));
            }
            else if (_currentItemNum == 2 && !isSecondItemCoolingDown)
            {
                StartCoroutine(UpdateItemCoolDown(secondItem, secondItemRightSkillCoolDown, secondItemRightSkillCoolDownText, secondItemCoolDownImage, secondItemKeyImage, isSecondItemSkillExists));
            }
        }

        // 무기 교체에 따른 스킬 쿨타임 패널 업데이트
        ToggleSkillCoolDownPanels(_currentItemNum);
    }

    IEnumerator UpdateItemCoolDown(Item item, float skillCoolDown, TextMeshProUGUI skillCoolDownText, Image coolDownImage, Image keyImage, bool isSkillExists)
    {
        if (!isSkillExists) yield break;

        // 쿨타임이 시작될 때의 상태 변경
        if (item == _playerInventory[1])
        {
            isFirstItemCoolingDown = true;
        }
        else if (item == _playerInventory[2])
        {
            isSecondItemCoolingDown = true;
        }

        while (skillCoolDown > 0.0f)
        {
            skillCoolDown -= Time.unscaledDeltaTime;
            coolDownImage.fillAmount = skillCoolDown / item.RightSkill.SkillCoolDownTime;
            keyImage.fillAmount = skillCoolDown / item.RightSkill.SkillCoolDownTime;
            skillCoolDownText.text = Mathf.Ceil(skillCoolDown).ToString() + "s";
            yield return new WaitForFixedUpdate();
        }

        skillCoolDownText.text = ""; // 쿨타임이 완전히 끝났을 때, 텍스트를 빈 문자열로 설정

        // 쿨타임이 종료될 때의 상태 변경
        if (item == _playerInventory[1])
        {
            isFirstItemCoolingDown = false;
        }
        else if (item == _playerInventory[2])
        {
            isSecondItemCoolingDown = false;
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