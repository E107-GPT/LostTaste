using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ��Ʈ�� �������̽� �Ŵ����� ������ ��ų�� ���� ��ų�� �����ܰ� ��Ÿ���� ǥ���ϴ� ����� �����մϴ�.
/// </summary>
public class ControlInterfaceManager : MonoBehaviour
{
    // ------------------------------------------------ ���� ���� ------------------------------------------------

    // �κ��丮 �Ŵ����� ����� ���� ����
    private PlayerController _playerController; // �÷��̾� ��Ʈ�ѷ� ���� ����
    private Item[] _playerInventory; // �÷��̾��� �κ��丮 �迭

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

        // ������� ������ ���� ǥ��
        //ItemChange();
    }


    // ------------------------------------------------ ����� ���� �޼��� ------------------------------------------------

    // ��Ʈ�� �������̽��� ������Ʈ�ϴ� �޼���
    void UpdateControlInterface()
    {
        // PlayerController ������Ʈ�� ã�Ƽ� �����մϴ�.
        _playerController = GameObject.FindObjectOfType<PlayerController>();

        if (_playerController != null)
        {
            // PlayerController�� �κ��丮�� �����մϴ�.
            _playerInventory = _playerController.Inventory;
        }
        else
        {
            Debug.LogError("PlayerController ������Ʈ�� ã�� �� �����ϴ�.");
        }

        Item firstItem = _playerInventory[1];
        Item secondItem = _playerInventory[2];

        // ������ 1 ������ ��ų ������ ������Ʈ
        firstItemRightSkillIcon.sprite = firstItem.RightSkill.Icon;

        // ������ 2 ������ ��ų ������ ������Ʈ
        secondItemRightSkillIcon.sprite = secondItem.RightSkill.Icon;

        // ���� ��ü
        if (Input.GetKey(KeyCode.Alpha1))
        {
            // ��ų�� ���� ��� ������ ��ų �������� ��ų ���� ���������� ǥ��
            if (firstItem.RightSkill.SkillCoolDownTime.ToString() == "Infinity")
            {
                skillNonePanel.SetActive(true);
                firstItemRightSkillPanel.SetActive(false);
            }
            else
            {
                skillNonePanel.SetActive(false);
                firstItemRightSkillPanel.SetActive(true);
            }

            secondItemRightSkillPanel.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            // ��ų�� ���� ��� ������ ��ų �������� ��ų ���� ���������� ǥ��
            if (secondItem.RightSkill.SkillCoolDownTime.ToString() == "Infinity")
            {
                skillNonePanel.SetActive(true);
                secondItemRightSkillPanel.SetActive(false);
            }
            else
            {
                skillNonePanel.SetActive(false);
                secondItemRightSkillPanel.SetActive(true);
            }

            firstItemRightSkillPanel.SetActive(false);
        }
    }

    // ��� ���� �����ۿ� ���� ��ų �������� Ȱ��ȭ/��Ȱ��ȭ �ϴ� �޼���
    //void ItemChange()
    //{
    //    // ���� ��ü
    //    if (Input.GetKey(KeyCode.Alpha1))
    //    {
    //        // ��ų�� ���� ��� ������ ��ų �������� ��ų ���� ���������� ǥ��
    //        if (firstItem.RightSkill.SkillCoolDownTime.ToString() == "Infinity")
    //        {
    //            skillNonePanel.SetActive(true);
    //            firstItemRightSkillPanel.SetActive(false);
    //        }
    //        else
    //        {
    //            skillNonePanel.SetActive(false);
    //            firstItemRightSkillPanel.SetActive(true);
    //        }
            
    //        secondItemRightSkillPanel.SetActive(false);
    //    }
    //    else if (Input.GetKey(KeyCode.Alpha2))
    //    {
    //        // ��ų�� ���� ��� ������ ��ų �������� ��ų ���� ���������� ǥ��
    //        if (secondItem.RightSkill.SkillCoolDownTime.ToString() == "Infinity")
    //        {
    //            skillNonePanel.SetActive(true);
    //            secondItemRightSkillPanel.SetActive(false);
    //        }
    //        else
    //        {
    //            skillNonePanel.SetActive(false);
    //            secondItemRightSkillPanel.SetActive(true);
    //        }

    //        firstItemRightSkillPanel.SetActive(false);
    //    }
    //}
}