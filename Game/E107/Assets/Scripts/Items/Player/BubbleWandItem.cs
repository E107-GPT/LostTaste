using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "방울 지팡이";
        FlavorText = "뽀송뽀송해지는 기분입니다.";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<BubbleWandSkill>();
    }
}
