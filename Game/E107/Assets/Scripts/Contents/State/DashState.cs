using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    public DashState(BaseController controller) : base(controller)
    {
        // ������ ���� ������ �ʿ��ϴٸ� ���⿡ �ۼ��մϴ�.
    }
    public override void Enter()
    {
        _controller.EnterDash();
    }

    public override void Execute()
    {
        _controller.ExcuteDash();
    }

    public override void Exit()
    {
        _controller.ExitDash();
    }
}
