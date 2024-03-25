using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingStabState : State
{
    public MonsterKingStabState(MonsterKingController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterMonsterKingStabState();
    }

    public override void Execute()
    {
        _controller.ExecuteMonsterKingStabState();
    }

    public override void Exit()
    {
        _controller.ExitMonsterKingStabState();
    }
}
