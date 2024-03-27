using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ���� ��ų ��Ÿ�� UI �Ŵ����� ���� ��ų�� ��Ÿ���� ǥ���ϴ� ����� �����մϴ�.
/// </summary>
public class ClassSkillCooldownUIManager : MonoBehaviour
{
    // ------------------------------------------------ ���� ���� ------------------------------------------------

    // ���� ��ų ��Ÿ�� UI �Ŵ����� ����� ���� ����
    private PlayerController _playerController; // �÷��̾� ��Ʈ�ѷ� ���� ����

    // ���� ��ų �г�
    [Header("[ ���� ��ų �г� ]")]
    public Image classSkillCoolDownImage; // ���� ��ų ��Ÿ�� �̹���
    public Image classSkillKeyImage; // ���� ��ų Ű �̹���
    public TextMeshProUGUI classSkillCoolDownText; // ���� ��ų ��Ÿ��

    // ���� ��Ÿ�� ���� ���� ����
    private float classSkillCoolDown; // ���� ��ų ���� ��Ÿ��

    // ��Ÿ�� ���� ���¸� �����ϴ� ���� �߰�
    private bool isClassSkillCoolingDown = false;


    // ------------------------------------------------ Life Cylce ------------------------------------------------

    void Start()
    {
        // �ʱ� Fill Amount�� 0���� ����
        classSkillCoolDownImage.fillAmount = 0;
        classSkillKeyImage.fillAmount = 0;

        // �ʱ� ��Ÿ�� �ؽ�Ʈ �� ���ڿ��� ����
        classSkillCoolDownText.text = "";
    }

    void Update()
    {
        // ���� ��ų ��Ÿ�� �г� ������Ʈ
        UpdateClassSkillCoolDownPanel();
    }


    // ------------------------------------------------ ����� ���� �޼��� ------------------------------------------------

    void UpdateClassSkillCoolDownPanel()
    {
        // PlayerController ������Ʈ�� ã�Ƽ� ����
        _playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (_playerController == null) return; // PlayerController ������Ʈ�� ã�� �� ���� ��

        classSkillCoolDown = 10.0f;

        // ��Ÿ�� ���� ������Ʈ
        if (Input.GetKey(KeyCode.Q) && !isClassSkillCoolingDown) StartCoroutine(UpdateClassSkillCoolDown(classSkillCoolDown, classSkillCoolDownText, classSkillCoolDownImage, classSkillKeyImage));
    }

    IEnumerator UpdateClassSkillCoolDown(float skillCoolDown, TextMeshProUGUI skillCoolDownText, Image coolDownImage, Image keyImage)
    {
        // ��Ÿ���� ���۵� ���� ���� ����
        isClassSkillCoolingDown = true;

        while (skillCoolDown > 0.0f)
        {
            skillCoolDown -= Time.deltaTime;
            coolDownImage.fillAmount = skillCoolDown / 10.0f;
            keyImage.fillAmount = skillCoolDown / 10.0f;
            skillCoolDownText.text = Mathf.Ceil(skillCoolDown).ToString() + "s";
            yield return new WaitForFixedUpdate();
        }

        skillCoolDownText.text = ""; // ��Ÿ���� ������ ������ ��, �ؽ�Ʈ�� �� ���ڿ��� ����

        // ��Ÿ���� ����� ���� ���� ����
        isClassSkillCoolingDown = false;
    }
}