using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų�� ����ϸ� �������� �Ҹ��ϰų� �ٸ� ���������� �ٲ�� ��ų�Դϴ�.
/// </summary>
public abstract class ConsumingSkill : Skill
{
    private string _weaponName = "Feast";

    public string WeaponName { get { return _weaponName; } set { _weaponName = value; } }

    protected override void Init()
    {
        RequiredMp = 0;
        SkillCoolDownTime = 1.0f;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        yield return null;

        Destroy(this.gameObject);

        StartCoroutine(OnConsume(playerController));

        playerController.ObtainWeapon(_weaponName);
        playerController.StateMachine.ChangeState(new IdleState(playerController));
    }

    protected abstract IEnumerator OnConsume(PlayerController playerController);
}
