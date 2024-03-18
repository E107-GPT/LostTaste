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
        _controller.EnterSlide();
    }

    public override void Execute()
    {
        _controller.ExcuteSlide();
    }

    public override void Exit()
    {
        _controller.ExitSlide();
    }
}
