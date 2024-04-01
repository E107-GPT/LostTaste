using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 직업 스킬 쿨타임 UI 매니저는 직업 스킬 쿨타임을 표시하는 기능을 제공합니다.
/// </summary>
public class ClassSkillCooldownUIManager : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 직업 스킬 쿨타임 UI 매니저가 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수

    // 직업 스킬 쿨타임
    private float classSkillCoolDown;

    // 직업 스킬 패널
    [Header("[ 직업 스킬 패널 ]")]
    public Image classSkillCoolDownImage; // 직업 스킬 쿨타임 이미지
    public Image classSkillKeyImage; // 직업 스킬 키 이미지
    public TextMeshProUGUI classSkillCoolDownText; // 직업 스킬 쿨타임


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Start()
    {
        // 쿨타임 패널 초기화
        ResetCoolDownUI(classSkillCoolDownText, classSkillCoolDownImage, classSkillKeyImage);
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

    private void HandleSkillCast(bool isCasting, string name)
    {
        if (isCasting && name == "Player")
        {
            // PlayerController 컴포넌트를 찾아서 참조
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

            if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때

            // 현재 직업 및 직업스킬 쿨타임 가져옴
            classSkillCoolDown = _playerController.PlayerClass.ClassSkill.SkillCoolDownTime;

            StartCoroutine(UpdateClassSkillCoolDown(classSkillCoolDown, classSkillCoolDownText, classSkillCoolDownImage, classSkillKeyImage));
        }
    }
    
    IEnumerator UpdateClassSkillCoolDown(float skillCoolDown, TextMeshProUGUI skillCoolDownText, Image coolDownImage, Image keyImage)
    {
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
    }

    // 코루틴이 끝난 뒤 쿨타임 패널을 초기화 하는 메서드
    void ResetCoolDownUI(TextMeshProUGUI dashCoolDownText, Image coolDownImage, Image keyImage)
    {
        dashCoolDownText.text = "";
        coolDownImage.fillAmount = 0;
        keyImage.fillAmount = 0;
    }
}