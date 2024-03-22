using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "�볪��";
        FlavorText = "���̰� �ܴ��մϴ�.";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<LogSkill>();
    }
}
