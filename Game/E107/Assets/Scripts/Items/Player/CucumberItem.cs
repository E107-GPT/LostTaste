using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "오이";
        FlavorText = "소문에 의하면 마왕은 오이를 싫어한다고 한다.";

        _attackDamage = 20;
        _attackRange = 10;
        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<CucumberSkill>();
    }
}
