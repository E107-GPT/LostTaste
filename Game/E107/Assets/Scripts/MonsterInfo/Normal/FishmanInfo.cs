using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishmanInfo : MonsterInfo
{
    protected override void Init()
    {
        base.Init();
        Skill = gameObject.GetOrAddComponent<FishmanAttackSkill>();
    }
}
