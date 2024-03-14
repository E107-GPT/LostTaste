using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public IdleState(BaseController controller) : base(controller)
    {
        // ������ ���� ������ �ʿ��ϴٸ� ���⿡ �ۼ��մϴ�.
    }
    public override void Enter()
    {
        _controller.EnterIdle();
    }

    public override void Execute()
    {
        _controller.ExcuteIdle();
    }

    public override void Exit()
    {
        _controller.ExitIdle();
    }
}
