using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class DungeonBuilder : MonoBehaviour
{
    public GameObject entrancePrefab;
    public GameObject bossPrefab;
    public GameObject monster01Prefab;
    public GameObject monster02Prefab;
    public GameObject benefitPrefab;
    public GameObject shopPrefab;
    public GameObject trapPrefab;

    Vector3 entrancePosition = new Vector3(0.09f, 0, -12.11f);


    private void Start()
    {
        //BuildDungeon();
        MovePlayerToEntrance();
    }


    void BuildDungeon()
    {

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

