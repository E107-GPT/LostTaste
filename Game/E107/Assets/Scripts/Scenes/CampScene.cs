using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CampScene : BaseScene
{
    public static CampScene Instance { get; private set; }

    // 텍스트
    public TextMeshProUGUI stageText; // 스테이지 텍스트

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.Resource.Instantiate("UnityChan");

        // 스테이지 이름 캠프로 변경
        stageText.text = "모험가의 캠프";
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
