using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 씬 전환 포탈 또는 게임 종료시 작동

public class SceneChange : MonoBehaviour
{
    private HashSet<GameObject> playersInPortal = new HashSet<GameObject>();

    // 필요한 플레이어 수, 게임 설정에 따라 조정 (지금은 1명)
    public int totalPlayers = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInPortal.Add(other.gameObject);
            CheckAllPlayersInPortal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInPortal.Remove(other.gameObject);
        }
    }

    private void CheckAllPlayersInPortal()
    {
        if (playersInPortal.Count == totalPlayers)
        {
            // 모든 플레이어가 포탈에 도달했을 때 실행할 로직
            DungeonSceneManager.Instance.EnterDungeon();
        }
    }
}
