using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingHitDownChargeState : State
{
    public MonsterKingHitDownChargeState(BaseController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterMonsterKingHitDownChargeState();
    }

    public override void Execute()
    {
        _controller.ExecuteMonsterKingHitDownChargeState();
    }

    public override void Exit()
    {
        _controller.ExitMonsterKingHitDownChargeState();
    }
}
