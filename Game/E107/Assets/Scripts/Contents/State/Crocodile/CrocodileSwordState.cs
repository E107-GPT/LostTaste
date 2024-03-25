using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileSwordState : State
{
    public CrocodileSwordState(BaseController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterCrocodileSwordState();
    }

    public override void Execute()
    {
        _controller.ExcuteCrocodileSwordState();
    }

    public override void Exit()
    {
        _controller.ExitCrocodileSwordState();
    }
}
