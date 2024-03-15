using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DungeonSceneManager : MonoBehaviour
{
    public static DungeonSceneManager Instance { get; private set; }

    // ���� �������� �迭
    private string[] Stages = { "DungeonForrest" };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }
    }

    // ���� ����
    public void EnterDungeon()
    {
        // �������� �ϳ��� �������� ����
        
        int DungeonIndex = Random.Range(0, Stages.Length);
        string SelectedStage = Stages[DungeonIndex];

        SceneManager.LoadScene(SelectedStage);
    }

    // ���� ����
    public void ExitDungeon()
    {
        SceneManager.LoadScene("Camp");
    }
}
