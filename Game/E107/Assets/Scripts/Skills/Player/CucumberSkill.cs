using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberSkill : Skill
{
    protected override void Init()
    {
        SkillCoolDownTime = 0;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("���� ��");

        yield return new WaitForSeconds(0);
    }
}
