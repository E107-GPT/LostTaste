using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingSlashChargeState : State
{
    public MonsterKingSlashChargeState(BaseController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterMonsterKingSlashChargeState();
    }

    public override void Execute()
    {
        _controller.ExecuteMonsterKingSlashChargeState();
    }

    public override void Exit()
    {
        _controller.ExitMonsterKingSlashChargeState();
    }
}
