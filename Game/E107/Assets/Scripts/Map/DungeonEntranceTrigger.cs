using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DungeonEntranceTrigger : MonoBehaviour
{
    private HashSet<string> playersInPortal = new HashSet<string>();

    public int totalPlayers;

    public List<GameObject> portal = new List<GameObject>();

    private bool portalActivated = false;

    private void Start()
    {
        //totalPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
        //totalPlayers = 1;
        //portal.SetActive(false);
        //Debug.Log("��Ż ��Ȱ��ȭ");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("�÷��̾ ��Ż�� ������");
            //string playerName = other.gameObject.name;
            //playersInPortal.Add(playerName);

            //if (playersInPortal.Count == totalPlayers)
            //{
            //    portal.SetActive(true); 
            //    Debug.Log("Portal activated");
            //    portalActivated = true;
            //}
            //portal.SetActive(true);

            foreach (var p in portal)
            {
                p.SetActive(true);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    string playerName = other.gameObject.name;
        //    playersInPortal.Remove(playerName);
        //}
    }
}
