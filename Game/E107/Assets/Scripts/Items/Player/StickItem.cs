using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickItem : Item
{
    protected override void Init()
    {
        base.Init();
        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
    }
}
