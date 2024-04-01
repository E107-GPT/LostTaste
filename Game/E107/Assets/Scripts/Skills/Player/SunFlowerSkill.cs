using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }
    [field: SerializeField]
    private Vector3 Scale = new Vector3(10f, 0.5f, 10f);

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;

        Transform dir = Root;
        Root.GetComponent<Animator>().CrossFade("ATTACK2", 0.1f, -1, 0);
        //dir = new Vector3(dir.x, 0, dir.z);
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.SunFallEffect, dir);

        ParticleSystem target = Managers.Effect.Play(Define.Effect.MagicFieldWhite, dir);

        ps.transform.position = new Vector3(dir.position.x, dir.position.y +10f, dir.position.z);


        float moveDuration = 1.5f; // 투사체가 날아가는 시간을 설정합니다.
        float timer = 0; // 타이머 초기화
        float speed = 7.0f; // 투사체의 속도를 설정합니다.

        while (timer < moveDuration)
        {
            ps.transform.Translate(Vector3.back * Time.deltaTime * speed);

            timer += Time.deltaTime; // 타이머를 업데이트합니다.
            yield return null; // 다음 프레임까지 대기합니다.
        }
        Managers.Sound.Play("Monster/KingHitDownAfterEffect");
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(dir, Damage, _seq);
        
        skillObj.localScale = Scale;
        skillObj.position = new Vector3(ps.transform.position.x, dir.position.y, ps.transform.position.z);
        Managers.Effect.Stop(ps);

        Managers.Effect.Stop(target);
        ParticleSystem explosion = Managers.Effect.Play(Define.Effect.GroundSlamRed, skillObj);
        ParticleSystem ExplosionDecalFire = Managers.Effect.Play(Define.Effect.ExplosionDecalFire, skillObj);
        yield return new WaitForSeconds(0.1f);
        Managers.Resource.Destroy(skillObj.gameObject);
        yield return new WaitForSeconds(1f);
        Managers.Effect.Stop(explosion);

        yield return new WaitForSeconds(1f);
        Managers.Effect.Stop(ExplosionDecalFire);
    }
}
