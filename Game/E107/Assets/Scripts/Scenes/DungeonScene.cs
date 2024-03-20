using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// 던전 씬을 관리하는 클래스입니다.
/// </summary>
public class DungeonScene : BaseScene
{
    public static DungeonScene Instance { get; private set; }

    // 모험 상태 패널
    [Header("[ 모험 상태 패널 ]")]
    public GameObject timeContainerPanel; // 게임 시간 패널
    public GameObject goldPanel; // 골드 패널

    // 지도 패널
    [Header("[ 지도 패널 ]")]
    public TextMeshProUGUI stageText; // 스테이지 이름 텍스트

    // Scene 초기화 시 호출되는 함수
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.Resource.Instantiate("UnityChan");

        // 게임 시간 및 골드 패널을 활성화
        timeContainerPanel.SetActive(true);
        goldPanel.SetActive(true);

        // 스테이지 텍스트를 현재 스테이지에 맞게 업데이트
        stageText.text = "Stage 1 - 깊은 숲";
    }

    public override void Clear()
    {

    }

    // 던전 입장
    public static void EnterDungeon()
    {
        SceneManager.LoadScene("Dungeon");
    }

    // 던전 종료
    public static void ExitDungeon()
    {
        SceneManager.LoadScene("Camp");
    }

}