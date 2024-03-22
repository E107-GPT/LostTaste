using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSwordSkill : Skill
{
    private const int DAMAGE = 150;
    private readonly Vector3 SCALE = new Vector3(5.0f, 2.0f, 5.0f);

    protected override void Init()
    {
        SkillCoolDownTime = 5.0f;
        RequiredMp = 20;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Root = transform.root;

        Debug.Log("Hero Sword Attack");
        Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0, 0.7f);
        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.StrongSwingEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, DAMAGE, _seq);

        Vector3 offset = Root.forward.normalized * 1.5f;
        ps.transform.position += offset;
        skillObj.position += offset;

        skillObj.localScale = SCALE;
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);



    }
}
