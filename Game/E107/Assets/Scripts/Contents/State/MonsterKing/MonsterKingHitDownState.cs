using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingHitDownState : State
{
    public MonsterKingHitDownState(MonsterKingController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterMonsterKingHitDownState();
    }

    public override void Execute()
    {
        _controller.ExecuteMonsterKingHitDownState();
    }

    public override void Exit()
    {
        _controller.ExitMonsterKingHitDownState();
    }
}
