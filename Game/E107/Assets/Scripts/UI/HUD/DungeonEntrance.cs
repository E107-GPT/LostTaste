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
    private float gameTime = 0f;
    private bool isInCamp = true;

    // ���� �ð�
    [Header("[ ���� �ð� ]")]
    public GameObject timeContainerPanel; // ���� �ð� �г�
    public TextMeshProUGUI gameTimeText; // ���� �ð� �ؽ�Ʈ

    // ���� �г�
    [Header("[ ���� �г� ]")]
    public TextMeshProUGUI stageText; // �������� �̸� �ؽ�Ʈ

    // ���� �޴�
    [Header("[ ���� �޴� ]")]
    public GameObject campGameMenu; // ķ�� ���� �޴�
    public GameObject dungeonGameMenu; // ���� ���� �޴�

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
        if (other.CompareTag("Player"))
        {

            isInCamp = false;
            timeContainerPanel.SetActive(true); // ���� �ð� UI Ȱ��ȭ
            stageText.text = "Stage 1 - ���� ��"; // �������� �ؽ�Ʈ�� ķ���� �°� ������Ʈ

            campGameMenu.SetActive(false); // ķ�� ���� �޴� ��Ȱ��ȭ
            dungeonGameMenu.SetActive(true); // ���� ���� �޴� Ȱ��ȭ
        }
    }
}