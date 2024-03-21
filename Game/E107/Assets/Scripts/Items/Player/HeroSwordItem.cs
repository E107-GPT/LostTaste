using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSwordItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "����� ��";
        FlavorText = "����� ���Դϴ�.";

        _attackDamage = 50;
        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<HeroSwordSkill>();
    }
}
