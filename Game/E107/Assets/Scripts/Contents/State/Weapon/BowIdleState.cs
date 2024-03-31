using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowIdleState : WeaponState
{
    public BowIdleState(Skill skill) : base(skill)
    {
    }

    public override void Enter()
    {
        _skill.EnterIdle();
    }

    public override void Execute()
    {
        _skill.ExecuteIdle();
    }

    public override void Exit()
    {
        _skill.ExitIdle();
    }
}
