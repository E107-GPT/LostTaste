using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IPlayerInteractable
{
    
    [SerializeField]
    public Define.NPCType _npcType;
    // Enum���� �������� ��� �޾� ���� ���
    
    public virtual void OnInteracted(GameObject player)
    {
        Debug.Log("NPC");
        // �÷��̾ EŰ�� ������ �� �� �ൿ �Ǵ� UI
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        // �÷��̾ �ٰ����� �� �� �ൿ �Ǵ� UI
    }
}
