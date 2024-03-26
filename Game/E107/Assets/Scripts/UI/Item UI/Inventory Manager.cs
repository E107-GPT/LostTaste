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
        // PlayerController ������Ʈ�� ã�Ƽ� �����մϴ�.
        _playerController = GameObject.FindObjectOfType<PlayerController>();

        if (_playerController != null)
        {
            // PlayerController�� �κ��丮�� �����մϴ�.
            _playerInventory = _playerController.Inventory;
            _currentItemNum = _playerController.CurrentItemNum;
        }
        else
        {
            Debug.LogError("PlayerController ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        Item firstItem = _playerInventory[1];
        Item secondItem = _playerInventory[2];

        // ������ 1 ������Ʈ
        firstItemIcon.sprite = firstItem.Icon;
        firstItemName.text = $"<color={GetTierColor(firstItem.Tier)}>{firstItem.Name}</color>"; // ������ �̸��� ���� ����
        
        // ��ų�� ���� ��� �ؽ�Ʈ�� -�� ǥ��
        if (firstItem.RightSkill.SkillCoolDownTime.ToString() == "Infinity")
        {
            firstItemRightSkillMana.text = "-";
            firstItemRightSkillCoolDown.text = "-";
        }
        else
        {
            firstItemRightSkillMana.text = firstItem.RightSkill.RequiredMp.ToString();
            firstItemRightSkillCoolDown.text = $"{firstItem.RightSkill.SkillCoolDownTime}s";
        }

        // ������ 2 ������Ʈ
        secondItemIcon.sprite = secondItem.Icon;
        secondItemName.text = $"<color={GetTierColor(secondItem.Tier)}>{secondItem.Name}</color>"; // ������ �̸��� ���� ����

        // ��ų�� ���� ��� �ؽ�Ʈ�� -�� ǥ��
        if (secondItem.RightSkill.SkillCoolDownTime.ToString() == "Infinity")
        {
            secondItemRightSkillMana.text = "-";
            secondItemRightSkillCoolDown.text = "-";
        }
        else
        {
            secondItemRightSkillMana.text = secondItem.RightSkill.RequiredMp.ToString();
            secondItemRightSkillCoolDown.text = $"{secondItem.RightSkill.SkillCoolDownTime}s";
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
}