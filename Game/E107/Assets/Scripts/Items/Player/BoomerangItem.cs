using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "부메랑";
        FlavorText = "사랑은 돌아오는 거야!";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<BoomerangSkill>();
    }
}
