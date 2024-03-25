using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �÷��̾ Stage 2�� �����ϸ� �������� �ؽ�Ʈ�� �˸°� ������Ʈ�ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class Stage2Entrance : MonoBehaviour
{
    // ���� �г�
    [Header("[ ���� �г� ]")]
    public TextMeshProUGUI stageText; // �������� �̸� �ؽ�Ʈ

    // Ŭ���� �� ��������
    [Header("[ Ŭ���� �� �������� ]")]
    public GameObject stageXIcon; // Ŭ���� �� �������� ���� ������
    public GameObject stage1Icon; // Stage 1 Ŭ���� ������
    public TextMeshProUGUI stageClearText; // �������� Ŭ���� �ؽ�Ʈ

    // �÷��̾ ķ���� ������ �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stageText.text = "Stage 2 - ������ �غ�"; // �������� �ؽ�Ʈ�� ķ���� �°� ������Ʈ
            stageXIcon.SetActive(false); // Ŭ���� �� �������� ���� ������ ��Ȱ��ȭ
            stage1Icon.SetActive(true); // Stage 1 Ŭ���� ������ Ȱ��ȭ
            stageClearText.text = "Ŭ������ ���������Դϴ�.";
        }
    }
}