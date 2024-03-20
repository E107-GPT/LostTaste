using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckReadyState : State
{
    public DrillDuckReadyState(BaseController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterDrillDuckReadyState();
    }

    public override void Execute()
    {
        _controller.ExcuteDrillDuckReadyState();
    }

    public override void Exit()
    {
        _controller.ExitDrillDuckReadyState();
    }
}
