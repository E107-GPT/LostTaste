using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKingInfo : MonsterInfo
{
    protected override void Init()
    {
        base.Init();
        Skill = gameObject.GetOrAddComponent<IceKingAttackSkill>();
        Patterns.Add(gameObject.GetOrAddComponent<IceKingSpikePattern>());
    }
}
