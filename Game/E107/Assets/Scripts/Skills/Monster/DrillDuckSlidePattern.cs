using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DrillDuckSlidePattern : Pattern
{
    private DrillDuckController _controller;
    private float _colliderRange;

    protected override void Init()
    {
        PatternName = "Slide";
        _colliderRange = 2.0f;
        _controller = GetComponent<DrillDuckController>();

        SkillObj = Managers.Resource.Instantiate("Skills/SkillObject");
        SkillObj.transform.parent = _controller.transform;
        SkillObj.SetActive(false);
    }

    public override void SetCollider(int attackDamage)
    {
        Debug.Log("Slide Attack - DrillDuck");

        // SkillObject에서 관리
        Root = transform.root;
        Debug.Log("Root: " + Root);
        SkillObj.SetActive(true);

        SlideEffect = Managers.Effect.Play(Define.Effect.DrillDuckSlideEffect, Root);
        SkillObj.GetComponent<SkillObject>().SetUp(Root, attackDamage, _seq);
        SkillObj.transform.localScale = new Vector3(2.0f, 5.0f, _colliderRange);
        SkillObj.transform.position = Root.transform.TransformPoint(Vector3.forward * 0.8f);
        SkillObj.transform.position = new Vector3(SkillObj.transform.position.x, Root.position.y + 0.5f, SkillObj.transform.position.z);
        SkillObj.transform.rotation = Root.rotation;
    }

    public override void DeActiveCollider()
    {
        Managers.Resource.Destroy(SlideEffect.gameObject);
        SkillObj.SetActive(false);
    }
}
