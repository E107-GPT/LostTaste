using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }

    protected override void Init()
    {
    }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;

        Vector3 dir = Root.forward;
        dir = new Vector3(dir.x, 0, dir.z);
        Root.GetComponent<Animator>().CrossFade("BOW", 0.1f, -1, 0);

        yield return new WaitForSeconds(0.3f);

        Transform skillObj = Managers.Resource.Instantiate("Skills/ArrowObject").transform;
        skillObj.GetComponent<ArrowObject>().Setup(transform, Damage, _seq, 20.0f);
        skillObj.rotation.SetLookRotation(dir);


    }


}
