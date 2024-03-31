using System.Collections;
using UnityEngine;

public class CrocodileSwordSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    public Vector3 Scale = new Vector3(5.0f, 2.0f, 5.0f);

    [field: SerializeField]
    public float Duration { get; set; }

    [field: SerializeField]
    public float StartVelocity { get; set; }

    [field: SerializeField]
    public float Acceleration { get; set; }

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {

        GameObject player = transform.root.gameObject;
        ParticleSystem tail = player.transform.GetComponentInChildren<ParticleSystem>();

        tail.Play();

        Vector3 dir = new Vector3(player.transform.forward.x, 0, player.transform.forward.z);
        player.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);

        yield return new WaitForSeconds(0.3f);

        tail.Stop();
        
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.CrocodileSwordEffect, player.transform);
        ps.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        GameObject skillObj = Managers.Resource.Instantiate("Skills/SkillObject");
        skillObj.GetComponent<SkillObject>().SetUp(player.transform, Damage, _seq);

        ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y + 0.5f, ps.transform.position.z);

        skillObj.transform.localScale = Scale;

        skillObj.transform.position = Root.transform.position;
        skillObj.transform.position = new Vector3(skillObj.transform.position.x, player.transform.position.y + 0.5f, skillObj.transform.position.z);
        skillObj.transform.rotation.SetLookRotation(dir);

        float timer = 0; // 타이머 초기화
        float vel = StartVelocity;

        while (timer < Duration)
        {
            // 투사체와 파티클 시스템을 앞으로 움직입니다.
            Vector3 moveStep = dir * vel * Time.deltaTime;
            skillObj.transform.position += moveStep;
            ps.transform.position += moveStep;

            vel += Acceleration * Time.deltaTime;   // 속도를 변화시킵니다
            timer += Time.deltaTime; // 타이머를 업데이트합니다.
            yield return null; // 다음 프레임까지 대기합니다.
        }

        tail.Stop();

        Managers.Resource.Destroy(skillObj.transform.gameObject);
        Managers.Effect.Stop(ps);
    }
}
