using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CampSpawn : MonoBehaviour
{
    Vector3 entrancePosition = new Vector3(0, 0, 0);

    private void Awake()
    {
        // 씬이 로드될 때마다 OnSceneLoaded 메서드를 호출하도록 이벤트에 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // 오브젝트가 파괴될 때 이벤트 등록 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드될 때 실행될 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Camp")
        {
            MovePlayerToEntrance();
        }
    }

    // 플레이어를 입구 위치로 이동시키는 메서드
    void MovePlayerToEntrance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 오브젝트 찾기
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        if (player != null)
        {
            agent.Warp(entrancePosition); // 플레이어 위치 설정
        }
    }
}
