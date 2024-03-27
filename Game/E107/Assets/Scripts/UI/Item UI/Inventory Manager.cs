using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// �κ��丮 �Ŵ����� �÷��̾��� �κ��丮�� �����ϰ� �� ������ Ȯ���ϴ� ����� �����մϴ�.
/// �÷��̾� ��Ʈ�ѷ��� ã�Ƽ� �κ��丮�� �����ϸ�, ù ��°�� �� ��° �������� ������ Ȱ���մϴ�.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    // ------------------------------------------------ ���� ���� ------------------------------------------------

    // �κ��丮 �Ŵ����� ����� ���� ����
    private PlayerController _playerController; // �÷��̾� ��Ʈ�ѷ� ���� ����
    private Item[] _playerInventory; // �÷��̾��� �κ��丮 �迭
    private int _currentItemNum; // ���� ������ ����

    // ������ 1
    [Header("[ ������ 1 ]")]
    public Image firstItemIcon; // ������ 1 ������
    public GameObject firstItemUsed; // ������ 1 ��� ����
    public TextMeshProUGUI firstItemName; // ������ 1 �̸�
    public TextMeshProUGUI firstItemRightSkillMana; // ������ 1 ������ ��ų ����
    public TextMeshProUGUI firstItemRightSkillCoolDown; // ������ 1 ������ ��ų ��Ÿ��

    // ������ 2
    [Header("[ ������ 2 ]")]
    public Image secondItemIcon; // ������ 2 ������
    public GameObject secondItemUsed; // ������ 2 ��� ����
    public TextMeshProUGUI secondItemName; // ������ 2 �̸�
    public TextMeshProUGUI secondItemRightSkillMana; // ������ 2 ������ ��ų ����
    public TextMeshProUGUI secondItemRightSkillCoolDown; // ������ 2 ������ ��ų ��Ÿ��


    // ------------------------------------------------ Life Cylce ------------------------------------------------

    void Update()
    {
        // �κ��丮 ������Ʈ
        UpdateInventory();

        // ������� ������ ���� ǥ��
        ItemChange();
    }


    // ------------------------------------------------ ����� ���� �޼��� ------------------------------------------------

    // �κ��丮�� ������Ʈ�ϴ� �޼���
    void UpdateInventory()
    {
        // PlayerController ������Ʈ�� ã�Ƽ� ����
        _playerController = GameObject.FindObjectOfType<PlayerController>();

        if (_playerController == null) return; // PlayerController ������Ʈ�� ã�� �� ���� ��

        // PlayerController�� �κ��丮�� ����
        _playerInventory = _playerController.Inventory;
        _currentItemNum = _playerController.CurrentItemNum;

        // ������ ���� ������Ʈ
        UpdateItemUI(firstItemIcon, firstItemName, firstItemRightSkillMana, firstItemRightSkillCoolDown, _playerInventory[1]);
        UpdateItemUI(secondItemIcon, secondItemName, secondItemRightSkillMana, secondItemRightSkillCoolDown, _playerInventory[2]);
    }

    // ������ UI ������Ʈ �޼���
    void UpdateItemUI(Image itemIcon, TextMeshProUGUI itemName, TextMeshProUGUI skillMana, TextMeshProUGUI skillCoolDown, Item item)
    {
        // ������ ������ �� �̸� ������Ʈ
        itemIcon.sprite = item.Icon;
        itemName.text = $"<color={GetTierColor(item.Tier)}>{item.Name}</color>"; // ������ �̸��� ���� ����

        // ��ų ���� ������Ʈ
        if (float.IsInfinity(item.RightSkill.SkillCoolDownTime))
        {
            skillMana.text = "-";
            skillCoolDown.text = "-";
        }
        else
        {
            skillMana.text = item.RightSkill.RequiredMp.ToString();
            skillCoolDown.text = $"{item.RightSkill.SkillCoolDownTime}s";
        }
    }

    // Ƽ� ���� ���� ��ȯ�ϴ� �޼���
    string GetTierColor(ItemTier tier)
    {
        switch (tier)
        {
            case ItemTier.COMMON:
                return "#BFBFBF";
            case ItemTier.UNCOMMON:
                return "#1AAC9C";
            case ItemTier.RARE:
                return "#3498DB";
            case ItemTier.EPIC:
                return "#9B59B6";
            case ItemTier.LEGENDARY:
                return "#F1C40F";
            case ItemTier.BOSS:
                return "#E74C3C";
            default:
                return "#FFFFFF"; // �⺻ ������ ��� ��ȯ
        }
    }

    // ��� ���� �����ۿ� ����ǥ�ø� �ϴ� �޼���
    void ItemChange()
    {
        // ���� ��ü
        if (_currentItemNum == 1)
        {
            firstItemUsed.SetActive(true);
            secondItemUsed.SetActive(false);
        }
        else if (_currentItemNum == 2)
        {
            firstItemUsed.SetActive(false);
            secondItemUsed.SetActive(true);
        }
    }
}