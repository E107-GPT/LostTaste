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
    public TextMeshProUGUI itemName; // ������ �̸�


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
            // UI Ȱ��ȭ
            interactionUI.SetActive(true);

            // �������� ������ ������
            Item item = _detectedInteractable as Item;

            if (item != null)
            {
                // �������� �� ������ UI�� ǥ��
                itemName.text = $"<color={GetTierColor(item.Tier)}>{item.Name}</color>"; // ������ �̸��� ���� ����
            }
        }
        else
        {
            // UI ��Ȱ��ȭ
            interactionUI.SetActive(false);
            itemName.text = "";
        }

        Debug.Log($"�̸�@@@@@@@@@@@@{_detectedInteractable}");
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
