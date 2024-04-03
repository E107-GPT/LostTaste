using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckSlideAfterState : State
{
    public DrillDuckSlideAfterState(BaseController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterDrillDuckSlideAfterState();
    }

    public override void Execute()
    {
        _controller.ExecuteDrillDuckSlideAfterState();
    }

    public override void Exit()
    {
        _controller.ExitDrillDuckSlideAfterState();
    }
}
