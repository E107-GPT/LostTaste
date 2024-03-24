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
            if(totalPlayers != PhotonNetwork.CurrentRoom.PlayerCount)
                totalPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
            Debug.Log(totalPlayers);

            MonsterManager.Instance.portalTrigger = this;
            playersInPortal.Add(other.GetComponent<PlayerController>().entityName, other.gameObject);
            CheckAllPlayersInPortal();
            MonsterManager.Instance.SpawnMonstersForMap(targetMapName);

            // "PortalToCamp" ��Ż�� ����� ���� �÷��̾��� HP�� �ʱ�ȭ
            if (portal.name == "PortalToCamp")
            {
                // �÷��̾��� HP�� �ʱ�ȭ�ϴ� ����
                PlayerController playerController = other.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.ResetHP(); // PlayerController ���� HP �ʱ�ȭ �޼��� ȣ��
                }

            }
        }
    }

    // Ʈ���� ���� ������ ������ �ο��� üũ
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInPortal.Remove(other.gameObject.GetComponent<PlayerController>().entityName);
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
                    Debug.Log("���������� ���°Ű���");
                    agent.Warp(targetPortalLocation.position);  
                }
                else
                {
                    Debug.Log("�̻��ѵ��� ���°Ű���");
                    player.Value.transform.position = targetPortalLocation.position;
                }
            }
            portal.SetActive(false);
        }
    }
}
