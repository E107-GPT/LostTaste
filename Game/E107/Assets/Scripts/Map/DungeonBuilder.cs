using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonBuilder : MonoBehaviour
{
    public GameObject entrancePrefab;
    public GameObject bossPrefab;
    public GameObject monster01Prefab;
    public GameObject monster02Prefab;
    public GameObject benefitPrefab;
    public GameObject shopPrefab;
    public GameObject trapPrefab;

    Vector3 entrancePosition = new Vector3(0.14f, 0.5f, -12.2f);


    private void Start()
    {
        //BuildDungeon();
        MovePlayerToEntrance();
    }


    void BuildDBuildDungeon()
    {

    }


    // 플레이어를 입구방으로 이동
    void MovePlayerToEntrance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = entrancePosition;
        }
        else
        {
            Debug.LogError("Player not found.");
        }
    }
}

