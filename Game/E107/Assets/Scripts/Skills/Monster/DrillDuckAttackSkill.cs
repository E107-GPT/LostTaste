using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckAttackSkill : Skill
{
    protected override void Init()
    {
        SkillCoolDownTime = 1;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("DrillDuck Attack");
        yield return new WaitForSeconds(0.3f);

        // SkillObject에서 관리
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        // _particleSystem.GetComponent<ParticleSystem>().Play();
        // Managers.Sound.Play("swing1");

        skillObj.localScale = new Vector3(2.5f, 3.0f, _attackRange);    // 5.0f
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange / 4));
        skillObj.position = new Vector3(skillObj.position.x + 1.0f, Root.position.y + 1.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);
        // _particleSystem.GetComponent<ParticleSystem>().Stop();
    }
}
