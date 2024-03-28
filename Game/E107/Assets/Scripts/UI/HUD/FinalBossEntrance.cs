using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �÷��̾ ���� ���� �濡 �����ϸ� �������� �ؽ�Ʈ�� �˸°� ������Ʈ�ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class FinalBossEntrance : MonoBehaviour
{
    // ���� �г�
    [Header("[ ���� �г� ]")]
    public TextMeshProUGUI stageText; // �������� �̸� �ؽ�Ʈ

    // �������� �г�
    [Header("[ �������� �г� ]")]
    public GameObject stagePanel; // �������� �г�
    public TextMeshProUGUI stageLevelText; // �������� ���� �ؽ�Ʈ
    public TextMeshProUGUI stageNameText; // �������� �̸� �ؽ�Ʈ

    // Ŭ���� �� ��������
    [Header("[ Ŭ���� �� �������� ]")]
    public GameObject stage3Icon; // Stage 3 Ŭ���� ������

    private bool hasEntered = false; // �÷��̾ �̹� �����ߴ��� ���θ� �����ϴ� ����

    // �÷��̾ ķ���� ������ �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {
            stageText.text = "FINAL STAGE - ������ ����"; // �������� �ؽ�Ʈ ������Ʈ

            stageLevelText.text = "FINAL STAGE"; // �������� ���� �ؽ�Ʈ�� ������Ʈ
            stageNameText.text = "������ ����"; // �������� �̸� �ؽ�Ʈ�� ������Ʈ

            stage3Icon.SetActive(true); // Stage 3 Ŭ���� ������ Ȱ��ȭ

            StartCoroutine(ShowStagePanel());

            hasEntered = true; // �÷��̾ ���������� ǥ��
        }
    }

    // 5�ʰ� �������� �г��� Ȱ��ȭ�ϰ�, �ٽ� ��Ȱ��ȭ �ϴ� �ڷ�ƾ
    IEnumerator ShowStagePanel()
    {
        stagePanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        stagePanel.SetActive(false);
    }
}