using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileInfo : MonsterInfo
{
    protected override void Init()
    {
        base.Init();
        Skill = gameObject.GetOrAddComponent<CrocodileAttackSkill>();
        Patterns.Add(gameObject.GetOrAddComponent<CrocodileSwordPattern>());
    }
}
