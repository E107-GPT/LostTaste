using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAttackSkill : Skill
{
    // private GameObject _particleSystem;

    protected override void Init()
    {
        // Root: Skill의 Start에서 관리
        // Effect: Skill의 Start에서 관리
        SkillCoolDownTime = 0;
        // RequiredMp = 0;
        // _particleSystem = Managers.Resource.Instantiate("Effects/SwordSlashThinBlue", Effect.transform);
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("Slime Attack");
        yield return new WaitForSeconds(0.3f);

        // SkillObject에서 관리
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        // ParticleSystem ps = Managers.Effect.Play(Define.Effect.NormalAttackEffect, Root);
        // Managers.Sound.Play("swing1");

        skillObj.localScale = new Vector3(1.0f, 5.0f, _attackRange);    // 1.1f
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);
        // Managers.Effect.Stop(ps);
    }
}
