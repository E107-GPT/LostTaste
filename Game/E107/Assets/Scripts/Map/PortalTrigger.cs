using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;


// 방 이동 포탈 

public class PortalTrigger : MonoBehaviour
{
    public Transform targetPortalLocation;  // 이동할 포탈 위치

    private Dictionary<string, GameObject> playersInPortal = new Dictionary<string, GameObject>();
    public int totalPlayers = 1;    // 필요한 플레이어 수, 게임 설정에 따라 조정 (지금은 1명)

    public string targetMapName;

    public GameObject portal;


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
            if(totalPlayers != PhotonNetwork.CurrentRoom.PlayerCount)
                totalPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
            Debug.Log(totalPlayers);

            MonsterManager.Instance.portalTrigger = this;
            playersInPortal.Add(other.GetComponent<PlayerController>().entityName, other.gameObject);
            CheckAllPlayersInPortal();
            MonsterManager.Instance.SpawnMonstersForMap(targetMapName);

            // "PortalToCamp" 포탈을 통과할 때만 플레이어의 HP를 초기화
            if (portal.name == "PortalToCamp")
            {
                // 플레이어의 HP를 초기화하는 로직
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.ResetHP(); // PlayerController 내의 HP 초기화 메서드 호출
                }

            }
        }
    }

    // 트리거 범위 밖으로 나가면 인원수 체크
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInPortal.Remove(other.gameObject.GetComponent<PlayerController>().entityName);
        }
    }

    // 모든 플레이어가 포탈 근처에 있을때 실행
    private void CheckAllPlayersInPortal()
    {
        if (playersInPortal.Count == totalPlayers)
        {
            Debug.Log(playersInPortal.Count + " / " + totalPlayers);

            // 모든 플레이어를 목표 포탈 위치로 이동
            foreach (KeyValuePair<string, GameObject> player in playersInPortal)
            {
                NavMeshAgent agent = player.Value.GetComponent<NavMeshAgent>();
                if (agent != null)
                {
                    Debug.Log("정상적으로 가는거같아");
                    agent.Warp(targetPortalLocation.position);  
                }
                else
                {
                    Debug.Log("이상한데로 가는거같아");
                    player.Value.transform.position = targetPortalLocation.position;
                }
            }
            portal.SetActive(false);
        }
    }
}
