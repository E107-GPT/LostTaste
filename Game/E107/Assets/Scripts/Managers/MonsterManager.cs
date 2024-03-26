using Photon.Pun;
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

    //public string targetMapName;

    [System.Serializable]
    public class MonsterSpawnInfo
    {
        public string mapName;
        public SpawnPointInfo[] spawnPoints;
    }

    [System.Serializable]
    public class SpawnPointInfo
    {
        public Transform spawnPoint;
        public GameObject monsterPrefab; // 각 스폰 포인트별로 몬스터 프리팹 지정
    }

    public List<MonsterSpawnInfo> monsterSpawnInfos;

    private bool continueCheckingMonsters = true;

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

        //StartCoroutine(CheckMonstersCoroutine());
    }

    IEnumerator CheckMonstersCoroutine()
    {
        bool monstersCleared = false;

        while (!monstersCleared)
        {
            yield return new WaitForSeconds(0.5f);

            monstersInCurrentMap.RemoveAll(monster => monster == null);

            if (monstersInCurrentMap.Count == 0)
            {
                // 몬스터가 모두 제거되면 포탈 활성화
                if (portalTrigger != null)
                {
                    portalTrigger.ActivatePortal(true);
                    monstersCleared = true;
                    StopCoroutine("CheckMonstersCoroutine");
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
                foreach (SpawnPointInfo spawnInfo in info.spawnPoints)
                {
                    // 각 스폰 포인트별로 지정된 몬스터 프리팹으로 몬스터를 소환
                    GameObject clone = null;
                    Debug.Log($"방안인가?{PhotonNetwork.InRoom}");
                    if (PhotonNetwork.IsConnected && !PhotonNetwork.InRoom) clone = Instantiate(spawnInfo.monsterPrefab, spawnInfo.spawnPoint.position, spawnInfo.spawnPoint.rotation);
                    else if (PhotonNetwork.IsMasterClient) clone = PhotonNetwork.Instantiate($"Prefabs/Monster/{spawnInfo.monsterPrefab.name}", spawnInfo.spawnPoint.position, spawnInfo.spawnPoint.rotation);
                    monstersInCurrentMap.Add(clone);
                    
                }
                break;
            }
        }
    }

    public void RestartCheckMonstersCoroutine(string newMapName)
    {
        StartCoroutine(CheckMonstersCoroutine());
    }
}
