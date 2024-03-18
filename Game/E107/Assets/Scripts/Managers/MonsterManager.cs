using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class MonsterManager : MonoBehaviour
{
    // TYPE이 늘어나면 이러한 배열과 prefab 변수를 추가한다.
    //[SerializeField]
    //private string[] monsterNames;     // monster 이름 배열, Inspector view에서 직접 입력
    //[SerializeField]
    //private GameObject monsterPrefab;   // monster TYPE prefab

    //private List<MonsterController> entitys;  // Monster entity를 담는다

    public static MonsterManager Instance { get; private set; }

    [System.Serializable]
    public class MonsterSpawnInfo
    {
        public string mapName;
        public GameObject monsterPrefab;
        public Transform[] spawnPoints;
    }

    public List<MonsterSpawnInfo> monsterSpawnInfos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        //entitys = new List<MonsterController>();

        //for (int i = 0; i < monsterNames.Length; i++)
        //{
        //    Vector3 pos = new Vector3(0, 5, 300);
        //    GameObject clone = Instantiate(monsterPrefab, pos, Quaternion.identity);
        //    MonsterController monsterController = clone.GetComponent<MonsterController>();

        //    monsterController.Setup(Define.UnitType.DrillDuck.ToString());         // BaseController에서 각 객체의 이름을 부여
        //    monsterController.name = $"{monsterController.ID:D2}_Monster_{monsterController.name}";    // 00_Monster_name 으로 hierarchy 창에서 보임

        //    entitys.Add(monsterController);
        //}
    }

    private void Update()
    {
        // Monster 동작은 BaseController에서 Update() 중
        // 여기서도 MonsterController를 Init()하면 애니메이션이 동작하지 않는다.

        //for (int i = 0; i < entitys.Count; ++i)
        //{
        //    //if (entitys[i].GetComponent<Monster>().Hp < 0)
        //    //{
        //    //    Destroy(entitys[i], 3.0f);
        //    //    entitys[i] = null;
        //    //    continue;
        //    //}
        //    entitys[i].Init();
        //}
    }

    // 특정 맵에 몬스터 소환
    public void SpawnMonstersForMap(string mapName)
    {
        foreach (MonsterSpawnInfo info in monsterSpawnInfos)
        {
            if ( info == null)
            {
                Debug.Log("info is null");
            }

            if (info.mapName == mapName)
            {
                foreach (Transform spawnPoint in info.spawnPoints)
                {
                    GameObject clone = Instantiate(info.monsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    Debug.Log(clone);
                }
                break;
            }
        }
    }

    //private void FixedUpdate()
    //{
    //    for (int i = 0; i < entitys.Count; ++i)
    //    {
    //        entitys[i].GetComponent<MonsterController>().FreezeVelocity();
    //    }
    //}
}
