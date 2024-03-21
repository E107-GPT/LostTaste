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
    // ���� �г�
    [Header("[ ���� �г� ]")]
    public TextMeshProUGUI stageText; // �������� �̸� �ؽ�Ʈ
    Vector3 entrancePosition = new Vector3(0, 0, 0);

    // Scene �ʱ�ȭ �� ȣ��Ǵ� �Լ�
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Camp;
        // �������� �ؽ�Ʈ�� ķ�� �̸����� ������Ʈ
        stageText.text = "���谡�� ķ��";

        // Cursor ����
        SetCursor();

        Managers.Resource.Instantiate("Player/Player");

        //Managers.UI.ShowSceneUI<UI_Inven>();
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
    }

    void ResetPlayerHP()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ResetHP(); // PlayerController ���� HP �ʱ�ȭ �޼��� ȣ��
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
