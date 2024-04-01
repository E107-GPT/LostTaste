using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingInfo : MonsterInfo
{
    protected override void Init()
    {
        // HitDownAfter Effect가 Collider에 포함되지 않도록 수정
        // Jump 수정
        // HitDownStart 수정: 이펙트를 도끼에 붙인다.
        base.Init();
        _skill = null;
        Patterns.Add(gameObject.GetOrAddComponent<MonsterKingHitDownChargePattern>());
        Patterns.Add(gameObject.GetOrAddComponent<MonsterKingHitDownPattern>());
        //Patterns.Add(gameObject.GetOrAddComponent<MonsterKingHitDownAfterPattern>());

        Patterns.Add(gameObject.GetOrAddComponent<MonsterKingSlashChargePattern>());
        Patterns.Add(gameObject.GetOrAddComponent<MonsterKingSlashPattern>());

        Patterns.Add(gameObject.GetOrAddComponent<MonsterKingStabChargePattern>());
        Patterns.Add(gameObject.GetOrAddComponent<MonsterKingStabPattern>());

        Patterns.Add(gameObject.GetOrAddComponent<MonsterKingJumpStartPattern>());
        //Patterns.Add(gameObject.GetOrAddComponent<MonsterKingJumpAirPattern>());
        Patterns.Add(gameObject.GetOrAddComponent<MonsterKingJumpEndPattern>());

        
    }
}
