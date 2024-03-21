using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyZzzItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "갤럭시 ZZZ";
        FlavorText = "이걸로 종이접기도 할 수 있어요.";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<GalaxyZzzSkill>();
    }
}
