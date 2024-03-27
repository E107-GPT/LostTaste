using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ������ ��ó���� ��ȣ�ۿ� UI�� �������� �ϴ� Ŭ�����Դϴ�.
/// </summary>
public class ItemInteraction : MonoBehaviour
{
    // ��ȣ�ۿ� UI
    [Header("[ ��ȣ�ۿ� UI ]")]
    public GameObject interactionUI;
    public TextMeshProUGUI itemName; // ������ �̸�

    private Item currentItem; // ���� ��ȣ�ۿ� ���� ������

    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Update()
    {
        // ��ȣ�ۿ� UI ������Ʈ
        UpdateInteractionUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ĳ���Ͱ� ������ ���� ������ ����
        {
            // �ε��� �������� ������ ������
            currentItem = other.GetComponent<Item>();
            if (currentItem != null)
            {
                interactionUI.SetActive(true); // ��ȣ�ۿ� UI Ȱ��ȭ
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && currentItem != null) // ĳ���Ͱ� ������ ������ ���
        {
            interactionUI.SetActive(false); // ��ȣ�ۿ� UI ��Ȱ��ȭ
            currentItem = null; // ���� ��ȣ�ۿ� ���� ������ ���� �ʱ�ȭ
        }
    }


    // ------------------------------------------------ ����� ���� �޼��� ------------------------------------------------

    // �κ��丮�� ������Ʈ�ϴ� �޼���
    void UpdateInteractionUI()
    {
        if (currentItem != null)
        {
            itemName.text = $"<color={GetTierColor(currentItem.Tier)}>{currentItem.Name}</color>"; // ������ �̸��� ���� ����
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
