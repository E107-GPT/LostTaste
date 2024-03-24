using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �÷��̾ ķ���� �����ϸ� Ư�� UI���� ��Ȱ��ȭ�ϰ�,
/// �������� �ؽ�Ʈ�� �˸°� ������Ʈ�ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class CampEntrance : MonoBehaviour
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
            timeContainerPanel.SetActive(false); // ���� �ð� UI ��Ȱ��ȭ
            goldPanel.SetActive(false); // ��� UI ��Ȱ��ȭ
            stageText.text = "���谡�� ķ��"; // �������� �ؽ�Ʈ�� ķ���� �°� ������Ʈ
        }
    }
}