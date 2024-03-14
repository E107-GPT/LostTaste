using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public MoveState(BaseController controller) : base(controller)
    {
        // 생성자 내부 로직이 필요하다면 여기에 작성합니다.
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
