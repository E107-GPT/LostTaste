using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeInfo : MonsterInfo
{
    protected override void Init()
    {
        base.Init();
        _skillList.Add(gameObject.GetOrAddComponent<SlimeAttackSkill>());
    }
}
