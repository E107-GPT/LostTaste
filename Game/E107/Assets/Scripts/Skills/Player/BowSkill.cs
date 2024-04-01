using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BowSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    public float Duration { get; set; }

    [field: SerializeField]
    public float Velocity { get; set; }

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        Vector3 dir = player.transform.forward;
        dir = new Vector3(dir.x, 0, dir.z);

        player.GetComponent<Animator>().CrossFade("BOW", 0.1f, -1, 0);
        gameObject.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);

        yield return new WaitForSeconds(0.3f);
        playerController.StateMachine.ChangeState(new IdleState(playerController));
        gameObject.GetComponent<Animator>().CrossFade("IDLE", 0.1f, -1, 0);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.BowSkillEffect, player.transform);
        ps.transform.position += Vector3.up * 0.5f;
        ps.transform.rotation = player.transform.rotation;

        GameObject skillObject = Managers.Resource.Instantiate("Skills/ArrowSkillObject");
        skillObject.GetComponent<ArrowSkillObject>().SetUp(player.transform, Damage, _seq, -1);
        skillObject.transform.localEulerAngles = player.transform.forward;

        float timer = 0;
        Debug.Log("bow test | Duration: " + Duration);
        while (timer < Duration)
        {
            Debug.Log(timer);
            if (skillObject.IsDestroyed()) break;   // 화살이 터졌다면 끝

            Vector3 step = dir * Velocity * Time.deltaTime;
            ps.transform.position += step;
            skillObject.transform.position = ps.transform.position;

            timer += Time.deltaTime;
            yield return null;
        }

        Managers.Effect.Stop(ps);
        Managers.Resource.Destroy(skillObject);
    }
}
