using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : State
{
    public DieState(BaseController controller) : base(controller)
    {
        // ������ ���� ������ �ʿ��ϴٸ� ���⿡ �ۼ��մϴ�.
    }
    public override void Enter()
    {
        _controller.EnterDie();
    }

    public override void Execute()
    {
        _controller.ExcuteDie();
    }

    public override void Exit()
    {
        _controller.ExitDie();
    }
}
