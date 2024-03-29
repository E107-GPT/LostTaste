using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileAttackSkill : Skill
{
    private CrocodileController _controller;


    protected override void Init()
    {
        SkillCoolDownTime = 0;
        _controller = GetComponent<CrocodileController>();
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Root = transform.root;
        Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);

        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.CrocodileAttackEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.transform.parent = _controller.transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        skillObj.localScale = new Vector3(1.2f, 3.0f, 2.7f);
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * 2.2f);
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 1.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;


        yield return new WaitForSeconds(0.4f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
