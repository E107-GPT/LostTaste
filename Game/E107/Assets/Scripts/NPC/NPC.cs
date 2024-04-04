using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IPlayerInteractable
{
    public DialogueUI dialogueUI;
    public ManualBoardUI boardUI;
    public string dialogueNpcName;
    public string[] dialogueTexts;
    public GameObject npcNamePanel;

    // ManualBoard
    public Button closeButton;

    // 대화 시 비활성화 할 UI 
    public GameObject HUD;
    public GameObject ChatWindow;
    public GameObject ChatBackground;

    [SerializeField]
    private ManualBoardUI.ManualType manualType;
    [SerializeField]
    public Define.NPCType _npcType; // normal, manual
    

    private int currentDialogueIndex = 0; // 현재 대사 인덱스
    private bool playerInRange = false;
    private bool isInteracting = false;

    private void Start()
    {
        npcNamePanel.SetActive(false);

        if (closeButton != null)
            closeButton.onClick.AddListener(CloseManual);
    }

    public virtual void OnInteracted(GameObject player)
    {
        // 상호작용이 시작되었고, NPC 타입이 Normal일 경우
        if (isInteracting && _npcType == Define.NPCType.Normal)
        {
            // 다음 대사가 존재할 경우
            if (currentDialogueIndex < dialogueTexts.Length)
            {
                
                dialogueUI.ShowDialogue(dialogueNpcName, dialogueTexts[currentDialogueIndex]);
                currentDialogueIndex++;
            }
            else
            {
                // 모든 대사가 표시된 후 대화창 숨김 처리
                FinishInteraction();
            }
        }
        else if (isInteracting && _npcType == Define.NPCType.Manual)
        {
            // 조작법 UI가 표시된 상태에서 다시 E키를 눌렀을 경우, UI를 숨김
            boardUI.HideManual();
            isInteracting = false;
        }
        else
        {
            // 상호작용 시작
            isInteracting = true;

            if (_npcType == Define.NPCType.Normal)
            {
                // 대화 시작 시 UI 비활성화
                HUD.SetActive(false);
                ChatWindow.SetActive(false);
                ChatBackground.SetActive(false);

                // 다음 대사 표시
                // 첫 번째 대사 표시 (상호작용이 처음 시작될 때)
                if (currentDialogueIndex < dialogueTexts.Length)
                {
                    dialogueUI.ShowDialogue(dialogueNpcName, dialogueTexts[currentDialogueIndex]);
                    currentDialogueIndex++;
                }
            }
            else if (_npcType == Define.NPCType.Manual)
            {
                if (boardUI.IsShown)
                {
                    boardUI.HideManual();
                }
                else
                {
                    boardUI.ShowManual(manualType);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.name == "Player")
        {
            playerInRange = true;
            ShowNPCName(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && other.name == "Player")
        {
            playerInRange = false;
            ShowNPCName(false);

            // 조작법 UI 숨기기 추가
            if (_npcType == Define.NPCType.Manual && isInteracting)
            {
                boardUI.HideManual();
            }

            // 대화 중에 플레이어가 범위를 벗어나면 대화창 숨김
            if (isInteracting)
            {
                FinishInteraction();
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

    void FinishInteraction()
    {
        // 대화 종료 후 UI 활성화
        HUD.SetActive(true);
        ChatWindow.SetActive(true);

        if (_npcType == Define.NPCType.Normal)
        {
            dialogueUI.HideDialogue();
        }
        else if (_npcType == Define.NPCType.Manual)
        {
            boardUI.HideManual();
        }

        currentDialogueIndex = 0;
        isInteracting = false;
    }

    void ShowNPCName(bool show)
    {
        if (npcNamePanel != null)
        {
            TextMeshProUGUI npcNameText = npcNamePanel.GetComponentInChildren<TextMeshProUGUI>();

            if (npcNameText != null)
            {
                npcNameText.text = dialogueNpcName;
            }

            npcNamePanel.SetActive(show);
        }
    }

    void CloseManual()
    {
        boardUI.HideManual();
        isInteracting = false;
    }
}
