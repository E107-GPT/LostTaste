using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;


// �� �̵� ��Ż 

public class PortalTrigger : MonoBehaviour
{
    public Transform targetPortalLocation;  // �̵��� ��Ż ��ġ

    private Dictionary<string, GameObject> playersInPortal = new Dictionary<string, GameObject>();
    public int totalPlayers = 1;    // �ʿ��� �÷��̾� ��, ���� ������ ���� ���� (������ 1��)

    public string targetMapName;

    public GameObject portal;

    public GameObject itemBox;

    public bool isBossRoom = false;



    // ��Ż Ȱ��ȭ �� ������ ���� Ȱ��ȭ
    public void ActivateItemBox()
    {
        // itemBox가 할당되지 않았다면, 메서드를 종료합니다.
        if (itemBox == null)
        {
            Debug.LogWarning("Item box is not assigned.");
            return;
        }

        itemBox.SetActive(false);

        if (isBossRoom) // �������� ���
        {
            var goldenBox = GetItemBoxByName("Golden");
            if (goldenBox != null)
            {
                goldenBox.SetActive(true); // Golden ���ڸ� Ȱ��ȭ
            }
        }

        else // �������� �ƴ� ��� ���� Ȯ�� ������ ����
        {
            GameObject boxToActivate = null; // Ȱ��ȭ�� ����

            
             boxToActivate = GetItemBoxByName("Wooden");
            

            if (boxToActivate != null)
            {
                boxToActivate.SetActive(true);
            }
        }
    }



    // ���� �̸����� ���� GameObject�� ã�� �޼���
    GameObject GetItemBoxByName(string boxName)
    {
        
        if (itemBox.name.Contains(boxName)) // ���� �̸��� Ȯ��
        {
            return itemBox;
        }
       
        return null; // �ش� �̸��� ���� ���ڰ� ���� ���
    }

    public void ActivatePortal(bool isActive)
    {
        portal.SetActive(isActive);
        Debug.Log("ActivatePortal called with " + isActive);

        // ��Ż�� Ȱ��ȭ�� �� ������ ���ڵ� �Բ� ó��
        if (isActive)
        {
            ActivateItemBox(); // ��Ż Ȱ��ȭ �� ������ ���� Ȱ��ȭ
        }
        else
        {
            // ��Ż�� ��Ȱ��ȭ�� �� ��� ������ ���ڸ� ��Ȱ��ȭ

            
            itemBox.SetActive(false);
            
        }
    }

    // Ʈ���� �����ȿ� ������ �ο��� üũ
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().WarpTo(targetPortalLocation.position);
            //if (PhotonNetwork.InRoom)
            //{
            //    if(totalPlayers != PhotonNetwork.CurrentRoom.PlayerCount)
            //        totalPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
            //}
            //Debug.Log(totalPlayers);
            //Debug.Log("xdlkfs : "+ other.gameObject.name);
            //MonsterManager.Instance.portalTrigger = this;
            //GameObject go;
            ////playersInPortal.Add(other.gameObject.name, other.gameObject);
            //if (playersInPortal.TryGetValue(other.gameObject.name, out go))
            //{
            //    playersInPortal[other.gameObject.name] = other.gameObject;
            //}
            //else
            //{
            //    playersInPortal.Add(other.gameObject.name, other.gameObject);
            //}


            //CheckAllPlayersInPortal();
            //MonsterManager.Instance.portalTrigger = this;
            //MonsterManager.Instance.SpawnMonstersForMap(targetMapName);
            //MonsterManager.Instance.RestartCheckMonstersCoroutine(targetMapName);
            if (GameObject.Find("SpawnMonster").GetComponent<MonsterManager>().monstersInCurrentMap.Count < 1)
            {
                MonsterManager.Instance.portalTrigger = this;
                MonsterManager.Instance.SpawnMonstersForMap(targetMapName);
                Debug.Log("몬스터 생성 완료");
                MonsterManager.Instance.RestartCheckMonstersCoroutine(targetMapName);
            }
        }
    }

    // Ʈ���� ���� ������ ������ �ο��� üũ
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    playersInPortal.Remove(other.gameObject.name);
        //}
    }

    // ��� �÷��̾ ��Ż ��ó�� ������ ����
    private void CheckAllPlayersInPortal()
    {
        if (playersInPortal.Count == totalPlayers)
        {
            Debug.Log(playersInPortal.Count + " / " + totalPlayers);

            // ��� �÷��̾ ��ǥ ��Ż ��ġ�� �̵�
            foreach (KeyValuePair<string, GameObject> player in playersInPortal)
            {
                NavMeshAgent agent = player.Value.GetComponent<NavMeshAgent>();
                Debug.Log($"{agent.gameObject.name}");
                if (agent.transform.GetComponent<PlayerController>().photonView.IsMine == false) continue;
                //Debug.Log($"{agent.gameObject.name}");
                if (agent != null)
                {
                    Debug.Log("있는가?" + player.Value.GetComponent<NavMeshAgent>());

                    //agent.Warp(targetPortalLocation.position);
                    agent.transform.GetComponent<PlayerController>().WarpTo(targetPortalLocation.position);
                }
            }
            portal.SetActive(false);
            MonsterManager.Instance.portalTrigger = this;
            MonsterManager.Instance.SpawnMonstersForMap(targetMapName);
            Debug.Log("몬스터 생성 완료");
            MonsterManager.Instance.RestartCheckMonstersCoroutine(targetMapName);
        }
    }

    public void Clear()
    {
        playersInPortal.Clear();
    }
}
