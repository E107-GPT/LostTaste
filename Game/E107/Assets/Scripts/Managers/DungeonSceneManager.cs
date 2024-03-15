using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DungeonSceneManager : MonoBehaviour
{
    public static DungeonSceneManager Instance { get; private set; }

    // 던전 스테이지 배열
    private string[] Stages = { "DungeonForrest" };

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }
    }

    // 던전 입장
    public void EnterDungeon()
    {
        // 랜덤으로 하나의 스테이지 선택
        
        int DungeonIndex = Random.Range(0, Stages.Length);
        string SelectedStage = Stages[DungeonIndex];

        SceneManager.LoadScene(SelectedStage);
    }

    // 던전 종료
    public void ExitDungeon()
    {
        SceneManager.LoadScene("Camp");
    }
}
