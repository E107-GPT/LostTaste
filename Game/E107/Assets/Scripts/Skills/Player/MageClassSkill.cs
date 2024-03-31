using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageClassSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;
        Vector3 dir = Root.forward;


        PlayerController _playerController = gameObject.GetComponent<PlayerController>();

        ParticleSystem start = Managers.Effect.Play(Define.Effect.MageClassSkillAuraEffect, Root);
        start.transform.parent = Root;
        Root.GetComponent<Animator>().CrossFade("MageClassSkillAnim2", 0.2f, -1, 0);
        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.MageClassSkillEffect, Root);

        Transform skillObj = Managers.Resource.Instantiate("Skills/CircleSkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq++);

        skillObj.localScale = new Vector3(9.0f, 9.0f, 9.0f);

        skillObj.position = Root.transform.position;
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation.SetLookRotation(dir);

        skillObj.position += dir * 5.0f;
        ps.transform.position = skillObj.position;

        yield return new WaitForSeconds(4.0f);
        Managers.Effect.Stop(start);
        Managers.Resource.Destroy(skillObj.gameObject);
        _playerController.StateMachine.ChangeState(new IdleState(_playerController));


        yield return new WaitForSeconds(0.5f);
        Managers.Effect.Stop(ps);
        



    }
}
