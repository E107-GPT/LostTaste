using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicFlowerInfo : MonsterInfo
{ 
    protected override void Init()
    {
        base.Init();
        Skill = gameObject.GetOrAddComponent<ToxicFlowerAttackSkill>();
    }
}
