using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �÷��̾ Stage 3�� �����ϸ� �������� �ؽ�Ʈ�� �˸°� ������Ʈ�ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class Stage3Entrance : MonoBehaviour
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
    public GameObject stage2Icon; // Stage 2 Ŭ���� ������

    // ũ��Ŀ���� ü�� �����̴�
    [Header("[ ũ��Ŀ���� ü�� �����̴� ]")]
    public GameObject crocodileHealthBar;

    private bool hasEntered = false; // �÷��̾ �̹� �����ߴ��� ���θ� �����ϴ� ����

    // �÷��̾ ķ���� ������ �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {
            stageText.text = "STAGE 3 - ������ ����"; // �������� �ؽ�Ʈ ������Ʈ
            stageLevelText.text = "STAGE 3"; // �������� ���� �ؽ�Ʈ�� ������Ʈ
            stageNameText.text = "������ ����"; // �������� �̸� �ؽ�Ʈ�� ������Ʈ

            stage2Icon.SetActive(true); // Stage 2 Ŭ���� ������ Ȱ��ȭ

            crocodileHealthBar.SetActive(false); // ���� �������� ���� ü�� �� ��Ȱ��ȭ

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