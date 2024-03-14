using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(BaseController controller) : base(controller)
    {
        // ������ ���� ������ �ʿ��ϴٸ� ���⿡ �ۼ��մϴ�.
    }
    public override void Enter()
    {
        _controller.EnterMove();
    }

    public override void Execute()
    {
        _controller.ExcuteMove();
    }

    public override void Exit()
    {
        _controller.ExitMove();
    }
}
