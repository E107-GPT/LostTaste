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
        // �÷��̾ ���� �濡 �ְ� ������ �����ϴ� ���
        if (playerIsInBossRoom)
        {
            // ���� ü�� UI Ȱ��ȭ
            bossStatusPanel.SetActive(true);
        }
    }

    // �÷��̾ ���� �濡 ������ �� ȣ��Ǵ� �Լ�
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsInBossRoom = true;
        }
    }
}