using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų�� ����ϸ� �������� �Ҹ��ϰų� �ٸ� ���������� �ٲ�� ��ų�Դϴ�.
/// </summary>
public abstract class ConsumingSkill : Skill
{
    private string _weaponName = "0000_Fist";

    public string WeaponName { get { return _weaponName; } set { _weaponName = value; } }

    protected override void Init()
    {
        RequiredMp = 0;
        SkillCoolDownTime = 1.0f;
    }

    protected override IEnumerator SkillCoroutine()
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        yield return null;

        Destroy(this.gameObject);

        Managers.Coroutine.Run(OnConsume(playerController));

        playerController.ObtainWeapon(_weaponName);
        playerController.StateMachine.ChangeState(new IdleState(playerController));
    }

    protected abstract IEnumerator OnConsume(PlayerController playerController);
}
