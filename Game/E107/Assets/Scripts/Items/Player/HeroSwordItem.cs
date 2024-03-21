using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSwordItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "용사의 검";
        FlavorText = "평범한 검입니다.";

        _attackDamage = 50;
        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<HeroSwordSkill>();
    }
}
