using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Slime이 사용하는 공격 패턴을 모두 넣는다.
// Slime이 Component로 가진다.
public class SlimeInfo : MonsterInfo
{
    protected override void Init()
    {
        base.Init();
        _skillList.Add(gameObject.GetOrAddComponent<SlimeAttackSkill>());
    }
}
