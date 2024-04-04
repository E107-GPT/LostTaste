using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderHammerSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }
    [field: SerializeField]
    private Vector3 Scale = new Vector3(10f, 10.0f, 10f);

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;
        Vector3 dir = Root.forward;

        PlayerController _playerController = Root.GetComponent<PlayerController>();

        Root.GetComponent<Animator>().CrossFade("ATTACK2", 0.1f, -1, 0);
        yield return new WaitForSeconds(0.2f);
        _playerController.StateMachine.ChangeState(new IdleState(_playerController));
        ParticleSystem thunder = Managers.Effect.Play(Define.Effect.GalaxyZzzSkillEffect, Root);
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.ThunderHammerEffect, Root);
        thunder.transform.localScale = Scale;
        //ps.transform.localScale = Scale/2;

        Transform skillObj = Managers.Resource.Instantiate("Skills/CircleSkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq++);

        skillObj.localScale = Scale;

        skillObj.position = Root.transform.position;
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation.SetLookRotation(dir);

        skillObj.position += dir * 5.0f;
        ps.transform.position = skillObj.position;
        thunder.transform.position = skillObj.position;


        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);


        yield return new WaitForSeconds(0.6f);
        Managers.Effect.Stop(ps);
    }
}
