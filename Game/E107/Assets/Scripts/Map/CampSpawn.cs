using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CampSpawn : MonoBehaviour
{
    Vector3 entrancePosition = new Vector3(0, 0, 0);

    private void Awake()
    {
        // ���� �ε�� ������ OnSceneLoaded �޼��带 ȣ���ϵ��� �̺�Ʈ�� ���
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // ������Ʈ�� �ı��� �� �̺�Ʈ ��� ����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // ���� �ε�� �� ����� �޼���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Camp")
        {
            MovePlayerToEntrance();
        }
    }

    // �÷��̾ �Ա� ��ġ�� �̵���Ű�� �޼���
    void MovePlayerToEntrance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // �÷��̾� ������Ʈ ã��
        NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
        if (player != null)
        {
            agent.Warp(entrancePosition); // �÷��̾� ��ġ ����
        }
    }
}
