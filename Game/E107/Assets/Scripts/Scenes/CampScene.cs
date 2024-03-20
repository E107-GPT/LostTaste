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
    public static CampScene Instance { get; private set; }

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

        // �������� �ؽ�Ʈ�� ķ�� �̸����� ������Ʈ
        stageText.text = "���谡�� ķ��";

        // Scene�� �ε�� �� ȣ��� �̺�Ʈ �ڵ鷯 ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ���� �ε�� �� ȣ��� �Լ�
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ŀ���� �⺻ ������� ����
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
