using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKingAttackSkill : Skill
{
    private IceKingController _controller;


    protected override void Init()
    {
        SkillCoolDownTime = 3.0f;
        _controller = GetComponent<IceKingController>();
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("IceKing Attack");

        yield return new WaitForSeconds(0.5f);

        // SkillObject에서 관리
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.IceKingCleaveEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.transform.parent = _controller.transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        // Managers.Sound.Play("swing1");

        skillObj.localScale = new Vector3(1.0f, 3.0f, _attackRange / 1.7f);    // 5.0f
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange / 2.2f));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        ps.transform.parent = skillObj.transform;
        //ps.transform.position = new Vector3(skillObj.position.x - 5.0f, skillObj.position.y, skillObj.position.z - 0.9f);
        //ps.transform.position = skillObj.transform.position + skillObj.transform.right * 3.0f;
        ps.transform.position = skillObj.transform.position - skillObj.transform.forward * 6.0f;
        //ps.position = skillObj.position / 10.0f;

        yield return new WaitForSeconds(1.0f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
