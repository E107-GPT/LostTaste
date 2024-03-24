using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
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

    public GameObject[] itemBoxes;

    public bool isBossRoom = false;


    // 포탈 활성화 시 아이템 상자 활성화
    public void ActivateItemBox()
    {
        // 모든 아이템 상자를 먼저 비활성화
        foreach (var box in itemBoxes)
        {
            box.SetActive(false);
        }

        if (isBossRoom) // 보스방인 경우
        {
            var goldenBox = GetItemBoxByName("Golden");
            if (goldenBox != null)
            {
                goldenBox.SetActive(true); // Golden 상자만 활성화
            }
        }

        else // 보스방이 아닌 경우 기존 확률 로직을 따름
        {
            float chance = Random.Range(0f, 100f); // 0에서 100 사이의 랜덤 숫자
            GameObject boxToActivate = null; // 활성화할 상자

            if (chance <= 5f) // Golden 상자 확률 5%
            {
                boxToActivate = GetItemBoxByName("Golden");
            }
            else if (chance <= 45f) // Better 상자 확률 25%
            {
                boxToActivate = GetItemBoxByName("Better");
            }
            else // Wooden 상자 확률 70%
            {
                boxToActivate = GetItemBoxByName("Wooden");
            }

            if (boxToActivate != null)
            {
                boxToActivate.SetActive(true);
            }
        }
    }

    // 상자 이름으로 상자 GameObject를 찾는 메서드
    GameObject GetItemBoxByName(string boxName)
    {
        foreach (var box in itemBoxes)
        {
            if (box.name.Contains(boxName)) // 상자 이름을 확인
            {
                return box;
            }
        }
        return null; // 해당 이름을 가진 상자가 없는 경우
    }

    public void ActivatePortal(bool isActive)
    {
        portal.SetActive(isActive);
        Debug.Log("ActivatePortal called with " + isActive);

        // 포탈을 활성화할 때 아이템 상자도 함께 처리
        if (isActive)
        {
            ActivateItemBox(); // 포탈 활성화 시 아이템 상자 활성화
        }
        else
        {
            // 포탈을 비활성화할 때 모든 아이템 상자를 비활성화
            foreach (var box in itemBoxes)
            {
                box.SetActive(false);
            }
        }
    }

    // 트리거 범위안에 들어오면 인원수 체크
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MonsterManager.Instance.portalTrigger = this;
            playersInPortal.Add(other.gameObject);
            CheckAllPlayersInPortal();
            MonsterManager.Instance.SpawnMonstersForMap(targetMapName);
            MonsterManager.Instance.RestartCheckMonstersCoroutine(targetMapName);

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
                Debug.Log(player.gameObject.name);
            }
            portal.SetActive(false);
        }
    }
}
