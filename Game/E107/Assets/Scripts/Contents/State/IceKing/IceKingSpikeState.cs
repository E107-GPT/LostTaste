using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKingSpikeState : State
{
    public IceKingSpikeState(BaseController controller) : base(controller)
    {
    }

    public override void Enter()
    {
        _controller.EnterIceKingSpikeState();
    }

    public override void Execute()
    {
        _controller.ExcuteIceKingSpikeState();
    }

    public override void Exit()
    {
        _controller.ExitIceKingSpikeState();
    }
}
