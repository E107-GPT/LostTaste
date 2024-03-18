using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 씬 전환 포탈 또는 게임 종료시 작동

public class SceneChange : MonoBehaviour
{
    // 포탈 타입은 스테이지 입장과 게임 종료 두 가지(게임 종료 시스템이 없기 때문에 임시로 제작)
    public enum PortalType { Entrance, Exit }
    public PortalType portalType;

    private HashSet<GameObject> playersInPortal = new HashSet<GameObject>();

    // 필요한 플레이어 수, 게임 설정에 따라 조정 (지금은 1명)
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
