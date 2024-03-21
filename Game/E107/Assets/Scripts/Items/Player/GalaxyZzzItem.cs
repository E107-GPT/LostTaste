using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyZzzItem : Item
{
    protected override void Init()
    {
        base.Init();
        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<RightAttackSkill>();
    }
}
