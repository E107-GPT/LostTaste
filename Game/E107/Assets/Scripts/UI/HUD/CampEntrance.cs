using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 플레이어가 캠프에 입장하면 특정 UI들을 비활성화하고,
/// 스테이지 텍스트를 알맞게 업데이트하는 컴포넌트입니다.
/// </summary>
public class CampEntrance : MonoBehaviour
{
    // 게임 시간
    [Header("[ 게임 시간 ]")]
    public GameObject timeContainerPanel; // 게임 시간 패널

    // 지도 패널
    [Header("[ 지도 패널 ]")]
    public TextMeshProUGUI stageText; // 스테이지 텍스트

    // 스테이지 패널
    [Header("[ 스테이지 패널 ]")]
    public GameObject stagePanel; // 스테이지 패널
    public TextMeshProUGUI stageLevelText; // 스테이지 레벨 텍스트
    public TextMeshProUGUI stageNameText; // 스테이지 이름 텍스트

    // 게임 메뉴
    [Header("[ 게임 메뉴 ]")]
    public GameObject campGameMenu; // 캠프 게임 메뉴
    public GameObject dungeonGameMenu; // 던전 게임 메뉴

    // 보스 상태
    [Header("[ 보스 상태 ]")]
    public GameObject drillDuckStatus; // 드릴덕 상태 패널
    public GameObject crocodileStatus; // 크로커다일 상태 패널

    // 플레이어가 캠프에 진입할 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        timeContainerPanel.SetActive(false); // 게임 시간 UI 비활성화
        stageText.text = "모험가의 캠프"; // 스테이지 텍스트를 캠프에 맞게 업데이트

        stageLevelText.text = "Camp"; // 스테이지 레벨 텍스트를 업데이트
        stageNameText.text = "모험가의 캠프"; // 스테이지 이름 텍스트를 업데이트

        campGameMenu.SetActive(true); // 캠프 게임 메뉴 활성화
        dungeonGameMenu.SetActive(false); // 던전 게임 메뉴 비활성화

        drillDuckStatus.SetActive(false); // 드릴덕 상태 패널 비활성화
        crocodileStatus.SetActive(false); // 크로커다일 상태 패널 비활성화

        StartCoroutine(ShowStagePanel());
    }

    // 5초간 스테이지 패널을 활성화하고, 다시 비활성화 하는 코루틴
    IEnumerator ShowStagePanel()
    {
        stagePanel.SetActive(true);
        yield return new WaitForSeconds(3);
        stagePanel.SetActive(false);
    }
}