using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckInfo : MonsterInfo
{
    protected override void Init()
    {
        base.Init();
        _skillList.Add(gameObject.GetOrAddComponent<DrillDuckAttackSkill>());
        _skillList.Add(gameObject.GetOrAddComponent<DrillDuckSlidePattern>());
    }
}
