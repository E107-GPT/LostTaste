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
    public static DungeonEntrance Instance { get; private set; }

    private float gameTime = 0f;
    public float GameTime => gameTime; // 외부에서 접근 가능하도록 게터 추가

    private bool isInCamp = true;

    // 게임 시간
    [Header("[ 게임 시간 ]")]
    public GameObject timeContainerPanel; // 게임 시간 패널
    public TextMeshProUGUI gameTimeText; // 게임 시간 텍스트

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

    // 클리어 한 스테이지
    [Header("[ 클리어 한 스테이지 ]")]
    public GameObject stageXIcon; // 클리어 한 스테이지 없음 아이콘
    public GameObject stage1Icon; // Stage 1 클리어 아이콘
    public GameObject stage2Icon; // Stage 2 클리어 아이콘
    public GameObject stage3Icon; // Stage 3 클리어 아이콘
    public GameObject finalStageIcon; // Final Stage 클리어 아이콘
    public TextMeshProUGUI stageClearText; // 스테이지 클리어 텍스트

    private bool hasEntered = false; // 플레이어가 이미 입장했는지 여부를 저장하는 변수

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 선택적
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isInCamp)
        {
            // 캠프에 있는 동안 시간 초기화
            gameTime = 0f;
        }
        else
        {
            // 던전에 있는 동안 시간 흐름
            gameTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(gameTime / 60);
            int seconds = Mathf.FloorToInt(gameTime % 60);
            gameTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // 플레이어가 캠프에 진입할 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {

            isInCamp = false;
            timeContainerPanel.SetActive(true); // 게임 시간 UI 활성화
            stageText.text = "STAGE 1 - 깊은 숲"; // 스테이지 텍스트 업데이트

            stageLevelText.text = "STAGE 1"; // 스테이지 레벨 텍스트를 업데이트
            stageNameText.text = "깊은 숲"; // 스테이지 이름 텍스트를 업데이트

            campGameMenu.SetActive(false); // 캠프 게임 메뉴 비활성화
            dungeonGameMenu.SetActive(true); // 던전 게임 메뉴 활성화

            stageXIcon.SetActive(true); // 클리어 한 스테이지 없음 아이콘 활성화
            stage1Icon.SetActive(false); // Stage 1 클리어 아이콘 비활성화
            stage2Icon.SetActive(false); // Stage 2 클리어 아이콘 비활성화
            stage3Icon.SetActive(false); // Stage 3 클리어 아이콘 비활성화
            finalStageIcon.SetActive(false); // Final Stage 클리어 아이콘 비활성화
            stageClearText.text = "클리어한 스테이지가 없습니다.";

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