using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �÷��̾ Stage 1 ���� �濡 �����ϸ� �������� �ؽ�Ʈ�� �˸°� ������Ʈ�ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class Stage1BossEntrance : MonoBehaviour
{
    // ���� �г�
    [Header("[ ���� �г� ]")]
    public TextMeshProUGUI stageText; // �������� �̸� �ؽ�Ʈ

    // �÷��̾ ķ���� ������ �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stageText.text = "Stage 1 - ���� ����"; // �������� �ؽ�Ʈ�� ķ���� �°� ������Ʈ
        }
    }
}