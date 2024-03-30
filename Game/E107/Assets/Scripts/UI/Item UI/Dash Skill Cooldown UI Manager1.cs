using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 직업 스킬 쿨타임 UI 매니저는 직업 스킬의 쿨타임을 표시하는 기능을 제공합니다.
/// </summary>
public class DashSkillCooldownUIManager : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 직업 스킬 쿨타임 UI 매니저가 사용할 변수 선언
    private PlayerController _playerController; // 플레이어 컨트롤러 참조 변수

    // 직업 스킬 패널
    [Header("[ 직업 스킬 패널 ]")]
    public Image dashCoolDownImage; // 대시 스킬 쿨타임 이미지
    public Image dashSkillKeyImage; // 직업 스킬 키 이미지
    public TextMeshProUGUI dashCoolDownText; // 직업 스킬 쿨타임

    // 남은 쿨타임 숫자 변수 선언
    private float dashSkillCoolDown; // 직업 스킬 현재 쿨타임

    // 쿨타임 진행 상태를 추적하는 변수 추가
    private bool isDashCoolingDown = false;


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Start()
    {
        // 초기 Fill Amount를 0으로 설정
        dashCoolDownImage.fillAmount = 0;
        dashSkillKeyImage.fillAmount = 0;

        // 초기 쿨타임 텍스트 빈 문자열로 설정
        dashCoolDownText.text = "";
    }

    void Update()
    {
        // 대쉬 쿨타임 패널 업데이트
        UpdateDashCoolDownPanel();
    }


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    void UpdateDashCoolDownPanel()
    {
        // PlayerController 컴포넌트를 찾아서 참조
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController 컴포넌트를 찾을 수 없을 때

        // 스킬 쿨타임 가져옴
        dashSkillCoolDown = _playerController.DashCoolDownTime;

        // 쿨타임 정보 업데이트
        if (Input.GetKey(KeyCode.Space) && !isDashCoolingDown)
        {
            // 쿨타임 정보 업데이트
            if (_playerController.CurState is not DashState) return;
        
            StartCoroutine(UpdateDashCoolDown(dashSkillCoolDown, dashCoolDownText, dashCoolDownImage, dashSkillKeyImage));
        }
            
    }

    IEnumerator UpdateDashCoolDown(float dashCoolDown, TextMeshProUGUI dashCoolDownText, Image coolDownImage, Image keyImage)
    {
        // 쿨타임이 시작될 때의 상태 변경
        isDashCoolingDown = true;

        // 경과 시간을 추적하는 변수
        float elapsedTime = 0;

        while (elapsedTime < dashCoolDown)
        {
            elapsedTime += Time.deltaTime;
            coolDownImage.fillAmount = (dashCoolDown - elapsedTime) / dashCoolDown;
            keyImage.fillAmount = (dashCoolDown - elapsedTime) / dashCoolDown;
            if (dashCoolDown - elapsedTime > 1)
            {
                dashCoolDownText.text = Mathf.Ceil(dashCoolDown - elapsedTime).ToString("F1") + "s";
            }
            else
            {
                dashCoolDownText.text = (dashCoolDown - elapsedTime).ToString("F1");
            }
            yield return new WaitForFixedUpdate();
        }

        // 코루틴이 끝난 뒤 쿨타임 패널을 초기화
        ResetCoolDownUI(dashCoolDownText, coolDownImage, keyImage);

        // 쿨타임이 종료될 때의 상태 변경
        isDashCoolingDown = false;
    }

    // 코루틴이 끝난 뒤 쿨타임 패널을 초기화 하는 메서드
    void ResetCoolDownUI(TextMeshProUGUI dashCoolDownText, Image coolDownImage, Image keyImage)
    {
        dashCoolDownText.text = "";
        coolDownImage.fillAmount = 0;
        keyImage.fillAmount = 0;
    }
}