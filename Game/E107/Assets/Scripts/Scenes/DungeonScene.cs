using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DungeonScene : BaseScene
{
    public static DungeonScene Instance { get; private set; }

    // 패널
    public GameObject timeContainerPanel; // 게임 시간 패널
    public GameObject goldPanel; // 골드 패널

    // 텍스트
    public TextMeshProUGUI stageText; // 스테이지 텍스트

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.Resource.Instantiate("UnityChan");

        // 던전 Scene에서 특정 패널들을 활성화
        timeContainerPanel.SetActive(true);
        goldPanel.SetActive(true);

        // 스테이지 이름 스테이지에 맞게 변경
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