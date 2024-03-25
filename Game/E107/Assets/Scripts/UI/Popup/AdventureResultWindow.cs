using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI; // NavMeshAgent 사용을 위해 필요
using TMPro;

/// <summary>
/// 모험이 종료되면 팝업되는 결과창을 관리하는 컴포넌트입니다.
/// </summary>
public class AdventureResultWindow : MonoBehaviour
{
    // 게임 시간
    [Header("[ 게임 시간 ]")]
    public TextMeshProUGUI gameTimeText; // 게임 시간 텍스트

    void OnEnable()
    {
        float gameTime = DungeonEntrance.Instance.GameTime;
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        gameTimeText.text = string.Format("{0:0}분 {1:00}초", minutes, seconds);
    }
}