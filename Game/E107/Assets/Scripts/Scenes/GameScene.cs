using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameScene : BaseScene
{
    public static GameScene Instance { get; private set; }


    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.Resource.Instantiate("UnityChan");
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
