using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightAttackSkill :Skill
{
    protected override void Init()
    {
        SkillCoolDownTime = 0.5f;
        RequiredMp = 10;

    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("Right Skill");
        Root = transform.root;

        Vector3 dir = Root.forward;
        Root.GetComponent<Animator>().CrossFade("ATTACK2", 0.1f, -1, 0);
        dir = new Vector3(dir.x, 0, dir.z);
        
        yield return new WaitForSeconds(0.3f);
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.RightAttackEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);
        //Managers.Sound.Play("swing1");

        ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y + 0.5f, ps.transform.position.z);

        skillObj.localScale = new Vector3(0.7f, 0.7f, 0.7f);

        skillObj.position = Root.transform.position;
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation.SetLookRotation(dir);

        float moveDuration = 1.5f; // ����ü�� ���ư��� �ð��� �����մϴ�.
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
