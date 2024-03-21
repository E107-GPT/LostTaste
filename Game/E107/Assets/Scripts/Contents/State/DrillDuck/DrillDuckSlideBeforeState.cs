using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckSlideBeforeState : State
{
    public DrillDuckSlideBeforeState(BaseController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterDrillDuckSlideBeforeState();
    }

    public override void Execute()
    {
        _controller.ExcuteDrillDuckSlideBeforeState();
    }

    public override void Exit()
    {
        _controller.ExitDrillDuckSlideBeforeState();
    }
}
