using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    public static event Action<bool, string> OnDashCast; // 대쉬 시전 여부 전달을 위한 이벤트 정의

    public DashState(BaseController controller) : base(controller)
    {
        
         // 스킬 시전 성공하면 이벤트 발생
        // 생성자 내부 로직이 필요하다면 여기에 작성합니다.
    }
    public override void Enter()
    {
        OnDashCast?.Invoke(true, _controller.gameObject.name);
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
