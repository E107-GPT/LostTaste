using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어가 보스 방에 진입하면 보스 상태 UI를 활성화하는 컴포넌트입니다.
/// </summary>
public class BossStatusUIActivator : MonoBehaviour
{
    // 보스 상태 패널
    [Header("[ 보스 상태 패널 ]")]
    public GameObject bossStatusPanel; // 보스 상태 패널

    // 플레이어가 보스 방에 진입할 때 호출되는 메서드
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossStatusPanel.SetActive(true); // 보스 체력 UI 활성화
        }
    }
}