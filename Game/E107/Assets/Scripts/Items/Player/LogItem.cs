using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "통나무";
        FlavorText = "무겁고 단단합니다.";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<LogSkill>();
    }
}
