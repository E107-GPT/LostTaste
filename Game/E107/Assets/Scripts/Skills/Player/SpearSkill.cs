using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    private Vector3 Scale = new Vector3(1.0f, 5.0f, 1.0f);

    public Define.Effect weaponEffect = Define.Effect.SpearSkillEffect;
    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;
        PlayerController _playerController = Root.GetComponent<PlayerController>();
        Debug.Log("Hero Sword Attack");
        Root.GetComponent<Animator>().CrossFade("SpearStab", 0.1f, -1, 0, 0.7f);
        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(weaponEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq);

        Vector3 offset = Root.forward.normalized * 1.5f;
        //ps.transform.position += offset;
        ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y + 0.5f, ps.transform.position.z);
        skillObj.position += offset;

        skillObj.localScale = Scale;
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (Scale.z / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        _playerController.isHolding = true;

        yield return new WaitForSeconds(0.3f);

        _playerController.StateMachine.ChangeState(new IdleState(_playerController));
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
        _playerController.isHolding = false;


    }
}
