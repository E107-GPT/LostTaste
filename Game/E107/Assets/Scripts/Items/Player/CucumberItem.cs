using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "����";
        FlavorText = "�ҹ��� ���ϸ� ������ ���̸� �Ⱦ��Ѵٰ� �Ѵ�.";

        _attackDamage = 20;
        _attackRange = 10;
        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<CucumberSkill>();
    }
}
