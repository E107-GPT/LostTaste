using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class MonsterManager : MonoBehaviour
{
    public static MonsterManager Instance { get; private set; }

    // �� �ʿ� ��ȯ�� ���� ����Ʈ
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
        public GameObject monsterPrefab; // �� ���� ����Ʈ���� ���� ������ ����
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
                // ���Ͱ� ��� ���ŵǸ� ��Ż Ȱ��ȭ
                if (portalTrigger != null)
                {
                    portalTrigger.ActivatePortal(true);
                    monstersCleared = true;
                    StopCoroutine("CheckMonstersCoroutine");
                }
            }
            else
            {
                Debug.Log("ī��Ʈ 0�ƴϿ��� ��Ȱ��ȭ �Ǿ���");
                if (portalTrigger != null)
                {
                    portalTrigger.ActivatePortal(false);
                }
            }
        } 
    }

    // Ư�� �ʿ� ���� ��ȯ
    public void SpawnMonstersForMap(string mapName)
    {
        foreach (MonsterSpawnInfo info in monsterSpawnInfos)
        {
            if (info.mapName == mapName)
            {
                foreach (SpawnPointInfo spawnInfo in info.spawnPoints)
                {
                    // �� ���� ����Ʈ���� ������ ���� ���������� ���͸� ��ȯ
                    GameObject clone = null;
                    Debug.Log($"����ΰ�?{PhotonNetwork.InRoom}");
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
