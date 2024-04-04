using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Slime�� ����ϴ� ���� ������ ��� �ִ´�.
// Slime�� Component�� ������.
public class MushroomInfo : MonsterInfo
{

    protected override void Init()
    {
        base.Init();
        Skill = gameObject.GetOrAddComponent<MushroomAttackSkill>();
    }
}
