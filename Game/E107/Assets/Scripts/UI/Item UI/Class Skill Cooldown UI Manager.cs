using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 직업 스킬 쿨타임 UI 매니저는 직업 스킬의 쿨타임을 표시하는 기능을 제공합니다.
/// </summary>
public class ClassSkillCooldownUIManager : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 직업 스킬 쿨타임 UI 매니저가 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수

    // 직업 스킬 패널
    [Header("[ 직업 스킬 패널 ]")]
    public Image classSkillCoolDownImage; // 직업 스킬 쿨타임 이미지
    public Image classSkillKeyImage; // 직업 스킬 키 이미지
    public TextMeshProUGUI classSkillCoolDownText; // 직업 스킬 쿨타임

    // 남은 쿨타임 숫자 변수 선언
    private float classSkillCoolDown; // 직업 스킬 현재 쿨타임

    // 쿨타임 진행 상태를 추적하는 변수 추가
    private bool isClassSkillCoolingDown = false;

    private Coroutine classSkillDownCoroutine = null;


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Start()
    {
        // 초기 Fill Amount를 0으로 설정
        classSkillCoolDownImage.fillAmount = 0;
        classSkillKeyImage.fillAmount = 0;

        // 초기 쿨타임 텍스트 빈 문자열로 설정
        classSkillCoolDownText.text = "";
    }

    // 각 직업 스킬의 OnSkillCast 이벤트를 구독
    void OnEnable()
    {
        MageClassSkill.OnMageSkillCast += HandleSkillCast;
        NinjaClassSkill.OnNinjaSkillCast += HandleSkillCast;
        WarriorClassSkill.OnWarriorSkillCast += HandleSkillCast;
        PriestClassSkill.OnPriestSkillCast += HandleSkillCast;
    }

    void OnDisable()
    {
        MageClassSkill.OnMageSkillCast -= HandleSkillCast;
        NinjaClassSkill.OnNinjaSkillCast -= HandleSkillCast;
        WarriorClassSkill.OnWarriorSkillCast -= HandleSkillCast;
        PriestClassSkill.OnPriestSkillCast -= HandleSkillCast;
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    private void HandleSkillCast(bool isCasting)
    {
        if (isCasting)
        {
            // PlayerController 컴포넌트를 찾아서 참조
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

            if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때

            // 현재 직업 및 직업스킬 쿨타임 가져옴
            classSkillCoolDown = _playerController.PlayerClass.ClassSkill.SkillCoolDownTime;

            classSkillDownCoroutine = StartCoroutine(UpdateClassSkillCoolDown(classSkillCoolDown, classSkillCoolDownText, classSkillCoolDownImage, classSkillKeyImage));
        }
    }
    
    IEnumerator UpdateClassSkillCoolDown(float skillCoolDown, TextMeshProUGUI skillCoolDownText, Image coolDownImage, Image keyImage)
    {
        // 쿨타임이 시작될 때의 상태 변경
        isClassSkillCoolingDown = true;

        // 경과 시간을 추적하는 변수
        float elapsedTime = 0;
        while (elapsedTime < skillCoolDown)
        {
            elapsedTime += Time.deltaTime;
            coolDownImage.fillAmount = (skillCoolDown - elapsedTime) / skillCoolDown;
            keyImage.fillAmount = (skillCoolDown - elapsedTime) / skillCoolDown;
            if (skillCoolDown - elapsedTime > 1)
            {
                skillCoolDownText.text = Mathf.Ceil(skillCoolDown - elapsedTime).ToString() + "s";
            }
            else
            {
                skillCoolDownText.text = (skillCoolDown - elapsedTime).ToString("F1");
            }
            yield return new WaitForFixedUpdate();
        }

        // 코루틴이 끝난 뒤 쿨타임 패널을 초기화
        ResetCoolDownUI(skillCoolDownText, coolDownImage, keyImage);

        // 쿨타임이 종료될 때의 상태 변경
        isClassSkillCoolingDown = false;
    }

    // 코루틴이 끝난 뒤 쿨타임 패널을 초기화 하는 메서드
    void ResetCoolDownUI(TextMeshProUGUI dashCoolDownText, Image coolDownImage, Image keyImage)
    {
        dashCoolDownText.text = "";
        coolDownImage.fillAmount = 0;
        keyImage.fillAmount = 0;
    }
}