using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Slime�� ����ϴ� ���� ������ ��� �ִ´�.
// Slime�� Component�� ������.
public class SlimeInfo : MonsterInfo
{

    protected override void Init()
    {
        base.Init();
        Skill = gameObject.GetOrAddComponent<SlimeAttackSkill>();
    }
}
