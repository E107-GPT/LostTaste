using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrillDuckAttackSkill : Skill
{
    private DrillDuckController _controller;

    protected override void Init()
    {
        SkillCoolDownTime = 0;
        _controller = GetComponent<DrillDuckController>();
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("DrillDuck Attack");
        yield return new WaitForSeconds(0.2f);

        // SkillObject에서 관리
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.transform.parent = _controller.transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        // _particleSystem.GetComponent<ParticleSystem>().Play();
        // Managers.Sound.Play("swing1");

        skillObj.localScale = new Vector3(1.0f, 3.0f, _attackRange / 2);    // 5.0f
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange/ 3));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 1.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.8f);
        Managers.Resource.Destroy(skillObj.gameObject);
        // _particleSystem.GetComponent<ParticleSystem>().Stop();
    }
}
