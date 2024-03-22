using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemUmbrellaItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "(�Ｚ)���� ���";
        FlavorText = "��� ������ �ۿ� ������ ������.";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<SemUmbrellaSkill>();
    }
}
