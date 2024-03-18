using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ��ȯ ��Ż �Ǵ� ���� ����� �۵�

public class SceneChange : MonoBehaviour
{
    // ��Ż Ÿ���� �������� ����� ���� ���� �� ����(���� ���� �ý����� ���� ������ �ӽ÷� ����)
    public enum PortalType { Entrance, Exit }
    public PortalType portalType;

    private HashSet<GameObject> playersInPortal = new HashSet<GameObject>();

    // �ʿ��� �÷��̾� ��, ���� ������ ���� ���� (������ 1��)
    public int totalPlayers = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInPortal.Add(other.gameObject);
            CheckAllPlayersInPortal();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playersInPortal.Remove(other.gameObject);
        }
    }

    private void CheckAllPlayersInPortal()
    {
        if (playersInPortal.Count == totalPlayers)
        {
            switch (portalType)
            {
                case PortalType.Entrance:
                    GameScene.EnterDungeon();
                    break;
                case PortalType.Exit:
                    GameScene.ExitDungeon();
                    break;
            }
        }
    }
}
