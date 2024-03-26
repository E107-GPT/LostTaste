using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MonsterKingStabChargePattern : Pattern
{
    private MonsterKingController _controller;
    private Transform _leftArm;                 // particle 위치를 잡기 위함
    private ParticleSystem _particle;
    private Coroutine _coroutine;

    protected override void Init()
    {
        PatternName = "KingStabChargeEffect";
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            if (_particle != null) Managers.Effect.Stop(_particle);
        }
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.transform;
        _leftArm = _controller.LeftArm.transform;

        _particle = Managers.Effect.Play(Define.Effect.KingStabChargeEffect, _leftArm);

        yield return null;
    }

    public override void SetCollider(int attackDamage)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CheckPatternObject(attackDamage));
        }
    }
}
