using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// Agent가 이동할 수 있는 장소 => 나타나는 스테이지를 지정할 때 사용해보자.
public enum Location
{
    GROUND = 0,
};
public class MonsterManager : MonoBehaviour
{
    // TYPE이 늘어나면 이러한 배열과 prefab 변수를 추가한다.
    [SerializeField]
    private string[] arrayMonsters;     // monster 이름 배열, Inspector view에서 직접 입력
    [SerializeField]
    private GameObject monsterPrefab;   // monster TYPE prefab

    private List<BaseController> entitys;  // Monster, Player 등 게임 상의 모든 entity를 담을 수 있다.

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

        //// 모든 monsterEntity를 동작시키기 위해서 Updated()를 호출한다.
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
