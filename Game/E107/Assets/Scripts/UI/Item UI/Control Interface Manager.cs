using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ��Ʈ�� �������̽� �Ŵ����� ������ ��ų�� ǥ���ϴ� ����� �����մϴ�.
/// </summary>
public class ControlInterfaceManager : MonoBehaviour
{
    // ------------------------------------------------ ���� ���� ------------------------------------------------

    // ��Ʈ�� �������̽� �Ŵ����� ����� ���� ����
    private PlayerController _playerController; // �÷��̾� ��Ʈ�ѷ� ���� ����
    private Item[] _playerInventory; // �÷��̾��� �κ��丮 �迭
    private int _currentItemNum; // ���� ������ ����

    // ������ 1
    [Header("[ ������ 1 ]")]
    public GameObject firstItemRightSkillPanel; // ������ 1 ������ ��ų �г�
    public Image firstItemRightSkillIcon; // ������ 1 ������ ��ų ������

    // ������ 2
    [Header("[ ������ 2 ]")]
    public GameObject secondItemRightSkillPanel; // ������ 2 ������ ��ų �г�
    public Image secondItemRightSkillIcon; // ������ 2 ������ ��ų ������

    // ��ų ���� ������
    [Header("[ ��ų ����]")]
    public GameObject skillNonePanel; // ��ų ���� �г�


    // ------------------------------------------------ Life Cylce ------------------------------------------------

    void Update()
    {
        // ��Ʈ�� �������̽� ������Ʈ
        UpdateControlInterface();
    }


    // ------------------------------------------------ ����� ���� �޼��� ------------------------------------------------

    // ��Ʈ�� �������̽��� ������Ʈ�ϴ� �޼���
    void UpdateControlInterface()
    {
        // PlayerController ������Ʈ�� ã�Ƽ� ����
        _playerController = GameObject.FindObjectOfType<PlayerController>();

        if (_playerController == null) return; // PlayerController ������Ʈ�� ã�� �� ���� ��

        // PlayerController�� �κ��丮�� ����
        _playerInventory = _playerController.Inventory;
        _currentItemNum = _playerController.CurrentItemNum;

        // PlayerController�� �κ��丮�� ���� ������ ��ȣ�� ������
        Item firstItem = _playerInventory[1];
        Item secondItem = _playerInventory[2];

        // ��ų ���� ���� Ȯ��
        bool isFirstItemSkillExists = !float.IsInfinity(firstItem.RightSkill.SkillCoolDownTime);
        bool isSecondItemSkillExists = !float.IsInfinity(secondItem.RightSkill.SkillCoolDownTime);

        // ��ų ������ ������Ʈ
        UpdateSkillIcon(firstItem, firstItemRightSkillIcon, isFirstItemSkillExists);
        UpdateSkillIcon(secondItem, secondItemRightSkillIcon, isSecondItemSkillExists);

        // ���� ��ü�� ���� ��ų �г� ������Ʈ
        ToggleSkillPanels(_currentItemNum, isFirstItemSkillExists, isSecondItemSkillExists);
    }

    // ��ų �������� ������Ʈ�ϴ� �޼���
    void UpdateSkillIcon(Item item, Image skillIcon, bool isSkillExists)
    {
        if (isSkillExists) skillIcon.sprite = item.RightSkill.Icon;
    }

    // ���� ���õ� �����ۿ� ���� �г��� ����ϴ� �޼���, ��ų ���� ���θ� ���
    void ToggleSkillPanels(int currentItemNum, bool isFirstItemSkillExists, bool isSecondItemSkillExists)
    {
        bool isSkillPanelActive = false;

        if (currentItemNum == 1 && isFirstItemSkillExists)
        {
            firstItemRightSkillPanel.SetActive(true);
            secondItemRightSkillPanel.SetActive(false);
            isSkillPanelActive = true;
        }
        else if (currentItemNum == 2 && isSecondItemSkillExists)
        {
            firstItemRightSkillPanel.SetActive(false);
            secondItemRightSkillPanel.SetActive(true);
            isSkillPanelActive = true;
        }

        skillNonePanel.SetActive(!isSkillPanelActive);
    }
}