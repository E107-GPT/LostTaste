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
    public MonologueManager monologueManager; // Inspector에서 할당

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

    // 아이스킹 체력 슬라이더
    [Header("[ 아이스킹 체력 슬라이더 ]")]
    public GameObject iceKingHealthBar;

    private bool hasEntered = false; // 플레이어가 이미 입장했는지 여부를 저장하는 변수

    void Start()
    {
        // MonologueManager 게임 오브젝트에 부착된 MonologueManager 컴포넌트를 가져옵니다.
        monologueManager = GameObject.FindObjectOfType<MonologueManager>();
    }

    // 플레이어가 캠프에 진입할 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {
            stageText.text = "FINAL STAGE - 던전의 마왕"; // 스테이지 텍스트 업데이트
            stageLevelText.text = "FINAL STAGE"; // 스테이지 레벨 텍스트를 업데이트
            stageNameText.text = "던전의 마왕"; // 스테이지 이름 텍스트를 업데이트

            stage3Icon.SetActive(true); // Stage 3 클리어 아이콘 활성화

            iceKingHealthBar.SetActive(false); // 이전 스테이지 보스 체력 바 비활성화

            ShowStagePanel();

            hasEntered = true; // 플레이어가 입장했음을 표시

            monologueManager.CloseMonologue();
        }
    }

    // 5초간 스테이지 패널을 활성화하고, 다시 비활성화 하는 코루틴
    void ShowStagePanel()
    {
        stagePanel.SetActive(true);
        Invoke("CloseStagePanel", 1.5f);
    }

    void CloseStagePanel()
    {
        stagePanel.SetActive(false);
    }
}