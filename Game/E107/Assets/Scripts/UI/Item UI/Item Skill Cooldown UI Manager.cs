using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    // ��Ÿ�� ���� ���¸� �����ϴ� ���� �߰�
    private bool isFirstItemCoolingDown = false;
    private bool isSecondItemCoolingDown = false;


    // ------------------------------------------------ Life Cylce ------------------------------------------------

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

        firstItemRightSkillCoolDown = firstItem.RightSkill.SkillCoolDownTime;
        secondItemRightSkillCoolDown = secondItem.RightSkill.SkillCoolDownTime;

        // ��Ÿ�� ���� ������Ʈ
        if (Input.GetMouseButton(1))
        {
            if (_currentItemNum == 1 && !isFirstItemCoolingDown)
            {
                StartCoroutine(UpdateItemCoolDown(firstItem, firstItemRightSkillCoolDown, firstItemRightSkillCoolDownText, firstItemCoolDownImage, firstItemKeyImage, isFirstItemSkillExists));
            }
            else if (_currentItemNum == 2 && !isSecondItemCoolingDown)
            {
                StartCoroutine(UpdateItemCoolDown(secondItem, secondItemRightSkillCoolDown, secondItemRightSkillCoolDownText, secondItemCoolDownImage, secondItemKeyImage, isSecondItemSkillExists));
            }
        }

        // ���� ��ü�� ���� ��ų ��Ÿ�� �г� ������Ʈ
        ToggleSkillCoolDownPanels(_currentItemNum);
    }

    IEnumerator UpdateItemCoolDown(Item item, float skillCoolDown, TextMeshProUGUI skillCoolDownText, Image coolDownImage, Image keyImage, bool isSkillExists)
    {
        if (!isSkillExists) yield break;

        // ��Ÿ���� ���۵� ���� ���� ����
        if (item == _playerInventory[1])
        {
            isFirstItemCoolingDown = true;
        }
        else if (item == _playerInventory[2])
        {
            isSecondItemCoolingDown = true;
        }

        while (skillCoolDown > 0.0f)
        {
            skillCoolDown -= Time.unscaledDeltaTime;
            coolDownImage.fillAmount = skillCoolDown / item.RightSkill.SkillCoolDownTime;
            keyImage.fillAmount = skillCoolDown / item.RightSkill.SkillCoolDownTime;
            skillCoolDownText.text = Mathf.Ceil(skillCoolDown).ToString() + "s";
            yield return new WaitForFixedUpdate();
        }

        skillCoolDownText.text = ""; // ��Ÿ���� ������ ������ ��, �ؽ�Ʈ�� �� ���ڿ��� ����

        // ��Ÿ���� ����� ���� ���� ����
        if (item == _playerInventory[1])
        {
            isFirstItemCoolingDown = false;
        }
        else if (item == _playerInventory[2])
        {
            isSecondItemCoolingDown = false;
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