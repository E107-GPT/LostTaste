using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 플레이어가 최종 보스 방에 입장하면 스테이지 텍스트를 알맞게 업데이트하는 컴포넌트입니다.
/// </summary>
public class FinalBossEntrance : MonoBehaviour
{
    // 지도 패널
    [Header("[ 지도 패널 ]")]
    public TextMeshProUGUI stageText; // 스테이지 이름 텍스트

    // 스테이지 패널
    [Header("[ 스테이지 패널 ]")]
    public GameObject stagePanel; // 스테이지 패널
    public TextMeshProUGUI stageLevelText; // 스테이지 레벨 텍스트
    public TextMeshProUGUI stageNameText; // 스테이지 이름 텍스트

    // 클리어 한 스테이지
    [Header("[ 클리어 한 스테이지 ]")]
    public GameObject stage3Icon; // Stage 3 클리어 아이콘

    private bool hasEntered = false; // 플레이어가 이미 입장했는지 여부를 저장하는 변수

    // 플레이어가 캠프에 진입할 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {
            stageText.text = "FINAL STAGE - 던전의 마왕"; // 스테이지 텍스트 업데이트

            stageLevelText.text = "FINAL STAGE"; // 스테이지 레벨 텍스트를 업데이트
            stageNameText.text = "던전의 마왕"; // 스테이지 이름 텍스트를 업데이트

            stage3Icon.SetActive(true); // Stage 3 클리어 아이콘 활성화

            StartCoroutine(ShowStagePanel());

            hasEntered = true; // 플레이어가 입장했음을 표시
        }
    }

    // 5초간 스테이지 패널을 활성화하고, 다시 비활성화 하는 코루틴
    IEnumerator ShowStagePanel()
    {
        stagePanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        stagePanel.SetActive(false);
    }
}