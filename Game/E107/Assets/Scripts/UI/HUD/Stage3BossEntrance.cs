using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �÷��̾ Stage 3 ���� �濡 �����ϸ� �������� �ؽ�Ʈ�� �˸°� ������Ʈ�ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class Stage3BossEntrance : MonoBehaviour
{
    // ���� �г�
    [Header("[ ���� �г� ]")]
    public TextMeshProUGUI stageText; // �������� �̸� �ؽ�Ʈ

    // �������� �г�
    [Header("[ �������� �г� ]")]
    public GameObject stagePanel; // �������� �г�
    public TextMeshProUGUI stageLevelText; // �������� ���� �ؽ�Ʈ
    public TextMeshProUGUI stageNameText; // �������� �̸� �ؽ�Ʈ

    // �÷��̾ ķ���� ������ �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stageText.text = "STAGE 3 - ���� ������ ����"; // �������� �ؽ�Ʈ ������Ʈ

            stageLevelText.text = "STAGE 3 BOSS"; // �������� ���� �ؽ�Ʈ�� ������Ʈ
            stageNameText.text = "���� ������ ����"; // �������� �̸� �ؽ�Ʈ�� ������Ʈ

            StartCoroutine(ShowStagePanel());
        }
    }

    // 5�ʰ� �������� �г��� Ȱ��ȭ�ϰ�, �ٽ� ��Ȱ��ȭ �ϴ� �ڷ�ƾ
    IEnumerator ShowStagePanel()
    {
        stagePanel.SetActive(true);
        yield return new WaitForSeconds(5);
        stagePanel.SetActive(false);
    }
}