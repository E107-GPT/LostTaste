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

    // �г�
    public GameObject timeContainerPanel; // ���� �ð� �г�
    public GameObject goldPanel; // ��� �г�

    // �ؽ�Ʈ
    public TextMeshProUGUI stageText; // �������� �ؽ�Ʈ

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.Resource.Instantiate("UnityChan");

        // ���� Scene���� Ư�� �гε��� Ȱ��ȭ
        timeContainerPanel.SetActive(true);
        goldPanel.SetActive(true);

        // �������� �̸� ���������� �°� ����
        stageText.text = "Stage 1 - ���� ��";
    }

    public override void Clear()
    {

    }

    // ���� ����
    public static void EnterDungeon()
    {
        SceneManager.LoadScene("Dungeon");
    }

    // ���� ����
    public static void ExitDungeon()
    {
        SceneManager.LoadScene("Camp");
    }

}