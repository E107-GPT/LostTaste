using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Slime,
    DrillDuck
}

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private List<StatTest> statTests;
    [SerializeField]
    private GameObject monsterPrefab;

    void Start()
    {
        for (int i = 0; i < statTests.Count; i++)
        {
            
        }
    }

    public DrillDuck SpawnDrillDuck(MonsterType monsterType)
    {
        DrillDuck drillDuck = Instantiate(monsterPrefab).GetComponent<DrillDuck>();
        drillDuck.DrillDuckData = statTests[(int)monsterType];
        return drillDuck;
    }
}
