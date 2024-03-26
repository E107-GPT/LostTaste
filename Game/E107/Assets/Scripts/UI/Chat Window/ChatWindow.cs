using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 기능을 사용하기 위해 추가
using TMPro;

/// <summary>
/// 채팅창을 관리하는 클래스입니다.
/// </summary>
public class ChatWindow : MonoBehaviour
{
    public float inactivityThreshold = 5f; // 5초
    private float lastActivityTime;

    // 채팅 InputField
    [Header("[ 채팅 InputField ]")]
    public TMP_InputField chatInputField; // InputField

    // 채팅 ScrollView
    [Header("[ 채팅 ScrollView ]")]
    public GameObject chatScrollView; // ScrollView

    // 채팅 Background
    [Header("[ 채팅 Background ]")]
    public GameObject chatBackground; // Background

    void Start()
    {
        lastActivityTime = Time.time;
    }

    void Update()
    {
        DetectUserActivity();
        CheckInactivity();
    }

    // 사용자 활동 감지
    void DetectUserActivity()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // 채팅 InputField 활성화
            chatInputField.ActivateInputField();
            chatInputField.Select();

            // 채팅 ScrollView 활성화
            chatScrollView.SetActive(true);

            // 채팅 chatBackground 활성화
            chatBackground.SetActive(true);

            // 활동 시간 갱신
            lastActivityTime = Time.time;
        }
    }

    // 사용자 비활동 시간 체크 및 채팅창 숨김 처리
    void CheckInactivity()
    {
        float timeSinceLastActivity = Time.time - lastActivityTime;
        if (timeSinceLastActivity >= inactivityThreshold)
        {
            // 채팅 ScrollView 비활성화
            chatScrollView.SetActive(false);
        }
    }
}
