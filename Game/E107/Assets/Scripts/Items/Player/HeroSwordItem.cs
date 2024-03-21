using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSwordItem : Item
{
    protected override void Init()
    {
        base.Init();
        _attackDamage = 50;
        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
    }
}
