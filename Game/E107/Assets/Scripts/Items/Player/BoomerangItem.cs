using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "�θ޶�";
        FlavorText = "����� ���ƿ��� �ž�!";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<BoomerangSkill>();
    }
}
