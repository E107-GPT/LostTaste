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

    // �ؽ�Ʈ
    public TextMeshProUGUI stageText; // �������� �ؽ�Ʈ

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.Resource.Instantiate("UnityChan");

        // �������� �̸� ķ���� ����
        stageText.text = "���谡�� ķ��";
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
