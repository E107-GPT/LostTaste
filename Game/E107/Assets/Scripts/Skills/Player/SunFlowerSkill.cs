using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }
    [field: SerializeField]
    private Vector3 Scale = new Vector3(1.0f, 1.0f, 0.5f);

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;

        Vector3 dir = Root.forward;
        Root.GetComponent<Animator>().CrossFade("ATTACK2", 0.1f, -1, 0);
        dir = new Vector3(dir.x, 0, dir.z);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.SunFallEffect, Root);

        ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y +10f, ps.transform.position.z);


        float moveDuration = 1.0f; // 투사체가 날아가는 시간을 설정합니다.
        float timer = 0; // 타이머 초기화
        float speed = 10.0f; // 투사체의 속도를 설정합니다.

        while (timer < moveDuration)
        {
            ps.transform.Translate(Vector3.forward * Time.deltaTime * speed);

            timer += Time.deltaTime; // 타이머를 업데이트합니다.
            yield return null; // 다음 프레임까지 대기합니다.
        }
        Managers.Sound.Play("Monster/KingHitDownAfterEffect");
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq);
        skillObj.localScale = new Vector3(3f, 0.5f, 3f);

        skillObj.position = ps.transform.position;
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y, skillObj.position.z);
        skillObj.rotation.SetLookRotation(dir);

        ParticleSystem explosion = Managers.Effect.Play(Define.Effect.ExplosionSunFallEffect, skillObj);
        yield return new WaitForSeconds(0.5f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
        Managers.Effect.Stop(explosion);

    }
}
