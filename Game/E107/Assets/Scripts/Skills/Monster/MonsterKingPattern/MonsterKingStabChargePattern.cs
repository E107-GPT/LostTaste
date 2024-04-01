using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MonsterKingStabChargePattern : Pattern
{
    private MonsterKingController _controller;
    private Transform _leftArm;                 // particle 위치를 잡기 위함

    protected override void Init()
    {
        PatternName = "KingStabChargeEffect";
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.transform;
        _leftArm = _controller.LeftArm.transform;

        ParticleSystem _particle = Managers.Effect.Play(Define.Effect.KingStabChargeEffect, _leftArm);

        yield return new WaitForSeconds(1.5f);

        Managers.Effect.Stop(_particle);
    }

    public override void SetCollider(int attackDamage)
    {
        StartCoroutine(CheckPatternObject(attackDamage));
    }
}
