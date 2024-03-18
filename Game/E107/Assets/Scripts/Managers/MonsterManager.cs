using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class MonsterManager : MonoBehaviour
{
    // TYPE�� �þ�� �̷��� �迭�� prefab ������ �߰��Ѵ�.
    //[SerializeField]
    //private string[] monsterNames;     // monster �̸� �迭, Inspector view���� ���� �Է�
    //[SerializeField]
    //private GameObject monsterPrefab;   // monster TYPE prefab

    //private List<MonsterController> entitys;  // Monster entity�� ��´�

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

        //    monsterController.Setup(Define.UnitType.DrillDuck.ToString());         // BaseController���� �� ��ü�� �̸��� �ο�
        //    monsterController.name = $"{monsterController.ID:D2}_Monster_{monsterController.name}";    // 00_Monster_name ���� hierarchy â���� ����

        //    entitys.Add(monsterController);
        //}
    }

    private void Update()
    {
        // Monster ������ BaseController���� Update() ��
        // ���⼭�� MonsterController�� Init()�ϸ� �ִϸ��̼��� �������� �ʴ´�.

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

    // Ư�� �ʿ� ���� ��ȯ
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
