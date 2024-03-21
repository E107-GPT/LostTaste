using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeastItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "�Ǽ�";
        FlavorText = "�ƹ� �͵� ���� �Ǽ��̴�.";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<EmptySkill>();
    }
}
