using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ������ ����Ǹ� �˾��Ǵ� ���â�� �����ϴ� ������Ʈ�Դϴ�.
/// </summary>
public class AdventureResultWindow : MonoBehaviour
{
    // ------------------------------------------------ ���� ���� ------------------------------------------------

    // ������ ��ų ��Ÿ�� UI �Ŵ����� ����� ���� ����
    private PlayerController _playerController; // �÷��̾� ��Ʈ�ѷ� ���� ����
    private Item[] _playerInventory; // �÷��̾��� �κ��丮 �迭

    // ������
    [Header("[ ������ ]")]
    public Image firstItemIcon; // ������ 1 ������
    public Image secondItemIcon; // ������ 2 ������

    // ���� �ð�
    [Header("[ ���� �ð� ]")]
    public TextMeshProUGUI gameTimeText; // ���� �ð� �ؽ�Ʈ


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void OnEnable()
    {
        float gameTime = DungeonEntrance.Instance.GameTime;
        int minutes = Mathf.FloorToInt(gameTime / 60);
        int seconds = Mathf.FloorToInt(gameTime % 60);

        gameTimeText.text = string.Format("{0:0}�� {1:00}��", minutes, seconds);
    }

    void Update()
    {
        UpdateInventory();
    }

    // ------------------------------------------------ ����� ���� �޼��� ------------------------------------------------

    // �κ��丮�� ������Ʈ�ϴ� �޼���
    void UpdateInventory()
    {
        // PlayerController ������Ʈ�� ã�Ƽ� ����
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController ������Ʈ�� ã�� �� ���� ��

        // PlayerController�� �κ��丮�� ����
        _playerInventory = _playerController.Inventory;

        // ������ ���� ������Ʈ
        UpdateItemIcon(firstItemIcon, _playerInventory[1]);
        UpdateItemIcon(secondItemIcon, _playerInventory[2]);
    }

    // ������ ������ ������Ʈ �޼���
    void UpdateItemIcon(Image itemIcon, Item item)
    {
        // ������ ������ ������Ʈ
        itemIcon.sprite = item.Icon;
    }

}