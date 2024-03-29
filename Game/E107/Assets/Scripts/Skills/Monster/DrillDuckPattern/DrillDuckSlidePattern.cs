using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrillDuckSlidePattern : Pattern
{
    private DrillDuckController _controller;
    private ParticleSystem _particleSystem;

    protected override void Init()
    {
        PatternName = "Slide";
        _controller = GetComponent<DrillDuckController>();

        SkillObj = Managers.Resource.Instantiate("Skills/SkillObject");
        SkillObj.transform.parent = _controller.transform;
        SkillObj.SetActive(false);
    }

    public override void SetCollider(int attackDamage)
    {
        // SkillObject에서 관리
        Root = transform.root;
        SkillObj.SetActive(true);

        _particleSystem = Managers.Effect.Play(Define.Effect.DrillDuckSlideEffect, Root);
        _particleSystem.transform.parent = _controller.transform;
        _particleSystem.transform.localScale = new Vector3(10.0f, 2.5f, 5.0f);
        _particleSystem.transform.localPosition = new Vector3(0, 1.5f, -1.5f);

        
        SkillObj.GetComponent<SkillObject>().SetUp(Root, attackDamage, _seq);           
        SkillObj.transform.localScale = new Vector3(1.5f, 5.0f, 1.5f);
        SkillObj.transform.position = Root.transform.TransformPoint(Vector3.forward * 0.8f);
        SkillObj.transform.position = new Vector3(SkillObj.transform.position.x, Root.position.y + 0.5f, SkillObj.transform.position.z);
        SkillObj.transform.rotation = Root.rotation;
    }

    public override void DeActiveCollider()
    {
        Managers.Resource.Destroy(_particleSystem.gameObject);
        SkillObj.SetActive(false);
    }
}
