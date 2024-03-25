using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingInfo : MonsterInfo
{
    protected override void Init()
    {
        base.Init();
        _skill = null;
        Patterns.Add(gameObject.GetOrAddComponent<MonsterKingHitDownPattern>());
    }
}
