using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClassSkill : Skill
{
    [field: SerializeField]
    public float Duration { get; set; }     // seconds

    [field: SerializeField]
    public float DetectionRadius { get; set; }

    [field: SerializeField]
    public float DrawingVelocity { get; set; }  // per seconds

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;

        Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);
        yield return new WaitForSeconds(0.3f);
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.WarriorClassSkillEffect, Root);
        ps.transform.parent = Root;

        float timer = 0; // 타이머 초기화
        while (timer < Duration)
        {
            // 투사체와 파티클 시스템을 앞으로 움직입니다.
            Collider[] monsters = Physics.OverlapSphere(Root.position, DetectionRadius, LayerMask.GetMask("Monster"));
            
            foreach (var monster in monsters)
            {
                Debug.Log(monster.name);
                var mon = monster.GetComponent<MonsterController>();
                mon.DetectPlayer = Root;
                Vector3 dir = (Root.transform.position - mon.transform.position).normalized;
                mon.Agent.Move(dir * Time.deltaTime * DrawingVelocity);
            }

            timer += Time.deltaTime; // 타이머를 업데이트합니다.
            yield return null; // 다음 프레임까지 대기합니다.
        }

        //yield return new WaitForSeconds(0.3f);
        Managers.Effect.Stop(ps);



    }
}
