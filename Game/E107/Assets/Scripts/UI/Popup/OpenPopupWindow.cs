using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 팝업 창을 활성화 하는 컴포넌트입니다.
/// </summary>
public class OpenPopupWindow : MonoBehaviour
{
    // 버튼
    [Header("[ 버튼 ]")]
    public Button openButton;

    // 팝업 창
    [Header("[ 팝업 창 ]")]
    public GameObject popupWindow;

    // 스크립트가 활성화되었을 때 호출되는 메서드
    private void Awake()
    {
        // 버튼에 클릭 이벤트를 추가
        if (openButton != null)
            this.openButton.onClick.AddListener(HandleOpenButtonClick);
    }

    // 게임 종료 버튼 클릭 시 호출되는 메서드
    public void HandleOpenButtonClick()
    {
        // 종료 확인 창 활성화
        popupWindow.SetActive(true);
    }
}
