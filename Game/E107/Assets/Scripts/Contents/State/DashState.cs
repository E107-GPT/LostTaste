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
        Debug.Log("1111111111111111111111");
        _controller.EnterDash();
        Debug.Log("2222222222222222222222");
    }

    public override void Execute()
    {
        Debug.Log("33333333333333333333");
        _controller.ExcuteDash();
        Debug.Log("444444444444444444444");
    }

    public override void Exit()
    {
        Debug.Log("555555555555555555");
        _controller.ExitDash();
        Debug.Log("6666666666666666666666");
    }
}
