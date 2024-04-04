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
    Vector3 entrancePosition = new Vector3(-100, 0, 0);

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

        SceneType = Define.Scene.Dungeon;

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.Resource.Instantiate("UnityChan");

        // 게임 시간 및 골드 패널을 활성화
        timeContainerPanel.SetActive(true);
        goldPanel.SetActive(true);

        // 스테이지 텍스트를 현재 스테이지에 맞게 업데이트
        stageText.text = "Stage 1 - 깊은 숲";
        Managers.Resource.Instantiate("Player/Player");
        MovePlayerToEntrance();

    }

    void MovePlayerToEntrance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            Debug.Log("Player found.");
            //player.transform.position = entrancePosition;
            agent.Warp(entrancePosition);
            Debug.Log("Player moved to entrance.");
        }
        else
        {
            Debug.LogError("Player not found.");
        }
    }

    public override void Clear()
    {

    }

}