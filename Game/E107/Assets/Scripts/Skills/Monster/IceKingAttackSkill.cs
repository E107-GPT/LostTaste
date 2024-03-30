using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKingAttackSkill : Skill
{
    private IceKingController _controller;

    [field: SerializeField]
    private int _damage;
    [field: SerializeField]
    private float _range;

    protected override void Init()
    {
        //SkillCoolDownTime = 3.0f;
        _controller = GetComponent<IceKingController>();
        _damage = Root.GetComponent<MonsterController>().Stat.AttackDamage;
        _range = Root.GetComponent<MonsterController>().Stat.AttackRange;
    }

    protected override IEnumerator SkillCoroutine()
    {
        Root.GetComponent<Animator>().CrossFade("Attack", 0.3f, -1, 0);

        yield return new WaitForSeconds(0.5f);

        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _damage, _seq);
        skillObj.rotation = Root.rotation;
        Vector3 rootForward = Root.TransformDirection(Vector3.forward);
        skillObj.transform.position = Root.position + rootForward;
        skillObj.localScale = new Vector3(1.0f, 3.0f, _range / 2);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.IceKingCleaveEffect, Root);
        ps.transform.position = skillObj.transform.position - skillObj.transform.forward * 3.0f;

        float moveDuration = 0.33f;
        float timer = 0;
        float speed = 20.0f;
        while (timer < moveDuration)
        {
            Vector3 moveStep = skillObj.forward * speed * Time.deltaTime;
            skillObj.position += moveStep;

            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);
        Managers.Resource.Destroy(skillObj.gameObject);

        yield return new WaitForSeconds(0.6f);
        Managers.Effect.Stop(ps);
    }
}
