using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 대쉬 스킬 위에 마우스를 올려서 스킬 정보 UI가 나오도록 하는 클래스입니다.
/// </summary>
public class DashSkillInfoOpen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    // 대쉬스킬 UI
    [Header("[ 대쉬스킬 UI ]")]
    public GameObject dashSkillInfoUI;

    // ------------------------------------------------ Life Cycle ------------------------------------------------

    // 마우스가 UI 요소 위에 올라왔을 때 호출될 메서드
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (dashSkillInfoUI != null)
        {
            dashSkillInfoUI.SetActive(true); // 게임 오브젝트 활성화
        }
    }

    // 마우스가 UI 요소에서 벗어났을 때 호출될 메서드
    public void OnPointerExit(PointerEventData eventData)
    {
        if (dashSkillInfoUI != null)
        {
            dashSkillInfoUI.SetActive(false); // 게임 오브젝트 비활성화
        }
    }
}
