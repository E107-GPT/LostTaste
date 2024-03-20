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
    public static DungeonScene Instance { get; private set; }

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

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowSceneUI<UI_Inven>();
        //Managers.Resource.Instantiate("UnityChan");

        // ���� �ð� �� ��� �г��� Ȱ��ȭ
        timeContainerPanel.SetActive(true);
        goldPanel.SetActive(true);

        // �������� �ؽ�Ʈ�� ���� ���������� �°� ������Ʈ
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