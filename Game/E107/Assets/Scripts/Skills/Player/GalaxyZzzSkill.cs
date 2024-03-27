using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GalaxyZzzSkill : Skill
{
    [field: SerializeField]
    public int Damage { get; set; }

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        Debug.Log("GalaxyZZZ Attack");
        Root = RaycastGround();
        if (Root == null) yield break;

        Debug.Log("GalaxyZZZ Not breaked");
        GameObject player = transform.root.gameObject;

        player.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0, 1.5f);
        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.GalaxyZzzSkillEffect, Root);
        GameObject skillObj = Managers.Resource.Instantiate("Skills/SkillObject");
        skillObj.GetComponent<SkillObject>().SetUp(player.transform, Damage, _seq);

        skillObj.transform.position = Root.position;
        skillObj.transform.rotation = Root.rotation;

        yield return new WaitForSeconds(0.5f);
        
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }

    private Transform RaycastGround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        bool isHit = Physics.Raycast(ray, out raycastHit, 100.0f, LayerMask.GetMask("Ground"));
        
        if (!isHit) return null;
        return raycastHit.transform;
    }
}
