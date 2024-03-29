using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonAttackSkill : Skill
{
    protected override void Init()
    {
        SkillCoolDownTime = 1.0f;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Root = transform.root;
        Vector3 dir = Root.forward;
        Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);
        dir = new Vector3(dir.x, 0, dir.z);

        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.DemonFireballEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y + 0.5f, ps.transform.position.z);
        // Managers.Sound.Play("swing1");

        skillObj.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        skillObj.position = Root.transform.position;
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation.SetLookRotation(dir);

        float moveDuration = 1.1f; // 투사체가 날아가는 시간을 설정합니다.
        float timer = 0; // 타이머 초기화
        float speed = 10.0f; // 투사체의 속도를 설정합니다.

        while (timer < moveDuration)
        {
            // 투사체와 파티클 시스템을 앞으로 움직입니다.
            Vector3 moveStep = dir * speed * Time.deltaTime;
            skillObj.position += moveStep;
            ps.transform.position += moveStep;

            timer += Time.deltaTime; // 타이머를 업데이트합니다.
            yield return null; // 다음 프레임까지 대기합니다.
        }

        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
