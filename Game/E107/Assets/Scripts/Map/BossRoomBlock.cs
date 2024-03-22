using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class BossRoomBlock : MonoBehaviour
{
    private HashSet<GameObject> playersInBossRoom = new HashSet<GameObject>();
    public int totalPlayers = 1;    // �ʿ��� �÷��̾� ��, ���� ������ ���� ���� (������ 1��)

    public GameObject bossRoomWall;

    private void Awake()
    {
        if (bossRoomWall != null)
        {
            bossRoomWall.SetActive(false); // �ʱ⿡�� ���� ��Ȱ��ȭ ���·� �Ӵϴ�.
            Debug.Log("Boss room wall is deactivated");
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
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInBossRoom.Remove(other.gameObject); // �÷��̾ ���� ������ ���տ��� ����
        }
    }

}