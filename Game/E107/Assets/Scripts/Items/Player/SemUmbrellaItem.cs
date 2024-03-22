using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemUmbrellaItem : Item
{
    protected override void Init()
    {
        base.Init();

        Name = "(삼성)전기 우산";
        FlavorText = "우산 가지고 밖에 나가지 마세요.";

        _leftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        _rightSkill = gameObject.GetOrAddComponent<SemUmbrellaSkill>();
    }
}
