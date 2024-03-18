using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // TYPE�� �þ�� �̷��� �迭�� prefab ������ �߰��Ѵ�.
    [SerializeField]
    private List<GameObject> monsterPrefab;   // monster TYPE prefab
    [SerializeField]
    private List<GameObject> spawnerPos;

    private List<MonsterController> monsters;  // Monster entity�� ��´�
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
                monsterController.Setup(Define.UnitType.Slime.ToString());         // BaseController���� �� ��ü�� �̸��� �ο�
                monsterController.name = $"{monsterController.ID:D2}_Monster_{monsterController.name}";    // 00_Monster_name ���� hierarchy â���� ����

                monsters.Add(monsterController);
            }
            
        }
    }
}
