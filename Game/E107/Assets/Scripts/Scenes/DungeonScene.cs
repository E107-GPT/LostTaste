using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// ���� ���� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class DungeonScene : BaseScene
{
    Vector3 entrancePosition = new Vector3(-100, 0, 0);

    // ���� ���� �г�
    [Header("[ ���� ���� �г� ]")]
    public GameObject timeContainerPanel; // ���� �ð� �г�
    public GameObject goldPanel; // ��� �г�

    // ���� �г�
    [Header("[ ���� �г� ]")]
    public TextMeshProUGUI stageText; // �������� �̸� �ؽ�Ʈ

    // Scene �ʱ�ȭ �� ȣ��Ǵ� �Լ�
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Dungeon;

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.Resource.Instantiate("UnityChan");

        // ���� �ð� �� ��� �г��� Ȱ��ȭ
        timeContainerPanel.SetActive(true);
        goldPanel.SetActive(true);

        // �������� �ؽ�Ʈ�� ���� ���������� �°� ������Ʈ
        stageText.text = "Stage 1 - ���� ��";
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