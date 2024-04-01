using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingHitDownChargePattern : Pattern
{
    private MonsterKingController _controller;
    private Transform _rightArm;

    protected override void Init()
    {
        PatternName = "KingHitDownStartEffect";
        _controller = GetComponent<MonsterKingController>();
    }
    public override void DeActiveCollider()
    {
    }

    IEnumerator CheckPatternObject()
    {
        _rightArm = _controller.RightArm.transform;

        ParticleSystem particle = Managers.Effect.Play(Define.Effect.KingHitDownStartEffect, _rightArm);
        particle.transform.parent = _rightArm;

        yield return new WaitForSeconds(1.5f);

        Managers.Effect.Stop(particle);
    }

    public override void SetCollider(int attackDamage)
    {
    }

    public override void SetCollider()
    {
        StartCoroutine(CheckPatternObject());
    }
}
