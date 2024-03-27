using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangSkill : Skill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    public Vector3 Scale { get; set; }

    [SerializeField]
    private float StartVelocity = 10.0f;  // per seconds

    [SerializeField]
    private float Acceleration = -10.0f;   // per seconds^2

    protected override void Init()
    {
        SkillCoolDownTime = 8.0f;
        RequiredMp = 10;
    }

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
            // ����ü�� ��ƼŬ �ý����� ������ �����Դϴ�.
            Vector3 moveStep = Time.deltaTime * vel * dir;
            skillObj.position += moveStep;
            ps.transform.position += moveStep;

            prevVel = vel;
            vel += Acceleration * Time.deltaTime;   // �ӵ��� ��ȭ��ŵ�ϴ�

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

            yield return null; // ���� �����ӱ��� ����մϴ�.
        }
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
