using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckSlidePattern : Skill
{
    protected override void Init()
    {
        SkillCoolDownTime =  10;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Debug.Log("Slide Attack - DrillDuck");
        yield return new WaitForSeconds(0.3f);

        // SkillObject에서 관리
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

        skillObj.localScale = new Vector3(4.0f, 5.0f, 5.0f);
        skillObj.position = Root.transform.TransformPoint(Vector3.forward);
        skillObj.position = new Vector3(skillObj.transform.position.x, Root.position.y + 0.5f, skillObj.transform.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);
    }
}
