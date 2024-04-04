using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 팝업 창을 비활성화하고 지정된 입력 필드를 초기화하는 컴포넌트입니다.
/// </summary>
public class ClosePopupWindow : MonoBehaviour
{
    // 버튼
    [Header("[ 버튼 ]")]
    public Button closeButton;

    // 팝업 창
    [Header("[ 팝업 창 ]")]
    public GameObject popupWindow;

    // 입력 필드 배열
    [Header("[ 입력 필드 배열 ]")]
    public InputField[] inputs;

    // 스크립트가 활성화되었을 때 호출되는 메서드
    private void Awake()
    {
        // 버튼에 클릭 이벤트를 추가
        if (closeButton != null)
            this.closeButton.onClick.AddListener(HandleCloseButtonClick);
    }

    // 게임 종료 버튼 클릭 시 호출되는 메서드
    public void HandleCloseButtonClick()
    {
        // 지정된 입력 필드를 초기화
        foreach (var input in inputs)
        {
            if (input != null) // 입력 필드가 지정되었는지 확인
                input.text = "";
        }

        // 팝업 창 비활성화
        popupWindow.SetActive(false);
    }
}
