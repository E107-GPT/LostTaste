using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 직업 선택 UI가 나오도록 하는 클래스입니다.
/// </summary>
public class ClassSelectionWindow : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수
    private int selectedClass = 1;

    // 직업선택 UI
    [Header("[ 직업선택 UI ]")]
    public GameObject classSelectionUI;

    // 스테이지 패널
    [Header("[ 스테이지 패널 ]")]
    public GameObject stagePanel; // 스테이지 패널

    // 버튼
    [Header("[ 버튼 ]")]
    public Button warriorButton; // 워리어
    public Button mageButton; // 메이지
    public Button priestButton; // 프리스트
    public Button ninjaButton; // 닌자
    public Button selectButton; // 선택 버튼

    // 선택 여부 테두리
    [Header("[ 선택 여부 테두리 ]")]
    public GameObject warriorSelected; // 워리어
    public GameObject mageSelected; // 메이지
    public GameObject priestSelected; // 프리스트
    public GameObject ninjaSelected; // 닌자

    // 직업 스탯 패널
    [Header("[ 직업 스탯 패널]")]
    public GameObject warriorStatPanel; // 워리어
    public GameObject mageStatPanel; // 메이지
    public GameObject priestStatPanel; // 프리스트
    public GameObject ninjaStatPanel; // 닌자

    // 직업 스킬 패널
    [Header("[ 직업 스킬 패널 ]")]
    public GameObject warriorSkillPanel; // 워리어
    public GameObject mageSkillPanel; // 메이지
    public GameObject priestSkillPanel; // 프리스트
    public GameObject ninjaSkillPanel; // 닌자


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    // 스크립트가 활성화되었을 때 호출되는 메서드
    private void Awake()
    {
        // 버튼에 워리어 선택 클릭 이벤트를 추가
        if (warriorButton != null)
            this.warriorButton.onClick.AddListener(HandleWarriorButtonClick);

        // 버튼에 메이지 선택 클릭 이벤트를 추가
        if (mageButton != null)
            this.mageButton.onClick.AddListener(HandleMageButtonClick);

        // 버튼에 프리스트 선택 클릭 이벤트를 추가
        if (priestButton != null)
            this.priestButton.onClick.AddListener(HandlePriestButtonClick);

        // 버튼에 닌자 선택 클릭 이벤트를 추가
        if (ninjaButton != null)
            this.ninjaButton.onClick.AddListener(HandleNinjaButtonClick);

        // 선택 버튼에 클래스 선택 클릭 이벤트를 추가
        if (selectButton != null)
            this.selectButton.onClick.AddListener(HandleSelectButtonClick);
    }

    void Update()
    {
        // PlayerController 컴포넌트를 찾아서 참조
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때
    }

    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 워리어 선택 시 호출되는 메서드
    public void HandleWarriorButtonClick()
    {
        selectedClass = 1;

        // 선택 테두리 활성화
        warriorSelected.SetActive(true);
        mageSelected.SetActive(false);
        priestSelected.SetActive(false);
        ninjaSelected.SetActive(false);

        // 스탯 패널 활성화
        warriorStatPanel.SetActive(true);
        mageStatPanel.SetActive(false);
        priestStatPanel.SetActive(false);
        ninjaStatPanel.SetActive(false);

        // 스킬 패널 활성화
        warriorSkillPanel.SetActive(true);
        mageSkillPanel.SetActive(false);
        priestSkillPanel.SetActive(false);
        ninjaSkillPanel.SetActive(false);
    }

    // 메이지 선택 시 호출되는 메서드
    public void HandleMageButtonClick()
    {
        selectedClass = 2;

        // 선택 테두리 활성화
        warriorSelected.SetActive(false);
        mageSelected.SetActive(true);
        priestSelected.SetActive(false);
        ninjaSelected.SetActive(false);

        // 스탯 패널 활성화
        warriorStatPanel.SetActive(false);
        mageStatPanel.SetActive(true);
        priestStatPanel.SetActive(false);
        ninjaStatPanel.SetActive(false);

        // 스킬 패널 활성화
        warriorSkillPanel.SetActive(false);
        mageSkillPanel.SetActive(true);
        priestSkillPanel.SetActive(false);
        ninjaSkillPanel.SetActive(false);
    }

    // 프리스트 선택 시 호출되는 메서드
    public void HandlePriestButtonClick()
    {
        selectedClass = 3;

        // 선택 테두리 활성화
        warriorSelected.SetActive(false);
        mageSelected.SetActive(false);
        priestSelected.SetActive(true);
        ninjaSelected.SetActive(false);

        // 스탯 패널 활성화
        warriorStatPanel.SetActive(false);
        mageStatPanel.SetActive(false);
        priestStatPanel.SetActive(true);
        ninjaStatPanel.SetActive(false);

        // 스킬 패널 활성화
        warriorSkillPanel.SetActive(false);
        mageSkillPanel.SetActive(false);
        priestSkillPanel.SetActive(true);
        ninjaSkillPanel.SetActive(false);
    }

    // 닌자 선택 시 호출되는 메서드
    public void HandleNinjaButtonClick()
    {
        selectedClass = 4;

        // 선택 테두리 활성화
        warriorSelected.SetActive(false);
        mageSelected.SetActive(false);
        priestSelected.SetActive(false);
        ninjaSelected.SetActive(true);

        // 스탯 패널 활성화
        warriorStatPanel.SetActive(false);
        mageStatPanel.SetActive(false);
        priestStatPanel.SetActive(false);
        ninjaStatPanel.SetActive(true);

        // 스킬 패널 활성화
        warriorSkillPanel.SetActive(false);
        mageSkillPanel.SetActive(false);
        priestSkillPanel.SetActive(false);
        ninjaSkillPanel.SetActive(true);
    }

    // 선택 버튼 클릭 시 호출되는 메서드
    public void HandleSelectButtonClick()
    {
        if (selectedClass == 1)
        {
            _playerController.ChangeClass(Define.ClassType.Warrior);
        }
        else if (selectedClass == 2)
        {
            _playerController.ChangeClass(Define.ClassType.Mage);
        }
        else if (selectedClass == 3)
        {
            _playerController.ChangeClass(Define.ClassType.Priest);
        }
        else if (selectedClass == 4)
        {
            _playerController.ChangeClass(Define.ClassType.Ninja);
        }

        stagePanel.SetActive(true);
        Invoke("DisableStagePanel", 1.5f);
    }

    // 1.5초 후에 호출되어 스테이지 패널을 비활성화하는 메서드
    private void DisableStagePanel()
    {
        stagePanel.SetActive(false);
    }
}
