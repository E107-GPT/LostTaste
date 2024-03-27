using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// 플레이어가 Stage 2에 입장하면 스테이지 텍스트를 알맞게 업데이트하는 컴포넌트입니다.
/// </summary>
public class Stage2Entrance : MonoBehaviour
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
    public GameObject stageXIcon; // 클리어 한 스테이지 없음 아이콘
    public GameObject stage1Icon; // Stage 1 클리어 아이콘
    public TextMeshProUGUI stageClearText; // 스테이지 클리어 텍스트

    // 플레이어가 캠프에 진입할 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stageText.text = "STAGE 2 - 잊혀진 해변"; // 스테이지 텍스트 업데이트

            stageLevelText.text = "STAGE 2"; // 스테이지 레벨 텍스트를 업데이트
            stageNameText.text = "잊혀진 해변"; // 스테이지 이름 텍스트를 업데이트

            stageXIcon.SetActive(false); // 클리어 한 스테이지 없음 아이콘 비활성화
            stage1Icon.SetActive(true); // Stage 1 클리어 아이콘 활성화
            stageClearText.text = "클리어한 스테이지입니다.";

            StartCoroutine(ShowStagePanel());
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