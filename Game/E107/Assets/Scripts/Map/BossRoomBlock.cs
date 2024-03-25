using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossRoomBlock : MonoBehaviour
{
    private HashSet<GameObject> playersInBossRoom = new HashSet<GameObject>();
    public int totalPlayers = 1;    // 필요한 플레이어 수, 게임 설정에 따라 조정 (지금은 1명)

    public GameObject bossRoomWall;

    public GameObject bossRoomEffect;

    private void Awake()
    {
        if (bossRoomWall != null)
        {
            bossRoomWall.SetActive(false); // 초기에는 벽을 비활성화 상태로 둡니다.
            //Debug.Log("Boss room wall is deactivated");
        }

        if (bossRoomEffect != null)
        {
            bossRoomEffect.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInBossRoom.Add(other.gameObject);

            if (playersInBossRoom.Count == totalPlayers)
            {
                Debug.Log("All players are in the boss room");
                if (bossRoomWall != null)
                {
                    bossRoomWall.SetActive(true);
                    Debug.Log("Boss room wall is activated");
                }
                if (bossRoomEffect != null)
                {
                    bossRoomEffect.SetActive(true);
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInBossRoom.Remove(other.gameObject); // 플레이어가 방을 나가면 집합에서 제거
        }
    }

}
