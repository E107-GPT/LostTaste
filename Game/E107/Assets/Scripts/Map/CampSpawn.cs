using System.Collections;
using System.Collections.Generic;
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

    // ���� �ε�� �� �÷��̾� �̵� �� ���� �ʱ�ȭ
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Camp")
        {
            //ResetPlayerHP();
            MovePlayerToEntrance();
        }
    }

    // �÷��̾ �Ա� ��ġ�� �̵���Ű�� �Լ�
    void MovePlayerToEntrance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // �÷��̾� ������Ʈ ã��
        if (player != null)
        {
            // �÷��̾� ��ġ ����
            NavMeshAgent agent = player.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.Warp(entrancePosition);
            }
        }
    }
}
