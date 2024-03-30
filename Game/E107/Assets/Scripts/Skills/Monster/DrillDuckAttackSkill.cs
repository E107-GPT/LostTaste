using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrillDuckAttackSkill : Skill
{
    private DrillDuckController _controller;
    private Animator _animator;

    [field: SerializeField]
    private int _damage;

    [field: SerializeField]
    private float _range;

    protected override void Init()
    {
        SkillCoolDownTime = 0.2f;
        _controller = GetComponent<DrillDuckController>();
        _animator = _controller.GetComponent<Animator>();

        _damage = Root.GetComponent<MonsterController>().Stat.AttackDamage;
        _range = Root.GetComponent<MonsterController>().Stat.AttackRange;
    }

    protected override IEnumerator SkillCoroutine()
    {
        _animator.CrossFade("AttackBefore", 0.3f, -1, 0);

        yield return new WaitForSeconds(SkillCoolDownTime);

        if (Root.GetComponent<MonsterController>().IsDie)
        {
            _animator.CrossFade("Die", 0.3f, -1, 0);
        }
        else
        {
            _animator.CrossFade("Attack", 0.3f, -1, 0);

            yield return new WaitForSeconds(0.8f);

            ParticleSystem ps = Managers.Effect.Play(Define.Effect.DrillDuckAttackEffect, Root);
            Vector3 rootForward = Root.TransformDirection(Vector3.forward * _range);
            Vector3 rootRight = Root.TransformDirection(Vector3.right * 0.8f);
            Vector3 rootUp = Root.TransformDirection(Vector3.up * 1.5f);
            ps.transform.position = Root.position + rootForward + rootRight + rootUp;

            yield return new WaitForSeconds(0.2f);

            Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
            skillObj.GetComponent<SkillObject>().SetUp(Root, _damage, _seq);

            skillObj.rotation = Root.rotation;
            rootForward = Root.TransformDirection(Vector3.forward * (_range / 2));
            rootRight = Root.TransformDirection(Vector3.right * 0.8f);
            rootUp = Root.TransformDirection(Vector3.up * 1.5f);

            skillObj.position = Root.position + rootForward + rootRight + rootUp;
            

            //skillObj.transform.parent = _controller.transform;
            skillObj.localScale = new Vector3(1.7f, 6.0f, _range);    // 5.0f
            //skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_range / 3));
            //skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 1.5f, skillObj.position.z);
            

            yield return new WaitForSeconds(0.15f);
            Managers.Resource.Destroy(skillObj.gameObject);

            yield return new WaitForSeconds(0.8f);
            Managers.Effect.Stop(ps);


        }
    }
}
