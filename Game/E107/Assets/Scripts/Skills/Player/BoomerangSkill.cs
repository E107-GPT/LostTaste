using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangSkill : AttackSkill
{
    [field: SerializeField]
    public Vector3 Scale { get; set; }

    [SerializeField]
    private float StartVelocity = 10.0f;  // per seconds

    [SerializeField]
    private float Acceleration = -10.0f;   // per seconds^2

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();
        
        playerController.gameObject.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0, 0.7f);

        Vector3 dir = new Vector3(player.transform.forward.x, 0, player.transform.forward.z);

        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.BoomerangSkillEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        
        skillObj.GetComponent<SkillObject>().SetUp(player.transform, Damage, _seq);

        ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y + 0.5f, ps.transform.position.z);
        ps.transform.eulerAngles = Vector3.zero;
        skillObj.position = ps.transform.position;
        skillObj.localScale = Scale;

        float vel = StartVelocity;
        float prevVel = StartVelocity;
        while (vel > -StartVelocity)
        {
            // 투사체와 파티클 시스템을 앞으로 움직입니다.
            Vector3 moveStep = Time.deltaTime * vel * dir;
            skillObj.position += moveStep;
            ps.transform.position += moveStep;

            prevVel = vel;
            vel += Acceleration * Time.deltaTime;   // 속도를 변화시킵니다

            if (prevVel >= 0 && vel < 0)
            {
                Debug.Log("Boomerang Returning");
                Transform newSkillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
                newSkillObj.GetComponent<SkillObject>().SetUp(player.transform, Damage, _seq + 1);
                skillObj.position = ps.transform.position;
                skillObj.localScale = Scale;

                Destroy(skillObj.gameObject);
                skillObj = newSkillObj;
            }

            yield return null; // 다음 프레임까지 대기합니다.
        }
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
