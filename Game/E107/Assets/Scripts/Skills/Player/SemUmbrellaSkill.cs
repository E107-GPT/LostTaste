using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemUmbrellaSkill : Skill
{
    private const int DAMAGE = 150;
    private readonly Vector3 SCALE = new Vector3(5.0f, 2.0f, 5.0f);

    protected override void Init()
    {
        SkillCoolDownTime = 1.0f;
        RequiredMp = 0;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        Debug.Log("SEM Umbrella Attack");
        yield return new WaitForSeconds(0.5f);

        playerController.StateMachine.ChangeState(new IdleState(playerController));

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.SemUmbrellaSkillEffect, player.transform);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, DAMAGE, _seq);

        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(4.0f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
