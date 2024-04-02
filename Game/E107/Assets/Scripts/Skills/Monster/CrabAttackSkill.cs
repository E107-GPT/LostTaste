using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAttackSkill : Skill
{
    [field: SerializeField]
    private int _damage;

    [field: SerializeField]
    private float _range;

    protected override void Init()
    {
        SkillCoolDownTime = 0.3f;

        _damage = Root.GetComponent<MonsterController>().Stat.AttackDamage;
        _range = Root.GetComponent<MonsterController>().Stat.AttackRange;
    }

    protected override IEnumerator SkillCoroutine()
    {
        Root.GetComponent<Animator>().CrossFade("AttackBefore", 0.3f, -1, 0);

        yield return new WaitForSeconds(SkillCoolDownTime);

        #region CheckDie
        if (Root.GetComponent<MonsterController>().IsDie)
        {
            Root.GetComponent<Animator>().CrossFade("Die", 0.3f, -1, 0);
        }
        else
        {
            Root.GetComponent<Animator>().CrossFade("Attack", 0.1f, -1, 0);

            yield return new WaitForSeconds(0.3f);

            ParticleSystem ps = Managers.Effect.Play(Define.Effect.CrabAttackEffect, Root);
            Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
            skillObj.GetComponent<SkillObject>().SetUp(Root, _damage, _seq);

            skillObj.localScale = new Vector3(1.0f, 5.0f, _range);    // 1.1f
            skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_range / 2));
            skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
            skillObj.rotation = Root.rotation;

            yield return new WaitForSeconds(0.3f);
            Managers.Resource.Destroy(skillObj.gameObject);
            Managers.Effect.Stop(ps);
        }
        #endregion
    }
}
