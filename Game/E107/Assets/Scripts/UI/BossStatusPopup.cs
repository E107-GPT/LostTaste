using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatusPopup : MonoBehaviour
{
    public GameObject bossStatusPanel;

    private bool playerIsInBossRoom = false;

    void Update()
    {
        // 플레이어가 보스 방에 있고 보스가 존재하는 경우
        if (playerIsInBossRoom)
        {
            // 보스 체력 UI 활성화
            bossStatusPanel.SetActive(true);
        }
    }

    // 플레이어가 보스 방에 진입할 때 호출되는 함수
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInBossRoom = true;
        }
    }
}