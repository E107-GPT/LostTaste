using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour, IPlayerInteractable
{
    public DialogueUI dialogueUI;
    public string dialogueNpcName;
    public string[] dialogueTexts;
    public GameObject npcNamePanel;

    [SerializeField]
    public Define.NPCType _npcType;
    // Enum으로 관리할지 상속 받아 쓸지 고민

    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool playerInRange = false;
    private bool isInteracting = false;

    private void Start()
    {
        npcNamePanel.SetActive(false);
    }

    public virtual void OnInteracted(GameObject player)
    {
        isInteracting = true;

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
            ShowNPCName(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            ShowNPCName(false);
            if (isInteracting) // 대화 중에 플레이어가 범위를 벗어나면 대화창 숨김
            {
                dialogueUI.HideDialogue();
                currentDialogueIndex = 0; // 대화 인덱스 초기화
                isInteracting = false; // 대화가 끝났으므로 isInteracting을 false로 설정
            }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OnInteracted(GameObject.FindWithTag("Player"));
        }
    }

    //void ShowNPCName(bool show)
    //{
    //    npcNameUI.text = dialogueNpcName; // NPC 이름 설정
    //    //npcNameUI.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + Vector3.up * 2); // NPC 위에 UI 위치 조정
    //    npcNameUI.gameObject.SetActive(show); // UI 활성화/비활성화
    //}
    void ShowNPCName(bool show)
    {
        if (npcNamePanel != null)
        {
            // NPC 이름 텍스트를 찾습니다.
            TextMeshProUGUI npcNameText = npcNamePanel.GetComponentInChildren<TextMeshProUGUI>();

            if (npcNameText != null)
            {
                // NPC 이름을 설정합니다.
                npcNameText.text = dialogueNpcName;
            }

            // 패널을 활성화 또는 비활성화합니다.
            npcNamePanel.SetActive(show);
        }
    }
}
