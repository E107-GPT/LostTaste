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
        public GameObject monsterPrefab; // 각 스폰 포인트별로 몬스터 프리팹 지정
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
                // 몬스터가 모두 제거되면 포탈 활성화
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

    // 특정 맵에 몬스터 소환
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
        StartCoroutine(CheckMonstersCoroutine(newMapName));
    }

    public void SendMonsterSpawnMsg(string mapName)
    {
        Debug.Log("몬스터 스폰 해달라고 메세지를 보냄");
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
        Debug.Log("코루틴이 돌아가요");
        RestartCheckMonstersCoroutine(_curMap);
    }
}
