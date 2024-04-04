using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingJumpEndState : State
{
    public MonsterKingJumpEndState(MonsterKingController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterMonsterKingJumpEndState();
    }

    public override void Execute()
    {
        _controller.ExecuteMonsterKingJumpEndState();
    }

    public override void Exit()
    {
        _controller.ExitMonsterKingJumpEndState();
    }
}
