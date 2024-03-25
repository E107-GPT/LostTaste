using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackSkill : Skill
{
    protected override void Init()
    {
        SkillCoolDownTime = 0;
        RequiredMp = 0;
        

    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Root = transform.root;

        //Debug.Log("Normal Attack");
        Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);
        yield return new WaitForSeconds(0.3f);
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.NormalAttackEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        

        skillObj.localScale = new Vector3(1.0f, 1.0f, _attackRange);
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
        


    }
}
