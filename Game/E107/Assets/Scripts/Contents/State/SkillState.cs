using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillState: State
{
    public SkillState(BaseController controller) : base(controller)
    {
        // ������ ���� ������ �ʿ��ϴٸ� ���⿡ �ۼ��մϴ�.
    }
    public override void Enter()
    {
        _controller.EnterSkill();
    }

    public override void Execute()
    {
        _controller.ExcuteSkill();
    }

    public override void Exit()
    {
        _controller.ExitSkill();
    }
}
