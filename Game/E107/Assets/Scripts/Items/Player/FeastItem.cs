using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeastItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "맨손";
        FlavorText = "아무 것도 없는 맨손이다.";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<EmptySkill>();
    }
}
