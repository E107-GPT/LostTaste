using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    private Vector3 Scale = new Vector3(0.5f, 2.0f, 1.5f);

    public Define.Effect stabEffect = Define.Effect.StabEffect;
    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;
        PlayerController _playerController = Root.GetComponent<PlayerController>();
        Root.GetComponent<Animator>().CrossFade("Stab", 0.1f, -1, 0, 0.7f);
        yield return new WaitForSeconds(0.2f);

        ParticleSystem ps = Managers.Effect.Play(stabEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq);

        Vector3 offset = Root.forward.normalized * 1.5f;
        Vector3 rootUp = Root.TransformDirection(Vector3.up * 0.5f);
        ps.transform.position += (offset + rootUp);
        skillObj.position += offset;

        skillObj.localScale = Scale;
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (Scale.z / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.1f);
        _playerController.StateMachine.ChangeState(new IdleState(_playerController));
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);



    }
}
