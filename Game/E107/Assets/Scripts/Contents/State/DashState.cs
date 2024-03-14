using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    public DashState(BaseController controller) : base(controller)
    {
        // 생성자 내부 로직이 필요하다면 여기에 작성합니다.
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
