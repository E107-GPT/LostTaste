using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 채팅창을 관리하는 클래스입니다.
/// </summary>
public class ChatWindow : MonoBehaviour
{
    // ------------------------------------------------ 변수 선언 ------------------------------------------------

    private bool isChatWindowOpen = false;
    private bool isPartyInfoOpen = false;

    // 채팅 InputField
    [Header("[ 채팅 InputField ]")]
    public TMP_InputField chatInputField; // InputField

    // 채팅 ScrollView
    [Header("[ 채팅 ScrollView ]")]
    public GameObject chatScrollView; // ScrollView

    // 채팅 Background
    [Header("[ 채팅 Background ]")]
    public GameObject chatBackground; // Background


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    //void Update()
    //{
    //    DetectUserActivity();
    //}


    // ------------------------------------------------ 사용자 정의 메서드 ------------------------------------------------

    // 사용자 활동 감지
    //void DetectUserActivity()
    //{
    //    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
    //    {
    //        if (!isChatWindowOpen)
    //        {
    //            // 채팅 InputField 활성화
    //            chatInputField.ActivateInputField();
    //            chatInputField.Select();

    //            // 채팅 ScrollView 활성화
    //            chatScrollView.SetActive(true);

    //            // 채팅 chatBackground 활성화
    //            chatBackground.SetActive(true);
    //        }
    //        else
    //        { 
                
    //        }
    //    }
    //}
}
