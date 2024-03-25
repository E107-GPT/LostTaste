using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingSlashState : State
{
    public MonsterKingSlashState(MonsterKingController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterMonsterKingSlashState();
    }

    public override void Execute()
    {
        _controller.ExecuteMonsterKingSlashState();
    }

    public override void Exit()
    {
        _controller.ExitMonsterKingSlashState();
    }
}
