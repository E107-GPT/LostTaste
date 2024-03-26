using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI; // NavMeshAgent ����� ���� �ʿ�
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MovePlayerToCamp : MonoBehaviour
{
    // ķ�� ��ġ
    [Header("[ ķ�� ��ġ ]")]
    public Transform campLocation; // ķ�� ��ġ�� �����ϴ� ����

    // Ȯ�� ��ư
    [Header("[ Ȯ�� ��ư ]")]
    public Button confirmButton;

    // ��ũ��Ʈ�� Ȱ��ȭ�Ǿ��� �� ȣ��Ǵ� �޼���
    private void Awake()
    {
        // ��ư�� Ŭ�� �̺�Ʈ�� �߰�
        if (confirmButton != null)
            this.confirmButton.onClick.AddListener(LoadCampScene);
    }

    // Camp Scene�� �ε��ϴ� �޼���
    public void LoadCampScene()
    {
        // "Dungeon" ���� LoadSceneMode.Single ���� �ε��մϴ�.
        //SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
        Managers.Scene.LoadScene(Define.Scene.Dungeon, true);
        
    }

    // 'Ȯ��' ��ư Ŭ�� �� ȣ��� �޼���
    //public void OnConfirmButtonClicked()
    //{
    //    GameObject player = GameObject.FindGameObjectWithTag("Player"); // �÷��̾� �±׸� ����Ͽ� �÷��̾� ��ü ã��
    //    if (player != null)
    //    {
    //        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
    //        if (agent != null)
    //        {
    //            // NavMeshAgent�� �ִ� ���, Warp �޼��带 ����Ͽ� ķ�� ��ġ�� �̵�
    //            agent.Warp(campLocation.position);
    //        }
    //        else
    //        {
    //            // NavMeshAgent�� ���� ���, ���� ��ġ�� ����
    //            player.transform.position = campLocation.position;
    //        }
    //    }
    //}
}


