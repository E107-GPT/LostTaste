using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualBoardUI : MonoBehaviour
{
    public enum ManualType
    {
        Basic, // 기본 조작법
        Combat // 전투 조작법
    }

    public GameObject basicManualPanel; // 기본 조작법 패널
    public GameObject combatManualPanel; // 전투 조작법 패널
    private ManualType currentManualType = ManualType.Basic; // 현재 활성화된 조작법 타입

    public bool IsShown { get; private set; }

    // 조작법을 표시하는 함수
    public void ShowManual(ManualBoardUI.ManualType type)
    {
        currentManualType = type; // 현재 타입 설정
        basicManualPanel.SetActive(type == ManualType.Basic); // 조건에 따라 기본 조작법 패널 활성화
        combatManualPanel.SetActive(type == ManualType.Combat); // 조건에 따라 전투 조작법 패널 활성화
    }

    public void HideManual()
    {
        basicManualPanel.SetActive(false); // 기본 조작법 패널 비활성화
        combatManualPanel.SetActive(false); // 전투 조작법 패널 비활성화
    }

    public void OnExitButtonClicked()
    {
        HideManual();
    }
}
