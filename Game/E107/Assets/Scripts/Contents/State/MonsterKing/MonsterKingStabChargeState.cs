using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingStabChargeState : State
{
    public MonsterKingStabChargeState(BaseController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterMonsterKingStabChargeState();
    }

    public override void Execute()
    {
        _controller.ExecuteMonsterKingStabChargeState();
    }

    public override void Exit()
    {
        _controller.ExitMonsterKingStabChargeState();
    }
}
