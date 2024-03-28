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
    
    Vector3 entrancePosition = new Vector3(-100, 0, 0);

    public string bgmName;

    // Scene 초기화 시 호출되는 함수
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Camp;

        // Cursor 설정
        SetCursor();

        Managers.Resource.Instantiate("Player/Player");

        //Managers.UI.ShowSceneUI<UI_Inven>();
        MovePlayerToEntrance();
    }

   
    private void SetCursor()
    {
        // 커서를 기본 모양으로 리셋
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public override void Clear()
    {

    }

    void MovePlayerToEntrance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 오브젝트 찾기
        if (player != null)
        {
            // 플레이어 위치 설정
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.Warp(entrancePosition);
            }
        }
        Managers.Sound.Play(bgmName, Define.Sound.BGM);
    }


}
