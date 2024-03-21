using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IPlayerInteractable
{
    
    [SerializeField]
    public Define.NPCType _npcType;
    // Enum으로 관리할지 상속 받아 쓸지 고민
    
    public virtual void OnInteracted(GameObject player)
    {
        Debug.Log("NPC");
        // 플레이어가 E키를 눌렀을 때 할 행동 또는 UI
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        // 플레이어가 다가왔을 때 할 행동 또는 UI
    }
}
