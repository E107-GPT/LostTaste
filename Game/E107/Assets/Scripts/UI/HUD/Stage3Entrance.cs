using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 플레이어가 Stage 3에 입장하면 스테이지 텍스트를 알맞게 업데이트하는 컴포넌트입니다.
/// </summary>
public class Stage3Entrance : MonoBehaviour
{
    // 지도 패널
    [Header("[ 지도 패널 ]")]
    public TextMeshProUGUI stageText; // 스테이지 이름 텍스트

    // 클리어 한 스테이지
    [Header("[ 클리어 한 스테이지 ]")]
    public GameObject stage2Icon; // Stage 2 클리어 아이콘

    // 플레이어가 캠프에 진입할 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stageText.text = "Stage 3 - 서리빛 궁전"; // 스테이지 텍스트를 캠프에 맞게 업데이트
            stage2Icon.SetActive(true); // Stage 2 클리어 아이콘 활성화
        }
    }
}