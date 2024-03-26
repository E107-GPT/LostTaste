using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���� ����� ����ϱ� ���� �߰�
using TMPro;

/// <summary>
/// ä��â�� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class ChatWindow : MonoBehaviour
{
    public float inactivityThreshold = 5f; // 5��
    private float lastActivityTime;

    // ä�� InputField
    [Header("[ ä�� InputField ]")]
    public TMP_InputField chatInputField; // InputField

    // ä�� ScrollView
    [Header("[ ä�� ScrollView ]")]
    public GameObject chatScrollView; // ScrollView

    // ä�� Background
    [Header("[ ä�� Background ]")]
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

    // ����� Ȱ�� ����
    void DetectUserActivity()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // ä�� InputField Ȱ��ȭ
            chatInputField.ActivateInputField();
            chatInputField.Select();

            // ä�� ScrollView Ȱ��ȭ
            chatScrollView.SetActive(true);

            // ä�� chatBackground Ȱ��ȭ
            chatBackground.SetActive(true);

            // Ȱ�� �ð� ����
            lastActivityTime = Time.time;
        }
    }

    // ����� ��Ȱ�� �ð� üũ �� ä��â ���� ó��
    void CheckInactivity()
    {
        float timeSinceLastActivity = Time.time - lastActivityTime;
        if (timeSinceLastActivity >= inactivityThreshold)
        {
            // ä�� ScrollView ��Ȱ��ȭ
            chatScrollView.SetActive(false);
        }
    }
}
