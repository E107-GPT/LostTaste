using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBowSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    public float Duration { get; set; }

    [field: SerializeField]
    public float Velocity { get; set; }

    [field: SerializeField]
    public AudioClip AudioClip { get; set; }

    [field: SerializeField]
    public int ArrowCount { get; set; }

    [field: SerializeField]
    public float SpreadAngle { get; set; }  // 단위: 도

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        player.GetComponent<Animator>().CrossFade("BOW", 0.1f, -1, 0);
        gameObject.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);

        yield return new WaitForSeconds(0.3f);
        
        if (playerController.StateMachine.CurState is SkillState)
        {
            playerController.StateMachine.ChangeState(new IdleState(playerController));
        }
        gameObject.GetComponent<Animator>().CrossFade("IDLE", 0.1f, -1, 0);

        // 화살 코루틴 시작
        // 예를 들어 ArrowCount(n) = 4, SpreadAngle = 30이라면
        // median: 0, 1, ..., n-1의 중앙값 = (n-1)/2    ->   1.5
        //         i - median                           ->  -1.5 |  -0.5 |  0.5 |  1.5
        // offset: SpreadAngle * (i - median)           -> -45.0 | -15.0 | 15.0 | 45.0
        // 각 각도간 차이는 SpreadAngle = 30이고 평균은 0이다.
        float median = (float)(ArrowCount - 1) / 2;
        for (int i = 0; i < ArrowCount; i++)
        {
            float offset = SpreadAngle * (i - median);
            Vector3 dir = Quaternion.Euler(0, offset, 0) * player.transform.forward; // Y축을 축으로 offset만큼 회전
            dir = new Vector3(dir.x, 0, dir.z).normalized;

            Managers.Coroutine.Run(ArrowCoroutine(player, dir, i));
        }

        // 사운드
        for (int i = 0; i < ArrowCount; i++)
        {
            Managers.Sound.Play(AudioClip, Define.Sound.Effect, 1.5f, 0.3f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator ArrowCoroutine(GameObject player, Vector3 dir, int idx)
    {
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.BowSkillEffect, player.transform);
        ps.transform.position += Vector3.up * 0.5f;
        ps.transform.localEulerAngles = dir;

        GameObject skillObject = Managers.Resource.Instantiate("Skills/ArrowSkillObject");
        skillObject.GetComponent<ArrowSkillObject>().SetUp(player.transform, Damage, _seq + idx, 1, new ArrowSkillObject.OnBreakCallback(OnBreak));
        skillObject.transform.localEulerAngles = dir;

        float timer = 0;
        while (timer < Duration)
        {
            if (!skillObject.activeSelf) break; // 화살이 터졌다면 끝

            Vector3 step = dir * Velocity * Time.deltaTime;
            ps.transform.position += step;
            skillObject.transform.position = ps.transform.position;

            timer += Time.deltaTime;
            yield return null;
        }

        Managers.Effect.Stop(ps);
        Managers.Resource.Destroy(skillObject);
    }

    private IEnumerator OnBreak(GameObject player, Collider other)
    {
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.BowArrowBrokenEFfect, other.transform);

        yield return new WaitForSeconds(0.5f);

        Managers.Effect.Stop(ps);
    }
}
