using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// 캠프 씬을 관리하는 클래스입니다.
/// </summary>
public class CampScene : BaseScene
{
    public static CampScene Instance { get; private set; }

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

        // 스테이지 텍스트를 캠프 이름으로 업데이트
        stageText.text = "모험가의 캠프";

        // Scene이 로드될 때 호출될 이벤트 핸들러 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 씬이 로드될 때 호출될 함수
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 커서를 기본 모양으로 리셋
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
