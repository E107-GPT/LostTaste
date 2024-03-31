using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttackState : WeaponState
{
    public BowAttackState(Skill skill) : base(skill)
    {
    }

    public override void Enter()
    {
        _skill.EnterAttack();
    }

    public override void Execute()
    {
        _skill.ExecuteAttack();
    }

    public override void Exit()
    {
        _skill.ExitAttack();
    }
}
