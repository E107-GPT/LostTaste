using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSlimeInfo : MonsterInfo
{
    protected override void Init()
    {
        base.Init();
        Skill = gameObject.GetOrAddComponent<TurtleSlimeAttackSkill>();
    }
}
