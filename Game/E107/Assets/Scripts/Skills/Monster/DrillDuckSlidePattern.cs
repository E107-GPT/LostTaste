using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 쿨타임은 State에서 관리
public class DrillDuckSlidePattern : Pattern
{
    protected override void Init()
    {
        SkillName = "Slide";
        SkillCoolDownTime =  10;
    }

    public override void SetCollider(int _attackDamage, float _attackRange)
    {
        Debug.Log("Slide Attack - DrillDuck");

        // SkillObject에서 관리
        Debug.Log("Root: " + Root);
        Root = transform.root;
        if (Root != null)
        {
            SkillObj.GetComponent<SkillObject>().SetUp(transform.root, _attackDamage, _seq);        // 여기서 계속 Null Error가 발생
            SkillObj.transform.localScale = new Vector3(4.0f, 5.0f, 5.0f);
            SkillObj.transform.position = Root.transform.TransformPoint(Vector3.forward);
            SkillObj.transform.position = new Vector3(SkillObj.transform.position.x, Root.position.y + 0.5f, SkillObj.transform.position.z);
            SkillObj.transform.rotation = Root.rotation;
            SkillObj.SetActive(true);
        }
        
        // Managers.Resource.Destroy(skillObj.gameObject);

        
    }

    public override void DeActiveCollider()
    {
        SkillObj.SetActive(false);
    }

    //protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    //{
    //    yield return null;
    //    //if (Time.time - LastCastTime >= SkillCoolDownTime)
    //    //{
    //    //    Debug.Log("Slide Attack - DrillDuck");
    //    //    yield return new WaitForSeconds(0.1f);

    //    //    // SkillObject에서 관리
    //    //    Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
    //    //    skillObj.GetComponent<SkillObject>().SetUp(Root, _attackDamage, _seq);

    //    //    skillObj.localScale = new Vector3(4.0f, 5.0f, 5.0f);
    //    //    skillObj.position = Root.transform.TransformPoint(Vector3.forward);
    //    //    skillObj.position = new Vector3(skillObj.transform.position.x, Root.position.y + 0.5f, skillObj.transform.position.z);
    //    //    skillObj.rotation = Root.rotation;

    //    //    yield return new WaitForSeconds(10f);
    //    //    Managers.Resource.Destroy(skillObj.gameObject);

    //    //    LastCastTime = Time.time;
    //    //}
    //}
}
