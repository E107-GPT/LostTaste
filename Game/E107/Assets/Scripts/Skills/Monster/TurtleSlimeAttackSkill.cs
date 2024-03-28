using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSlimeAttackSkill : Skill
{
    protected override void Init()
    {
        SkillCoolDownTime = 0;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("Slime Attack");
        yield return new WaitForSeconds(0.3f);

        // SkillObject에서 관리
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        skillObj.localScale = new Vector3(1.0f, 5.0f, _attackRange);    // 1.1f
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.TurtleSlimeAttackEffect, skillObj);

        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
