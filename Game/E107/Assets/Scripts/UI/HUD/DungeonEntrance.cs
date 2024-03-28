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
    public static DungeonEntrance Instance { get; private set; }

    private float gameTime = 0f;
    public float GameTime => gameTime; // �ܺο��� ���� �����ϵ��� ���� �߰�

    private bool isInCamp = true;

    // ���� �ð�
    [Header("[ ���� �ð� ]")]
    public GameObject timeContainerPanel; // ���� �ð� �г�
    public TextMeshProUGUI gameTimeText; // ���� �ð� �ؽ�Ʈ

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

    // Ŭ���� �� ��������
    [Header("[ Ŭ���� �� �������� ]")]
    public GameObject stageXIcon; // Ŭ���� �� �������� ���� ������
    public GameObject stage1Icon; // Stage 1 Ŭ���� ������
    public GameObject stage2Icon; // Stage 2 Ŭ���� ������
    public GameObject stage3Icon; // Stage 3 Ŭ���� ������
    public GameObject finalStageIcon; // Final Stage Ŭ���� ������
    public TextMeshProUGUI stageClearText; // �������� Ŭ���� �ؽ�Ʈ

    private bool hasEntered = false; // �÷��̾ �̹� �����ߴ��� ���θ� �����ϴ� ����

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ������
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (isInCamp)
        {
            // ķ���� �ִ� ���� �ð� �ʱ�ȭ
            gameTime = 0f;
        }
        else
        {
            // ������ �ִ� ���� �ð� �帧
            gameTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(gameTime / 60);
            int seconds = Mathf.FloorToInt(gameTime % 60);
            gameTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    // �÷��̾ ķ���� ������ �� ȣ��Ǵ� �޼���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasEntered)
        {

            isInCamp = false;
            timeContainerPanel.SetActive(true); // ���� �ð� UI Ȱ��ȭ
            stageText.text = "STAGE 1 - ���� ��"; // �������� �ؽ�Ʈ ������Ʈ

            stageLevelText.text = "STAGE 1"; // �������� ���� �ؽ�Ʈ�� ������Ʈ
            stageNameText.text = "���� ��"; // �������� �̸� �ؽ�Ʈ�� ������Ʈ

            campGameMenu.SetActive(false); // ķ�� ���� �޴� ��Ȱ��ȭ
            dungeonGameMenu.SetActive(true); // ���� ���� �޴� Ȱ��ȭ

            stageXIcon.SetActive(true); // Ŭ���� �� �������� ���� ������ Ȱ��ȭ
            stage1Icon.SetActive(false); // Stage 1 Ŭ���� ������ ��Ȱ��ȭ
            stage2Icon.SetActive(false); // Stage 2 Ŭ���� ������ ��Ȱ��ȭ
            stage3Icon.SetActive(false); // Stage 3 Ŭ���� ������ ��Ȱ��ȭ
            finalStageIcon.SetActive(false); // Final Stage Ŭ���� ������ ��Ȱ��ȭ
            stageClearText.text = "Ŭ������ ���������� �����ϴ�.";

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