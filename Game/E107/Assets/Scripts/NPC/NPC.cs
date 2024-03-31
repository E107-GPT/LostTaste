using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour, IPlayerInteractable
{
    public DialogueUI dialogueUI;
    public ManualBoardUI boardUI;
    public string dialogueNpcName;
    public string[] dialogueTexts;
    public GameObject npcNamePanel;

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
    }

    //public virtual void OnInteracted(GameObject player)
    //{
    //    isInteracting = true;

    //    switch (_npcType)
    //    {
    //        case Define.NPCType.Normal:
    //            if (currentDialogueIndex < dialogueTexts.Length)
    //            {
    //                dialogueUI.ShowDialogue(dialogueNpcName, dialogueTexts[currentDialogueIndex]);
    //                currentDialogueIndex++; // 다음 대사로 인덱스 업데이트
    //            }
    //            else
    //            {
    //                // 모든 대사가 표시된 후 대화창을 숨김
    //                dialogueUI.HideDialogue();
    //                currentDialogueIndex = 0; // 인덱스 초기화
    //            }
    //            break;

    //        case Define.NPCType.Manual:
    //            if (boardUI.IsShown)
    //            {
    //                // 이미 조작법 UI가 활성화되어 있으면, UI를 숨깁니다.
    //                boardUI.HideManual();
    //            }
    //            else
    //            {
    //                // 조작법 UI 표시
    //                boardUI.ShowManual(ManualBoardUI.ManualType.Basic);
    //            }
    //            break;
    //    }
    //}
    public virtual void OnInteracted(GameObject player)
    {
        // 상호작용이 시작되었고, NPC 타입이 Normal일 경우
        if (isInteracting && _npcType == Define.NPCType.Normal)
        {
            // 다음 대사가 존재할 경우
            if (currentDialogueIndex < dialogueTexts.Length)
            {
                // 다음 대사 표시
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
                // 첫 번째 대사 표시 (상호작용이 처음 시작될 때)
                if (currentDialogueIndex < dialogueTexts.Length)
                {
                    dialogueUI.ShowDialogue(dialogueNpcName, dialogueTexts[currentDialogueIndex]);
                    currentDialogueIndex++;
                }
            }
            else if (_npcType == Define.NPCType.Manual)
            {
                // 조작법 UI 처리
                if (boardUI.IsShown)
                {
                    // 이미 표시된 조작법 UI 숨김 처리
                    boardUI.HideManual();
                }
                else
                {
                    // 조작법 UI 표시
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

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        playerInRange = false;
    //        ShowNPCName(false);
    //        if (isInteracting) // 대화 중에 플레이어가 범위를 벗어나면 대화창 숨김
    //        {
    //            dialogueUI.HideDialogue();
    //            boardUI.HideManual();
    //            currentDialogueIndex = 0; // 대화 인덱스 초기화
    //            isInteracting = false; // 대화가 끝났으므로 isInteracting을 false로 설정
    //        }
    //    }
    //}
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
        // 현재 상호작용 중인 상태를 종료하고, 필요한 UI를 숨깁니다.
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
