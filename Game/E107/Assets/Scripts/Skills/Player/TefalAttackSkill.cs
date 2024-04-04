using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TefalAttackSkill : NormalAttackSkill
{
    protected override IEnumerator SkillCoroutine()
    {
        StartCoroutine(base.SkillCoroutine());

        yield return new WaitForSeconds(0.3f);

        Managers.Sound.Play("metal_crash");
    }
}
