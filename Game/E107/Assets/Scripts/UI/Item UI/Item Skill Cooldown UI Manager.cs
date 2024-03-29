using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using UnityEditor.Experimental.GraphView;

/// <summary>
/// ������ ��ų ��Ÿ�� UI �Ŵ����� ��Ÿ���� �ִ� ������ ��ų�� ��Ÿ���� ǥ���ϴ� ����� �����մϴ�.
/// </summary>
public class ItemSkillCooldownUIManager : MonoBehaviour
{
    // ------------------------------------------------ ���� ���� ------------------------------------------------

    // ������ ��ų ��Ÿ�� UI �Ŵ����� ����� ���� ����
    private PlayerController _playerController; // �÷��̾� ��Ʈ�ѷ� ���� ����
    private Item[] _playerInventory; // �÷��̾��� �κ��丮 �迭
    private int _currentItemNum; // ���� ������ ����

    // ������ 1
    [Header("[ ������ 1 ]")]
    public GameObject firstItemRightSkillCoolDownPanel; // ������ 1 ������ ��ų ��Ÿ�� �г�
    public Image firstItemCoolDownImage; // ������ 1 ������ ��ų ��Ÿ�� �̹���
    public Image firstItemKeyImage; // ������ 1 ������ ��ų Ű �̹���
    public TextMeshProUGUI firstItemRightSkillCoolDownText; // ������ 1 ������ ��ų ��Ÿ��

    // ������ 2
    [Header("[ ������ 2 ]")]
    public GameObject secondItemRightSkillCoolDownPanel; // ������ 2 ������ ��ų ��Ÿ�� �г�
    public Image secondItemCoolDownImage; // ������ 2 ������ ��ų ��Ÿ�� �̹���
    public Image secondItemKeyImage; // ������ 2 ������ ��ų Ű �̹���
    public TextMeshProUGUI secondItemRightSkillCoolDownText; // ������ 2 ������ ��ų ��Ÿ��

    // ���� ��Ÿ�� ���� ���� ����
    private float firstItemRightSkillCoolDown; // ������ 1 ������ ��ų ���� ��Ÿ��
    private float secondItemRightSkillCoolDown; // ������ 2 ������ ��ų ���� ��Ÿ��

    private bool _isCurrentItemCoolingDownPrev = false;


    // ------------------------------------------------ Life Cycle ------------------------------------------------

    void Start()
    {
        // �ʱ� Fill Amount�� 0���� ����
        firstItemCoolDownImage.fillAmount = 0;
        secondItemCoolDownImage.fillAmount = 0;
        firstItemKeyImage.fillAmount = 0;
        secondItemKeyImage.fillAmount = 0;

        // �ʱ� ��Ÿ�� �ؽ�Ʈ �� ���ڿ��� ����
        firstItemRightSkillCoolDownText.text = "";
        secondItemRightSkillCoolDownText.text = "";
    }

    void Update()
    {
        // ��Ÿ�� �г� ������Ʈ
        UpdateItemCoolDownPanel();
    }


    // ------------------------------------------------ ����� ���� �޼��� ------------------------------------------------

    void UpdateItemCoolDownPanel()
    {
        // PlayerController ������Ʈ�� ã�Ƽ� ����
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController ������Ʈ�� ã�� �� ���� ��

        // PlayerController�� �κ��丮�� ����
        _playerInventory = _playerController.Inventory;

        Item currentItem = _playerController.Inventory[_playerController.CurrentItemNum];

        // ��ų ���� ���� Ȯ��
        bool isCurrentItemSkillExists = currentItem.RightSkill != null && !(currentItem.RightSkill is EmptySkill);

        bool isCurrentItemCoolingDown = !currentItem.RightSkill.IsPlayerCastable(_playerController);

        if (isCurrentItemCoolingDown && isCurrentItemSkillExists)
        {
            UpdateItemSkillCoolDown(currentItem);
        }
        else if (!isCurrentItemCoolingDown && _isCurrentItemCoolingDownPrev)
        {
            ResetCoolDownUI(currentItem);
        }

        // ���� ��ü�� ���� ��ų ��Ÿ�� �г� ������Ʈ
        ToggleSkillCoolDownPanels(_currentItemNum);

        // ������ ���� �Ǵ� ���� ����
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.B))
        {
            // ĳ���Ͱ� '��� ����' �Ǵ� '�ȱ� ����'�� �ƴ� ��� �Լ��� ��������
            if (!(_playerController.CurState is IdleState || _playerController.CurState is MoveState)) return;
        }

        _isCurrentItemCoolingDownPrev = isCurrentItemCoolingDown;
    }

    void UpdateItemSkillCoolDown(Item item)
    {
        float remainingTime = item.RightSkill.SkillCoolDownTime - (Time.time - item.RightSkill.LastCastTime);
        float percentage = remainingTime / item.RightSkill.SkillCoolDownTime;
        string remainingTimeString;
        if (remainingTime > 1.0f)
        {
            remainingTimeString = Mathf.Ceil(remainingTime).ToString() + "s";
        }
        else
        {
            remainingTimeString = string.Format("{0:0.0}", remainingTime) + "s";
        }

        UpdateCoolDownUI(item, percentage, remainingTimeString);
    }

    // ��ٿ� UI ������Ʈ �޼��� (���� �߰�)
    void UpdateCoolDownUI(Item item, float fillAmount, string text)
    {
        if (item == _playerInventory[1])
        {
            firstItemRightSkillCoolDownText.text = text;
            firstItemCoolDownImage.fillAmount = fillAmount;
            firstItemKeyImage.fillAmount = fillAmount;
        }
        else if (item == _playerInventory[2])
        {
            secondItemRightSkillCoolDownText.text = text;
            secondItemCoolDownImage.fillAmount = fillAmount;
            secondItemKeyImage.fillAmount = fillAmount;
        }
    }

    // �ڷ�ƾ�� ���� �� ��Ÿ�� �г��� �ʱ�ȭ �ϴ� �޼���
    void ResetCoolDownUI(Item item)
    {
        if (item == _playerInventory[1])
        {
            firstItemRightSkillCoolDownText.text = "";
            firstItemCoolDownImage.fillAmount = 0;
            firstItemKeyImage.fillAmount = 0;
        }
        else if (item == _playerInventory[2])
        {
            secondItemRightSkillCoolDownText.text = "";
            secondItemCoolDownImage.fillAmount = 0;
            secondItemKeyImage.fillAmount = 0;
        }
    }

    // ���� ���õ� �����ۿ� ���� ��Ÿ�� �г��� ����ϴ� �޼���
    void ToggleSkillCoolDownPanels(int currentItemNum)
    {
        if (currentItemNum == 1)
        {
            firstItemRightSkillCoolDownPanel.SetActive(true);
            secondItemRightSkillCoolDownPanel.SetActive(false);
        }
        else if (currentItemNum == 2)
        {
            firstItemRightSkillCoolDownPanel.SetActive(false);
            secondItemRightSkillCoolDownPanel.SetActive(true);
        }
    }
}