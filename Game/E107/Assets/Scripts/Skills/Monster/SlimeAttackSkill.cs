using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Slime�� ������ �� �ִ� ���� �� �ϳ�
public class SlimeAttackSkill : Skill
{
    // private GameObject _particleSystem;

    protected override void Init()
    {
        // Root: Skill�� Start���� ����
        // Effect: Skill�� Start���� ����
        SkillCoolDownTime = 0;
        RequiredMp = 0;
        // _particleSystem = Managers.Resource.Instantiate("Effects/SwordSlashThinBlue", Effect.transform);
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("Normal Attack");
        yield return new WaitForSeconds(0.3f);

        // SkillObject���� ����
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        // _particleSystem.GetComponent<ParticleSystem>().Play();
        // Managers.Sound.Play("swing1");

        skillObj.localScale = new Vector3(1.0f, 5.0f, 1.1f);
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);
        // _particleSystem.GetComponent<ParticleSystem>().Stop();
    }
}
