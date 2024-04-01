using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaClassSkill : Skill, IAttackSkill
{
    public static event Action<bool> OnNinjaSkillCast; // 스킬 시전 여부 전달을 위한 이벤트 정의

    [field: SerializeField]
    public int Damage { get; set; }

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        OnNinjaSkillCast?.Invoke(true); // 스킬 시전 성공하면 이벤트 발생

        Root = transform.root;
        Vector3 dir = Root.forward;

        PlayerController _playerController = gameObject.GetComponent<PlayerController>();

        //Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.NinjaClassSkillStartEffect, Root);
        ps.transform.parent = _playerController._righthand.transform;
        ps.transform.localPosition = new Vector3();

        yield return null;  // 쿨타임 인식을 위한 1틱 대기
        _playerController.StateMachine.ChangeState(new IdleState(_playerController));


        yield return new WaitForSeconds(0.8f);
        Managers.Effect.Stop(ps);

        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq);

        skillObj.localScale = new Vector3(0.7f, 0.7f, 0.7f);

        skillObj.position = Root.transform.position;
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation.SetLookRotation(dir);


        ParticleSystem finishEffect1 = Managers.Effect.Play(Define.Effect.NinjaClassSkillFinishEffect, Root);
        ps.transform.position = _playerController._righthand.transform.position;
        

        ParticleSystem finishEffect2 = Managers.Effect.Play(Define.Effect.GalaxyZzzSkillEffect, Root);
        ps.transform.position = _playerController._righthand.transform.position;


        yield return new WaitForSeconds(0.2f);
        Managers.Resource.Destroy(skillObj.gameObject);

        yield return new WaitForSeconds(0.5f);
        Managers.Effect.Stop(finishEffect1);
        Managers.Effect.Stop(finishEffect2);




    }
}
