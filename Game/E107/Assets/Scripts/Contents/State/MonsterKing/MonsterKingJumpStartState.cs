using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingJumpStartState : State
{
    public MonsterKingJumpStartState(MonsterKingController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterMonsterKingJumpStartState();
    }

    public override void Execute()
    {
        _controller.ExecuteMonsterKingJumpStartState();
    }

    public override void Exit()
    {
        _controller.ExitMonsterKingJumpStartState();
    }
}
