using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ƹ� �͵� �� �ϴ� ��ų�Դϴ�.
/// </summary>
public class EmptySkill : Skill
{
    protected override void Init()
    {
        SkillCoolDownTime = Mathf.Infinity;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        yield return null;
        playerController.StateMachine.ChangeState(new IdleState(playerController));
    }
}