using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckSlideState : State
{
    public DrillDuckSlideState(BaseController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterDrillDuckSlideState();
    }

    public override void Execute()
    {
        _controller.ExcuteDrillDuckSlideState();
    }

    public override void Exit()
    {
        _controller.ExitDrillDuckSlideState();
    }
}
