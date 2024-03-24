using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �÷��̾ ������ �����ϸ� Ư�� UI���� Ȱ��ȭ�ϰ�,
/// �������� �ؽ�Ʈ�� �˸°� ������Ʈ�ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class DungeonEntrance : MonoBehaviour
{
    // ���� ���� �г�
    [Header("[ ���� ���� �г� ]")]
    public GameObject timeContainerPanel; // ���� �ð� �г�
    public GameObject goldPanel; // ��� �г�

    // ���� �г�
    [Header("[ ���� �г� ]")]
    public TextMeshProUGUI stageText; // �������� �̸� �ؽ�Ʈ

    // �÷��̾ ķ���� ������ �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeContainerPanel.SetActive(true); // ���� �ð� UI Ȱ��ȭ
            goldPanel.SetActive(true); // ��� UI Ȱ��ȭ
            stageText.text = "Stage 1 - ���� ���� �ӻ���"; // �������� �ؽ�Ʈ�� ķ���� �°� ������Ʈ
        }
    }
}