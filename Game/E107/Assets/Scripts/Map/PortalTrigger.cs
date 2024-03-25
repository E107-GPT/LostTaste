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

    public GameObject[] itemBoxes;

    public bool isBossRoom = false;


    // ��Ż Ȱ��ȭ �� ������ ���� Ȱ��ȭ
    public void ActivateItemBox()
    {
        // ��� ������ ���ڸ� ���� ��Ȱ��ȭ
        foreach (var box in itemBoxes)
        {
            box.SetActive(false);
        }

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
            float chance = Random.Range(0f, 100f); // 0���� 100 ������ ���� ����
            GameObject boxToActivate = null; // Ȱ��ȭ�� ����

            if (chance <= 5f) // Golden ���� Ȯ�� 5%
            {
                boxToActivate = GetItemBoxByName("Golden");
            }
            else if (chance <= 45f) // Better ���� Ȯ�� 25%
            {
                boxToActivate = GetItemBoxByName("Better");
            }
            else // Wooden ���� Ȯ�� 70%
            {
                boxToActivate = GetItemBoxByName("Wooden");
            }

            if (boxToActivate != null)
            {
                boxToActivate.SetActive(true);
            }
        }
    }

    // ���� �̸����� ���� GameObject�� ã�� �޼���
    GameObject GetItemBoxByName(string boxName)
    {
        foreach (var box in itemBoxes)
        {
            if (box.name.Contains(boxName)) // ���� �̸��� Ȯ��
            {
                return box;
            }
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
            foreach (var box in itemBoxes)
            {
                box.SetActive(false);
            }
        }
    }

    // Ʈ���� �����ȿ� ������ �ο��� üũ
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {           
            if(totalPlayers != PhotonNetwork.CurrentRoom.PlayerCount)
                totalPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
            Debug.Log(totalPlayers);
            Debug.Log("xdlkfs : "+ other.gameObject.name);

            MonsterManager.Instance.portalTrigger = this;
            playersInPortal.Add(other.gameObject.name, other.gameObject);
            CheckAllPlayersInPortal();
            //MonsterManager.Instance.portalTrigger = this;
            //MonsterManager.Instance.SpawnMonstersForMap(targetMapName);
            //MonsterManager.Instance.RestartCheckMonstersCoroutine(targetMapName);

        }
    }

    // Ʈ���� ���� ������ ������ �ο��� üũ
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInPortal.Remove(other.gameObject.name);
        }
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
                if (agent != null)
                {
                    Debug.Log("있는가?" + player.Value.GetComponent<NavMeshAgent>());

                    agent.Warp(targetPortalLocation.position);  
                }
                else
                {
                    Debug.Log("왜 없지?" + player.Value.GetComponent<NavMeshAgent>());
                    player.Value.transform.position = targetPortalLocation.position;
                }
            }
            portal.SetActive(false);
            MonsterManager.Instance.portalTrigger = this;
            MonsterManager.Instance.SpawnMonstersForMap(targetMapName);
            MonsterManager.Instance.RestartCheckMonstersCoroutine(targetMapName);
        }
    }
}
