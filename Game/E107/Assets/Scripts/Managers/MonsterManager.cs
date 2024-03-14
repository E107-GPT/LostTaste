using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// Agent�� �̵��� �� �ִ� ��� => ��Ÿ���� ���������� ������ �� ����غ���.
public enum Location
{
    GROUND = 0,
};
public class MonsterManager : MonoBehaviour
{
    // TYPE�� �þ�� �̷��� �迭�� prefab ������ �߰��Ѵ�.
    [SerializeField]
    private string[] arrayMonsters;     // monster �̸� �迭, Inspector view���� ���� �Է�
    [SerializeField]
    private GameObject monsterPrefab;   // monster TYPE prefab

    private List<BaseController> entitys;  // Monster, Player �� ���� ���� ��� entity�� ���� �� �ִ�.

    public static bool IsGameStop { set; get; } = false;


    private void Awake()
    {
        entitys = new List<BaseController>();

        for (int i = 0; i < arrayMonsters.Length; i++)
        {
            Vector3 pos = new Vector3(5 + i, 0, 5 + i);
            GameObject clone = Instantiate(monsterPrefab, pos, Quaternion.identity);
            MonsterController monster = clone.GetComponent<MonsterController>();
            monster.Setup(arrayMonsters[i]);

            entitys.Add(monster);
        }
    }

    private void Update()
    {
        //if (IsGameStop == true) return;

        //// ��� monsterEntity�� ���۽�Ű�� ���ؼ� Updated()�� ȣ���Ѵ�.
        //for (int i = 0; i < entitys.Count; ++i)
        //{
        //    //if (entitys[i].GetComponent<Monster>().Hp < 0)
        //    //{
        //    //    Destroy(entitys[i], 3.0f);
        //    //    entitys[i] = null;
        //    //    continue;
        //    //}
        //    entitys[i].Updated();
        //}
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < entitys.Count; ++i)
        {
            entitys[i].GetComponent<MonsterController>().FreezeVelocity();
        }
    }

    public static void Stop(EnemyBaseEntity entity)
    {
        IsGameStop = true;
    }
}
