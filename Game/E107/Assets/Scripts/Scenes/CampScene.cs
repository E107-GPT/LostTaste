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
    // 지도 패널
    [Header("[ 지도 패널 ]")]
    public TextMeshProUGUI stageText; // 스테이지 이름 텍스트
    Vector3 entrancePosition = new Vector3(0, 0, 0);

    // Scene 초기화 시 호출되는 함수
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Camp;
        // 스테이지 텍스트를 캠프 이름으로 업데이트
        stageText.text = "모험가의 캠프";

        // Cursor 설정
        SetCursor();

        Managers.Resource.Instantiate("Player/Player");

        //Managers.UI.ShowSceneUI<UI_Inven>();
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
    }

    void ResetPlayerHP()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ResetHP(); // PlayerController 내의 HP 초기화 메서드 호출
            }
            else
            {
                Debug.LogError("PlayerController component not found on the player!");
            }
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }


}
