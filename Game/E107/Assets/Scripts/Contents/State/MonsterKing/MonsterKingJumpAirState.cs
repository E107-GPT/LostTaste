using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingJumpAirState : State
{
    public MonsterKingJumpAirState(MonsterKingController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterMonsterKingJumpAirState();
    }

    public override void Execute()
    {
        _controller.ExecuteMonsterKingJumpAirState();
    }

    public override void Exit()
    {
        _controller.ExitMonsterKingJumpAirState();
    }
}
