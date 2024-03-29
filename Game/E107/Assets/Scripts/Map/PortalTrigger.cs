using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;


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

    public string bgmName;


    private void Awake()
    {
        gameObject.SetActive(false);
        if (itemBox == null) return;
        itemBox.SetActive(false);
    }

    private void Start()
    {
    }



    public Color nextBackgroundColor; // 변경할 배경색

    // ��Ż Ȱ��ȭ �� ������ ���� Ȱ��ȭ
    public void ActivateItemBox()
    {
        // itemBox가 할당되지 않았다면, 메서드를 종료합니다.
        if (itemBox == null)
        {
            Debug.LogWarning("Item box is not assigned.");
            return;
        }

        itemBox.SetActive(true);

        //itemBox.SetActive(false);

        //if (isBossRoom) // �������� ���
        //{
        //    var goldenBox = GetItemBoxByName("Golden");
        //    if (goldenBox != null)
        //    {
        //        goldenBox.SetActive(true); // Golden ���ڸ� Ȱ��ȭ
        //    }
        //}

        //else // �������� �ƴ� ��� ���� Ȯ�� ������ ����
        //{
        //    GameObject boxToActivate = null; // Ȱ��ȭ�� ����

            
        //     boxToActivate = GetItemBoxByName("Wooden");
            

        //    if (boxToActivate != null)
        //    {
        //        boxToActivate.SetActive(true);
        //    }
        //}
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

    private void ChangeCameraBackgroundColor()
    {
        Camera.main.backgroundColor = nextBackgroundColor;
    }

    public void ActivatePortal(bool isActive)
    {
        portal.SetActive(isActive);
        Debug.Log("ActivatePortal called with " + isActive);

        // ��Ż�� Ȱ��ȭ�� �� ������ ���ڵ� �Բ� ó��
        if (isActive)
        {
            //ChangeCameraBackgroundColor();
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
            ChangeCameraBackgroundColor();

            if (!string.IsNullOrEmpty(bgmName))
            {

                Managers.Sound.Clear();
                Managers.Sound.Play(bgmName, Define.Sound.BGM, 1.0f, 0.3f);
                
            }

            MonsterManager.Instance._curMap = targetMapName;
            if (!PhotonNetwork.IsMasterClient)
            {
                MonsterManager.Instance.SendMonsterSpawnMsg(targetMapName);
                
                return;
            }

            MonsterManager.Instance.SpawnMonstersForMap(targetMapName);
            Debug.Log("몬스터 생성 완료");
            MonsterManager.Instance.RestartCheckMonstersCoroutine(targetMapName);
        }
    }

    // Ʈ���� ���� ������ ������ �ο��� üũ
    private void OnTriggerExit(Collider other)
    {
    }

    // ��� �÷��̾ ��Ż ��ó�� ������ ����
    //private void CheckAllPlayersInPortal()
    //{
    //    if (playersInPortal.Count == totalPlayers)
    //    {
    //        Debug.Log(playersInPortal.Count + " / " + totalPlayers);

    //        // ��� �÷��̾ ��ǥ ��Ż ��ġ�� �̵�
    //        foreach (KeyValuePair<string, GameObject> player in playersInPortal)
    //        {
    //            NavMeshAgent agent = player.Value.GetComponent<NavMeshAgent>();
    //            Debug.Log($"{agent.gameObject.name}");
    //            if (agent.transform.GetComponent<PlayerController>().photonView.IsMine == false) continue;
    //            //Debug.Log($"{agent.gameObject.name}");
    //            if (agent != null)
    //            {
    //                Debug.Log("있는가?" + player.Value.GetComponent<NavMeshAgent>());

    //                //agent.Warp(targetPortalLocation.position);
    //                agent.transform.GetComponent<PlayerController>().WarpTo(targetPortalLocation.position);
    //            }
    //        }
    //        portal.SetActive(false);
    //        MonsterManager.Instance.portalTrigger = this;
    //        MonsterManager.Instance.SpawnMonstersForMap(targetMapName);
    //        Debug.Log("몬스터 생성 완료");
    //        MonsterManager.Instance.RestartCheckMonstersCoroutine(targetMapName);
    //    }
    //}

    public void Clear()
    {
        playersInPortal.Clear();
    }
}
