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
    public TextMeshProUGUI stageText; // �������� �ؽ�Ʈ

    // �������� �г�
    [Header("[ �������� �г� ]")]
    public GameObject stagePanel; // �������� �г�
    public TextMeshProUGUI stageLevelText; // �������� ���� �ؽ�Ʈ
    public TextMeshProUGUI stageNameText; // �������� �̸� �ؽ�Ʈ

    // ���� �޴�
    [Header("[ ���� �޴� ]")]
    public GameObject campGameMenu; // ķ�� ���� �޴�
    public GameObject dungeonGameMenu; // ���� ���� �޴�

    // ���� ����
    [Header("[ ���� ���� ]")]
    public GameObject drillDuckStatus; // �帱�� ���� �г�
    public GameObject crocodileStatus; // ũ��Ŀ���� ���� �г�

    // �÷��̾ ķ���� ������ �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        timeContainerPanel.SetActive(false); // ���� �ð� UI ��Ȱ��ȭ
        stageText.text = "���谡�� ķ��"; // �������� �ؽ�Ʈ�� ķ���� �°� ������Ʈ

        stageLevelText.text = "Camp"; // �������� ���� �ؽ�Ʈ�� ������Ʈ
        stageNameText.text = "���谡�� ķ��"; // �������� �̸� �ؽ�Ʈ�� ������Ʈ

        campGameMenu.SetActive(true); // ķ�� ���� �޴� Ȱ��ȭ
        dungeonGameMenu.SetActive(false); // ���� ���� �޴� ��Ȱ��ȭ

        drillDuckStatus.SetActive(false); // �帱�� ���� �г� ��Ȱ��ȭ
        crocodileStatus.SetActive(false); // ũ��Ŀ���� ���� �г� ��Ȱ��ȭ

        StartCoroutine(ShowStagePanel());
    }

    // 5�ʰ� �������� �г��� Ȱ��ȭ�ϰ�, �ٽ� ��Ȱ��ȭ �ϴ� �ڷ�ƾ
    IEnumerator ShowStagePanel()
    {
        stagePanel.SetActive(true);
        yield return new WaitForSeconds(3);
        stagePanel.SetActive(false);
    }
}