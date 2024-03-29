using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckBeforePatttern : Pattern
{
    private DrillDuckController _controller;
    private ParticleSystem _particleSystem;

    protected override void Init()
    {
        PatternName = "BeforeSlide";
        _controller = GetComponent<DrillDuckController>();
    }

    public override void DeActiveCollider()
    {
        Managers.Resource.Destroy(_particleSystem.gameObject);
    }

    public override void SetCollider(int attackDamage)
    {
        Root = _controller.transform;
        _particleSystem = Managers.Effect.Play(Define.Effect.DrillDuckBeforeEffect, Root);
        _particleSystem.transform.parent = _controller.transform;
    }
}
