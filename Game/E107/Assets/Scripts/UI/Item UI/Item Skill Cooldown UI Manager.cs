using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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

    // 코루틴 참조를 저장할 변수
    private Coroutine firstItemCoolDownCoroutine = null;
    private Coroutine secondItemCoolDownCoroutine = null;


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
        _currentItemNum = _playerController.CurrentItemNum;

        // PlayerController의 인벤토리와 현재 아이템 번호를 가져옴
        Item firstItem = _playerInventory[1];
        Item secondItem = _playerInventory[2];

        // '맨손'일 경우 UI 초기화 (사용 아이템 사용 시 UI 초기화하는 기능)
        if (firstItem != null && firstItem.name == "0000_Fist")
        {
            ResetCoolDownUI(firstItem);
        }
        else if (secondItem != null && secondItem.name == "0000_Fist")
        {
            ResetCoolDownUI(secondItem);
        }

        // 육회 비빔밥 사용시 쿨타임 UI 미적용
        if (firstItem != null &&
            (firstItem.name == "0020_SixTimesBibimbap" ||
             firstItem.name == "0021_FiveTimesBibimbap" ||
             firstItem.name == "0022_FourTimesBibimbap" ||
             firstItem.name == "0023_ThreeTimesBibimbap" ||
             firstItem.name == "0024_TwoTimesBibimbap" ||
             firstItem.name == "0025_Bibimbap"))
        {
            ResetCoolDownUI(firstItem);
        }
        else if (secondItem != null &&
            (secondItem.name == "0020_SixTimesBibimbap" ||
             secondItem.name == "0021_FiveTimesBibimbap" ||
             secondItem.name == "0022_FourTimesBibimbap" ||
             secondItem.name == "0023_ThreeTimesBibimbap" ||
             secondItem.name == "0024_TwoTimesBibimbap" ||
             secondItem.name == "0025_Bibimbap"))
        {
            ResetCoolDownUI(secondItem);
        }

        // 스킬 존재 여부 확인
        bool isFirstItemSkillExists = firstItem.RightSkill != null && !(firstItem.RightSkill is EmptySkill);
        bool isSecondItemSkillExists = secondItem.RightSkill != null && !(secondItem.RightSkill is EmptySkill);

        // 쿨타임 정보 업데이트
        if (Input.GetMouseButton(1))
        {
            // 캐릭터가 '스킬 상태'가 아닐 경우 함수를 빠져나감
            if (_playerController.CurState is not SkillState) return;

            // 현재 쿨타임이 진행 중이지 않으면서, 스킬이 존재할 경우 쿨타임을 진행시킴
            if (_currentItemNum == 1 && !isFirstItemCoolingDown && isFirstItemSkillExists)
            {
                UpdateItemSkillCoolDown(firstItem, ref isFirstItemCoolingDown, ref firstItemCoolDownCoroutine);
            }
            else if (_currentItemNum == 2 && !isSecondItemCoolingDown && isSecondItemSkillExists)
            {
                UpdateItemSkillCoolDown(secondItem, ref isSecondItemCoolingDown, ref secondItemCoolDownCoroutine);
            }
        }

        // 무기 교체에 따른 스킬 쿨타임 패널 업데이트
        ToggleSkillCoolDownPanels(_currentItemNum);

        // 아이템 변경 또는 버림 감지
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.B))
        {
            // 캐릭터가 '대기 상태' 또는 '걷기 상태'가 아닐 경우 함수를 빠져나감
            if (!(_playerController.CurState is IdleState || _playerController.CurState is MoveState)) return;

            // 진행중인 코르틴을 멈추고 UI를 초기화함
            if (firstItemCoolDownCoroutine != null && _currentItemNum == 1)
            {
                StopCoroutine(firstItemCoolDownCoroutine);
                ResetCoolDownUI(firstItem);
            }
            else if (secondItemCoolDownCoroutine != null && _currentItemNum == 2)
            {
                StopCoroutine(secondItemCoolDownCoroutine);
                ResetCoolDownUI(secondItem);
            }
        }
    }

    void UpdateItemSkillCoolDown(Item item, ref bool isCoolingDown, ref Coroutine coolDownCoroutine)
    {
        // 쿨다운 중이거나 아이템의 스킬이 없으면 함수 종료
        if (isCoolingDown || item.RightSkill == null) return;

        isCoolingDown = true;
        
        // 새로운 코루틴 시작
        coolDownCoroutine = StartCoroutine(UpdateItemCoolDownCoroutine(item));

        isCoolingDown = false;
    }

    IEnumerator UpdateItemCoolDownCoroutine(Item item)
    {
        // 아이템의 쿨타임 정보 가져오기
        float skillCoolDown = item.RightSkill.SkillCoolDownTime;
        float elapsedTime = 0; // 경과 시간을 추적하는 변수

        while (elapsedTime < skillCoolDown)
        {
            elapsedTime += Time.deltaTime;
            float remainingTime = skillCoolDown - elapsedTime;

            // UI 업데이트 로직
            UpdateCoolDownUI(item, remainingTime / skillCoolDown, remainingTime);
            yield return null;
        }
        
        // 쿨다운 완료 후 UI 초기화
        ResetCoolDownUI(item);
    }

    // 쿨다운 UI 업데이트 메서드
    void UpdateCoolDownUI(Item item, float fillAmount, float remainingTime)
    {
        string text;

        if (remainingTime > 1)
        {
            text = Mathf.Ceil(remainingTime).ToString() + "s";
        }
        else
        {
            text = (remainingTime).ToString("F1");
        }

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