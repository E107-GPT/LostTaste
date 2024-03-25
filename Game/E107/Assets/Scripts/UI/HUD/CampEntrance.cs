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
    // ���� �ð�
    [Header("[ ���� �ð� ]")]
    public GameObject timeContainerPanel; // ���� �ð� �г�

    // ���� �г�
    [Header("[ ���� �г� ]")]
    public TextMeshProUGUI stageText; // �������� �̸� �ؽ�Ʈ

    // ���� �޴�
    [Header("[ ���� �޴� ]")]
    public GameObject campGameMenu; // ķ�� ���� �޴�
    public GameObject dungeonGameMenu; // ���� ���� �޴�

    // �÷��̾ ķ���� ������ �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeContainerPanel.SetActive(false); // ���� �ð� UI ��Ȱ��ȭ
            stageText.text = "���谡�� ķ��"; // �������� �ؽ�Ʈ�� ķ���� �°� ������Ʈ

            campGameMenu.SetActive(true); // ķ�� ���� �޴� Ȱ��ȭ
            dungeonGameMenu.SetActive(false); // ���� ���� �޴� ��Ȱ��ȭ
        }
    }
}