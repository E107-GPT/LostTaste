using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackSkill : Skill
{
    protected override void Init()
    {
        SkillCoolDownTime = 0;
        RequiredMp = 0;

        // attackDamage 세팅

        //_normalAttackObj = new GameObject($"{_controller.gameObject.name} Attack");
        //_normalAttackObj.AddComponent<SkillObject>();
        //_normalAttackCollider = _normalAttackObj.AddComponent<BoxCollider>();
        //_normalAttackCollider.isTrigger = true;
        //// 몬스터마다 변하는 부분
        //_normalAttackObj.transform.localScale = new Vector3(1.0f, 5.0f, 1.1f);

        //_normalAttackObj.SetActive(false);
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("Normal Attack");
        yield return new WaitForSeconds(0.3f);

        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);
        // Managers.Sound.Play("swing1");

        //    _normalAttackObj.transform.position = root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        //    _normalAttackObj.transform.position = new Vector3(_normalAttackObj.transform.position.x, root.position.y + 0.5f, _normalAttackObj.transform.position.z);
        //    //_normalAttackObj.transform.localScale = new Vector3(2.5f, 5, 3);
        //    _normalAttackObj.transform.rotation = root.rotation;

        skillObj.localScale = new Vector3(1.0f, 5.0f, 1.1f);
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        // skillObj.transform.localScale = new Vector3(2.5f, 5, 3);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);
    }
}
