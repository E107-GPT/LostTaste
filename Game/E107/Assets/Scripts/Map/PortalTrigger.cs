using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

// 방 이동 포탈 

public class PortalTrigger : MonoBehaviour
{
    public Transform targetPortalLocation;  // 이동할 포탈 위치

    private HashSet<GameObject> playersInPortal = new HashSet<GameObject>();
    public int totalPlayers = 1;    // 필요한 플레이어 수, 게임 설정에 따라 조정 (지금은 1명)

    public string targetMapName;

    public GameObject portal;

    //private void Update()
    //{
    //    int monstersCount = MonsterManager.Instance.GetMonstersCount();

    //    // monstersCount가 0이 아니면 포탈을 비활성화, 0이면 활성화
    //    portal.SetActive(monstersCount == 0);
    //}

    public void ActivatePortal(bool isActive)
    {
        portal.SetActive(isActive);
        Debug.Log("ActivatePortal called with " + isActive);
    }

    // 트리거 범위안에 들어오면 인원수 체크
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInPortal.Add(other.gameObject);
            CheckAllPlayersInPortal();
            MonsterManager.Instance.SetPortalTrigger(this);
            MonsterManager.Instance.SpawnMonstersForMap(targetMapName);
        }
    }

    // 트리거 범위 밖으로 나가면 인원수 체크
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInPortal.Remove(other.gameObject);
        }
    }

    // 모든 플레이어가 포탈 근처에 있을때 실행
    private void CheckAllPlayersInPortal()
    {
        if (playersInPortal.Count == totalPlayers)
        {
            // 모든 플레이어를 목표 포탈 위치로 이동
            foreach (GameObject player in playersInPortal)
            {
                NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    agent.Warp(targetPortalLocation.position);
                }
                else
                {
                    player.transform.position = targetPortalLocation.position;
                }
            }
            
        }
    }
}
