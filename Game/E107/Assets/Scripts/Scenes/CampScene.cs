using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// ķ�� ���� �����ϴ� Ŭ�����Դϴ�.
/// </summary>
public class CampScene : BaseScene
{
    
    Vector3 entrancePosition = new Vector3(-100, 0, 0);

    public string bgmName;

    // Scene �ʱ�ȭ �� ȣ��Ǵ� �Լ�
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Camp;

        // Cursor ����
        SetCursor();

        Managers.Resource.Instantiate("Player/Player");

        //Managers.UI.ShowSceneUI<UI_Inven>();
        MovePlayerToEntrance();
    }

   
    private void SetCursor()
    {
        // Ŀ���� �⺻ ������� ����
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public override void Clear()
    {

    }

    void MovePlayerToEntrance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // �÷��̾� ������Ʈ ã��
        if (player != null)
        {
            // �÷��̾� ��ġ ����
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.Warp(entrancePosition);
            }
        }
        Managers.Sound.Play(bgmName, Define.Sound.BGM);
    }


}
