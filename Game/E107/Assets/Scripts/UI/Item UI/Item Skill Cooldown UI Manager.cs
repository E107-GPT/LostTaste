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
    public TextMeshProUGUI firstItemRightSkillCoolDownText; // ������ 1 ������ ��ų ��Ÿ��

    // ������ 2
    [Header("[ ������ 2 ]")]
    public GameObject secondItemRightSkillCoolDownPanel; // ������ 2 ������ ��ų ��Ÿ�� �г�
    public Image secondItemCoolDownImage; // ������ 2 ������ ��ų ��Ÿ�� �̹���
    public TextMeshProUGUI secondItemRightSkillCoolDownText; // ������ 2 ������ ��ų ��Ÿ��

    // ���� ��Ÿ�� ���� ���� ����
    private float firstItemRightSkillCoolDown; // ������ 1 ������ ��ų ���� ��Ÿ��
    private float secondItemRightSkillCoolDown; // ������ 2 ������ ��ų ���� ��Ÿ��


    // ------------------------------------------------ Life Cylce ------------------------------------------------

    void Start()
    {
        firstItemCoolDownImage.fillAmount = 0; // �ʱ� Fill Amount�� 0���� ����
        secondItemCoolDownImage.fillAmount = 0; // �ʱ� Fill Amount�� 0���� ����
        firstItemRightSkillCoolDownText.text = "";
        secondItemRightSkillCoolDownText.text = "";
    }

    void Update()
    {
        // ��Ÿ�� �г� ������Ʈ
        UpdateItemCoolDownPanel();
    }


    // ------------------------------------------------ ����� ���� �޼��� ------------------------------------------------

    // ��Ÿ�� �г��� ������Ʈ�ϴ� �޼���
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
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (_currentItemNum == 1) StartCoroutine(UpdateItemCoolDown(firstItem, firstItemRightSkillCoolDown, firstItemRightSkillCoolDownText, firstItemCoolDownImage, isFirstItemSkillExists));
            else StartCoroutine(UpdateItemCoolDown(secondItem, secondItemRightSkillCoolDown, secondItemRightSkillCoolDownText, secondItemCoolDownImage, isSecondItemSkillExists));
        }

        // ���� ��ü�� ���� ��ų ��Ÿ�� �г� ������Ʈ
        ToggleSkillCoolDownPanels(_currentItemNum);
    }

    // ������ ��Ÿ�� ������Ʈ �޼���
    IEnumerator UpdateItemCoolDown(Item item, float skillCoolDown, TextMeshProUGUI skillCoolDownText, Image coolDownImage, bool isSkillExists)
    {
        if (!isSkillExists) yield break;

        while (skillCoolDown > 0.0f)
        {
            skillCoolDown -= Time.unscaledDeltaTime;
            coolDownImage.fillAmount = skillCoolDown / item.RightSkill.SkillCoolDownTime;
            skillCoolDownText.text = Mathf.Ceil(skillCoolDown).ToString() + "s";
            yield return new WaitForFixedUpdate();
        }

        // ��Ÿ���� ������ ������ ��, �ؽ�Ʈ�� �� ���ڿ��� ����
        skillCoolDownText.text = "";
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