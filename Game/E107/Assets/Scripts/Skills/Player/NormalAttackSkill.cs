using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackSkill : Skill
{
    private GameObject _particleSystem;
    protected override void Init()
    {
        SkillCoolDownTime = 0;
        RequiredMp = 0;
        Debug.Log("123123");
        _particleSystem = Managers.Resource.Instantiate("Effects/SwordSlashThinBlue", Effect.transform);
        

    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("Normal Attack");
        yield return new WaitForSeconds(0.3f);
        _particleSystem.GetComponent<ParticleSystem>().Play();
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);
        Managers.Sound.Play("swing1");

        

        skillObj.localScale = new Vector3(1.0f, 1.0f, _attackRange);
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);
        _particleSystem.GetComponent<ParticleSystem>().Stop();


    }
}
