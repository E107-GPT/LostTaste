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

    PhotonView photonView;

    public List<GameObject> PortalList;

    private string _curMap;

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
            photonView = GetComponent<PhotonView>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //StartCoroutine(CheckMonstersCoroutine());
    }

    IEnumerator CheckMonstersCoroutine(string mapName)
    {
        bool monstersCleared = false;

        PortalTrigger portal = PortalList.Find((e) =>  e.transform.root.name == mapName ).GetComponent<PortalTrigger>();

        Debug.Log(portal.name);
        while (!monstersCleared)
        {
            yield return new WaitForSeconds(0.5f);

            monstersInCurrentMap.RemoveAll(monster => monster == null);

            if (monstersInCurrentMap.Count == 0)
            {
                // ���Ͱ� ��� ���ŵǸ� ��Ż Ȱ��ȭ
                //if (portalTrigger != null)
                //{
                //    portalTrigger.ActivatePortal(true);
                //    monstersCleared = true;
                //    SendActivatePortal();
                //    StopCoroutine("CheckMonstersCoroutine");
                //}

                //portal.ActivatePortal(true);
                portal.gameObject.SetActive(true);
                portal.ActivateItemBox();
                monstersCleared = true;
                SendActivatePortal(portal.name);
                StopCoroutine("CheckMonstersCoroutine");

            }
        }
    }

    // Ư�� �ʿ� ���� ��ȯ
    public void SpawnMonstersForMap(string mapName)
    {
        _curMap = mapName;
        if (monstersInCurrentMap.Count != 0) return;
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
        StartCoroutine(CheckMonstersCoroutine(newMapName));
    }

    public void SendMonsterSpawnMsg(string mapName)
    {
        Debug.Log("���� ���� �ش޶�� �޼����� ����");
        photonView.RPC("RPC_SpawnMonster", RpcTarget.MasterClient, mapName);
    }

    [PunRPC]
    void RPC_SpawnMonster(string mapName)
    {
        SpawnMonstersForMap(mapName);
        RestartCheckMonstersCoroutine(mapName);
    }

    public void SendActivatePortal(string portalName)
    {
        photonView.RPC("RPC_ActivatePortal",RpcTarget.Others, portalName);
        
    }

    [PunRPC]
    void RPC_ActivatePortal(string portalName)
    {
        //portalTrigger.ActivatePortal(true);
        Debug.Log(portalName);
        Debug.Log(PortalList.Find((e) => e.name == portalName));
        GameObject go = PortalList.Find((e) => e.name == portalName);
        go.SetActive(true);
        go.GetComponent<PortalTrigger>().ActivateItemBox();

    }

    public void SendReStartMsg()
    {
        photonView.RPC("ReStartManage", RpcTarget.Others);
    }

    [PunRPC]
    void ReStartManage()
    {
        foreach(var monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            monstersInCurrentMap.Add(monster);
        }
        Debug.Log("�ڷ�ƾ�� ���ư���");
        RestartCheckMonstersCoroutine(_curMap);
    }
}
