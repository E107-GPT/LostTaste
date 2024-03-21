using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // TYPE이 늘어나면 이러한 배열과 prefab 변수를 추가한다.
    [SerializeField]
    private List<GameObject> monsterPrefab;   // monster TYPE prefab
    [SerializeField]
    private List<GameObject> spawnerPos;

    private List<MonsterController> monsters;  // Monster entity를 담는다
    private List<DrillDuckController> drillDucks;

    private void Awake()
    {
        monsters = new List<MonsterController>();
        drillDucks = new List<DrillDuckController>();

        for (int i = 0; i < monsterPrefab.Count; i++)
        {
            GameObject clone = Instantiate(monsterPrefab[i], spawnerPos[i].transform.position, Quaternion.identity);

            if (clone.GetComponent<DrillDuckController>() != null)
            {
                DrillDuckController drillDuckController = clone.GetComponent<DrillDuckController>();
                drillDuckController.Setup(Define.UnitType.DrillDuck.ToString());
                drillDuckController.name = $"{drillDuckController.ID:D2}_DrillDuck_{drillDuckController.name}";

                drillDucks.Add(drillDuckController);
            }
            else if (clone.GetComponent<MonsterController>() != null)
            {
                MonsterController monsterController = clone.GetComponent<MonsterController>();
                monsterController.Setup(Define.UnitType.Slime.ToString());         // BaseController에서 각 객체의 이름을 부여
                monsterController.name = $"{monsterController.ID:D2}_Monster_{monsterController.name}";    // 00_Monster_name 으로 hierarchy 창에서 보임

                monsters.Add(monsterController);
            }
            
        }
    }
}
