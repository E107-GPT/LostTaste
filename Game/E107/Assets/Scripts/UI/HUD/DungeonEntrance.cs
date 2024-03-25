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
    private float gameTime = 0f;
    private bool isInCamp = true;

    // 게임 시간
    [Header("[ 게임 시간 ]")]
    public GameObject timeContainerPanel; // 게임 시간 패널
    public TextMeshProUGUI gameTimeText; // 게임 시간 텍스트

    // 지도 패널
    [Header("[ 지도 패널 ]")]
    public TextMeshProUGUI stageText; // 스테이지 이름 텍스트

    // 게임 메뉴
    [Header("[ 게임 메뉴 ]")]
    public GameObject campGameMenu; // 캠프 게임 메뉴
    public GameObject dungeonGameMenu; // 던전 게임 메뉴

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
        if (other.CompareTag("Player"))
        {

            isInCamp = false;
            timeContainerPanel.SetActive(true); // 게임 시간 UI 활성화
            stageText.text = "Stage 1 - 깊은 숲"; // 스테이지 텍스트를 캠프에 맞게 업데이트

            campGameMenu.SetActive(false); // 캠프 게임 메뉴 비활성화
            dungeonGameMenu.SetActive(true); // 던전 게임 메뉴 활성화
        }
    }
}