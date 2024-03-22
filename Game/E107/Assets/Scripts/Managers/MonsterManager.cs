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
            //Debug.Log(monstersInCurrentMap.Count);

            if (monstersInCurrentMap.Count == 0)
            {
                // ���Ͱ� ��� ���ŵǸ� ��Ż Ȱ��ȭ
                if (portalTrigger != null)
                {
                    portalTrigger.ActivatePortal(true);
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
                    GameObject clone = Instantiate(spawnInfo.monsterPrefab, spawnInfo.spawnPoint.position, spawnInfo.spawnPoint.rotation);
                    monstersInCurrentMap.Add(clone);
                }
                break;
            }
        }
    }

}
