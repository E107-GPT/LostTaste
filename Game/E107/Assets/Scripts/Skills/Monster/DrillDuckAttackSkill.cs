using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrillDuckAttackSkill : Skill
{
    private DrillDuckController _controller;

    protected override void Init()
    {
        SkillCoolDownTime = 0;
        _controller = GetComponent<DrillDuckController>();
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        yield return new WaitForSeconds(0.2f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.DrillDuckAttackEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.transform.parent = _controller.transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        skillObj.localScale = new Vector3(1.0f, 3.0f, _attackRange / 2);    // 5.0f
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange/ 3));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 1.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        ps.transform.parent = skillObj.transform;
        ps.transform.position = skillObj.transform.position + skillObj.transform.right * 3.0f;
        ps.transform.position = skillObj.transform.position + skillObj.transform.forward * 1.0f;

        yield return new WaitForSeconds(0.8f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
