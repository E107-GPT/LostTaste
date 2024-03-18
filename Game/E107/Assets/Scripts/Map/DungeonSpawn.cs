using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class DungeonSpawn : MonoBehaviour
{
    Vector3 entrancePosition = new Vector3(0.09f, 0, -12.11f);


    private void Start()
    {
        MovePlayerToEntrance();
    }

    // 플레이어를 입구방으로 이동
    void MovePlayerToEntrance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            Debug.Log("Player found.");
            //player.transform.position = entrancePosition;
            agent.Warp(entrancePosition);
            Debug.Log("Player moved to entrance.");
        }
        else
        {
            Debug.LogError("Player not found.");
        }
    }
}

