using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel; // 대사창 패널
    public TextMeshProUGUI npcNameText; // Npc 이름 텍스트
    public TextMeshProUGUI dialogueText; // 대사 텍스트


    // 대사창을 표시하는 함수
    public void ShowDialogue(string name, string dialogues)
    {
        npcNameText.text = name; // NPC 이름 설정
        dialogueText.text = dialogues; // 첫 번째 대화 텍스트 설정

        dialoguePanel.SetActive(true); // 대사창 활성화
    }

    // 다음 대사 함수
    public void ShowNextDialogue(string[] dialogues, int currentIndex)
    {
        if (currentIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[currentIndex]; // 현재 인덱스의 대화 텍스트 설정
        }
        else
        {
            // 대화가 끝났을 경우 대화창을 숨김
            HideDialogue();
        }
    }

    // 대사창을 숨기는 함수
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false); // 대사창 비활성화
    }
}
