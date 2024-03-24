using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckInfo : MonsterInfo
{
    protected override void Init()
    {
        base.Init();
        Skill = gameObject.GetOrAddComponent<DrillDuckAttackSkill>();
        Patterns.Add(gameObject.GetOrAddComponent<DrillDuckSlidePattern>());
        Patterns.Add(gameObject.GetOrAddComponent<DrillDuckBeforePatttern>());
    }
}
