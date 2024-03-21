using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance { get; private set; }

    // 각 맵에 소환된 몬스터 리스트
    public List<GameObject> monstersInCurrentMap = new List<GameObject>();

    public PortalTrigger portalTrigger;

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

        StartCoroutine(CheckMonstersCoroutine());
    }

    IEnumerator CheckMonstersCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            monstersInCurrentMap.RemoveAll(monster => monster == null);
            Debug.Log(monstersInCurrentMap.Count);
            
            if (monstersInCurrentMap.Count == 0)
            {
                Debug.Log("카운트 0되서 활성화 되야함");
                if (portalTrigger != null)
                {
                    portalTrigger.ActivatePortal(true);
                }
            }
            else
            {
                Debug.Log("카운트 0아니여서 비활성화 되야함");
                if (portalTrigger != null)
                {
                    portalTrigger.ActivatePortal(false);
                }
            }
        }
    }

    // 특정 맵에 몬스터 소환
    public void SpawnMonstersForMap(string mapName)
    {
        foreach (MonsterSpawnInfo info in monsterSpawnInfos)
        {
            if (info.mapName == mapName)
            {
                foreach (Transform spawnPoint in info.spawnPoints)
                {
                    GameObject clone = Instantiate(info.monsterPrefab, spawnPoint.position, spawnPoint.rotation);
                    monstersInCurrentMap.Add(clone);
                }
                break;
            }
        }
    }

}
