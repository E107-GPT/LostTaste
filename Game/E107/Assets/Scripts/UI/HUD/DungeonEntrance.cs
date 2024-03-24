using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 플레이어가 던전에 입장하면 특정 UI들을 활성화하고,
/// 스테이지 텍스트를 알맞게 업데이트하는 컴포넌트입니다.
/// </summary>
public class DungeonEntrance : MonoBehaviour
{
    // 모험 상태 패널
    [Header("[ 모험 상태 패널 ]")]
    public GameObject timeContainerPanel; // 게임 시간 패널
    public GameObject goldPanel; // 골드 패널

    // 지도 패널
    [Header("[ 지도 패널 ]")]
    public TextMeshProUGUI stageText; // 스테이지 이름 텍스트

    // 플레이어가 캠프에 진입할 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeContainerPanel.SetActive(true); // 게임 시간 UI 활성화
            goldPanel.SetActive(true); // 골드 UI 활성화
            stageText.text = "Stage 1 - 깊은 숲의 속삭임"; // 스테이지 텍스트를 캠프에 맞게 업데이트
        }
    }
}