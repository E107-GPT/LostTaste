using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongSwingSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    private Vector3 Scale = new Vector3(5.0f, 2.0f, 5.0f);

    public Define.Effect swingEffect = Define.Effect.StrongSwingEffect;
    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;

        Debug.Log("Hero Sword Attack");
        Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0, 0.7f);
        //yield return new WaitForSeconds(0.3f);

        ParticleSystem ps = Managers.Effect.Play(swingEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq);

        Vector3 offset = Root.forward.normalized * 1.5f;
        ps.transform.position += offset;
        skillObj.position += offset;

        skillObj.localScale = Scale;
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (Scale.z / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;
        ps.transform.parent = skillObj;

        yield return new WaitForSeconds(0.3f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);



    }
}
