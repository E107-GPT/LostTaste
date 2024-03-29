using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamanderAttackSkill : Skill
{
    [field: SerializeField]
    private int _damage;

    protected override void Init()
    {
        SkillCoolDownTime = 0.5f;

        _damage = Root.GetComponent<MonsterController>().Stat.AttackDamage;
    }

    protected override IEnumerator SkillCoroutine()
    {
        Root.GetComponent<Animator>().CrossFade("AttackBefore", 0.3f, -1, 0);

        yield return new WaitForSeconds(SkillCoolDownTime);

        Vector3 dir = Root.forward;
        dir = new Vector3(dir.x, 0, dir.z);
        Root.GetComponent<Animator>().CrossFade("Attack", 0.3f, -1, 0);

        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.SalamanderFlameEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _damage, _seq);

        ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y + 0.5f, ps.transform.position.z);

        skillObj.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        skillObj.position = Root.transform.position;
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation.SetLookRotation(dir);

        float moveDuration = 1.1f; // ����ü�� ���ư��� �ð��� �����մϴ�.
        float timer = 0; // Ÿ�̸� �ʱ�ȭ
        float speed = 10.0f; // ����ü�� �ӵ��� �����մϴ�.

        while (timer < moveDuration)
        {
            // ����ü�� ��ƼŬ �ý����� ������ �����Դϴ�.
            Vector3 moveStep = dir * speed * Time.deltaTime;
            skillObj.position += moveStep;
            ps.transform.position += moveStep;

            timer += Time.deltaTime; // Ÿ�̸Ӹ� ������Ʈ�մϴ�.
            yield return null; // ���� �����ӱ��� ����մϴ�.
        }

        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
