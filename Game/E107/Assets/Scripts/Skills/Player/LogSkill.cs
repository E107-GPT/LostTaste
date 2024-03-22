using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSkill : Skill
{
    protected override void Init()
    {
        SkillCoolDownTime = 10.0f;
        RequiredMp = 30;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        throw new System.NotImplementedException();
    }
}
