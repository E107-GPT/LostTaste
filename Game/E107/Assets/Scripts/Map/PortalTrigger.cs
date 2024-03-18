using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

// �� �̵� ��Ż 

public class PortalTrigger : MonoBehaviour
{
    public Transform targetPortalLocation;  // �̵��� ��Ż ��ġ

    private HashSet<GameObject> playersInPortal = new HashSet<GameObject>();
    public int totalPlayers = 1;    // �ʿ��� �÷��̾� ��, ���� ������ ���� ���� (������ 1��)

    public string targetMapName;

    public GameObject portal;

    //private void Update()
    //{
    //    int monstersCount = MonsterManager.Instance.GetMonstersCount();

    //    // monstersCount�� 0�� �ƴϸ� ��Ż�� ��Ȱ��ȭ, 0�̸� Ȱ��ȭ
    //    portal.SetActive(monstersCount == 0);
    //}

    public void ActivatePortal(bool isActive)
    {
        portal.SetActive(isActive);
        Debug.Log("ActivatePortal called with " + isActive);
    }

    // Ʈ���� �����ȿ� ������ �ο��� üũ
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

    // Ʈ���� ���� ������ ������ �ο��� üũ
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInPortal.Remove(other.gameObject);
        }
    }

    // ��� �÷��̾ ��Ż ��ó�� ������ ����
    private void CheckAllPlayersInPortal()
    {
        if (playersInPortal.Count == totalPlayers)
        {
            // ��� �÷��̾ ��ǥ ��Ż ��ġ�� �̵�
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
