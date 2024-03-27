using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ������ ��ó���� ��ȣ�ۿ� UI�� �������� �ϴ� Ŭ�����Դϴ�.
/// </summary>
public class ItemInteraction : MonoBehaviour
{
    // ------------------------------------------------ ���� ���� ------------------------------------------------

    // ������ ���ͷ��� Ŭ������ ����� ���� ����
    private PlayerController _playerController; // �÷��̾� ��Ʈ�ѷ� ���� ����
    private IPlayerInteractable _detectedInteractable; // �÷��̾� ���� ��ȣ�ۿ�

    // ��ȣ�ۿ� UI
    [Header("[ ��ȣ�ۿ� UI ]")]
    public GameObject interactionUI;
    public TextMeshProUGUI nameText; // �̸� �ؽ�Ʈ


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Update()
    {
        // ��ȣ�ۿ� UI ������Ʈ
        UpdateInteractionUI();
    }


    // ------------------------------------------------ ����� ���� �޼��� ------------------------------------------------

    // ���ͷ��� UI�� ������Ʈ�ϴ� �޼���
    void UpdateInteractionUI()
    {
        // PlayerController ������Ʈ�� ã�Ƽ� ����
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController ������Ʈ�� ã�� �� ���� ��

        // PlayerController�� ���ͷ��ǿ� ����
        _detectedInteractable = _playerController.DetectedInteractable;

        if (_detectedInteractable != null)
        {
            // ��ȣ�ۿ� ������ ����� �����Ǹ� UI ������Ʈ
            interactionUI.SetActive(true);
            DisplayInteractableInfo();
        }
        else
        {
            // ������ ����� ������ UI ��Ȱ��ȭ �� �ؽ�Ʈ �ʱ�ȭ
            interactionUI.SetActive(false);
            nameText.text = "";
        }
    }

    // ��ȣ�ۿ� ������ ��� ���� UI�� ������ ǥ��
    void DisplayInteractableInfo()
    {
        if (_detectedInteractable is Item)
        {
            // �������� ��� ������ ���� ǥ��
            DisplayItemInfo(_detectedInteractable as Item);
        }
        else if (_detectedInteractable is RandomItemChest)
        {
            // ������ ������ ��� ���� ���� ǥ��
            DisplayChestInfo(_detectedInteractable as RandomItemChest);
        }
    }

    // ������ ������ UI�� ǥ��
    void DisplayItemInfo(Item item)
    {
        string colorHex = "";

        switch (item.Tier)
        {
            case ItemTier.COMMON:
                colorHex = "#BFBFBF";
                break;
            case ItemTier.UNCOMMON:
                colorHex = "#1AAC9C";
                break;
            case ItemTier.RARE:
                colorHex = "#3498DB";
                break;
            case ItemTier.EPIC:
                colorHex = "#9B59B6";
                break;
            case ItemTier.LEGENDARY:
                colorHex = "#F1C40F";
                break;
            case ItemTier.BOSS:
                colorHex = "#E74C3C";
                break;
            default:
                colorHex = "#FFFFFF"; // �⺻ ������ ��� ��ȯ
                break;
        }

        nameText.text = $"<color={colorHex}>{item.Name}</color>";
    }

    // ������ ���� ������ UI�� ǥ��
    void DisplayChestInfo(RandomItemChest itemChest)
    {
        string chestTypeName = "";
        string colorHex = "";

        switch (itemChest.ChestType)
        {
            case ItemChestType.WOODEN:
                chestTypeName = "�㸧�� ���� ����";
                colorHex = "#FFFFFF"; // ���
                break;
            case ItemChestType.BETTER:
                chestTypeName = "ưư�� ���� ����";
                colorHex = "#4682B4"; // û��
                break;
            case ItemChestType.GOLDEN:
                chestTypeName = "������ Ȳ�� ����";
                colorHex = "#FFD700"; // �ݻ�
                break;
            default:
                break;
        }

        nameText.text = $"<color={colorHex}>{chestTypeName}</color>";
    }
}
