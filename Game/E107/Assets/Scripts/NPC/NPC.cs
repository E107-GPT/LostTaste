using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IPlayerInteractable
{
    public DialogueUI dialogueUI;
    public string dialogueNpcName;
    public string[] dialogueTexts;

    [SerializeField]
    public Define.NPCType _npcType;
    // Enum으로 관리할지 상속 받아 쓸지 고민

    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool playerInRange = false;

    public virtual void OnInteracted(GameObject player)
    {
        if (currentDialogueIndex < dialogueTexts.Length)
        {
            dialogueUI.ShowDialogue(dialogueNpcName, dialogueTexts[currentDialogueIndex]);
            currentDialogueIndex++; // 다음 대사로 인덱스 업데이트
        }
        else
        {
            // 모든 대사가 표시된 후 대화창을 숨김
            dialogueUI.HideDialogue();
            currentDialogueIndex = 0; // 인덱스 초기화
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OnInteracted(GameObject.FindWithTag("Player"));
        }
    }
}
