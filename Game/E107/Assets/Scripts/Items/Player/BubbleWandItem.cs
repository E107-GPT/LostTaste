using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "��� ������";
        FlavorText = "�ǼۻǼ������� ����Դϴ�.";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<BubbleWandSkill>();
    }
}
