using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItemV2 : Item
{
    protected override void Init()
    {
        base.Init();
        LeftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
    }
}
