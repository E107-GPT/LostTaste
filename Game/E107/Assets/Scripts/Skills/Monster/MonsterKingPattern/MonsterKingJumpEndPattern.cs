using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingJumpEndPattern : Pattern
{
    private MonsterKingController _controller;
    private ParticleSystem _particle;
    private Coroutine _coroutine;
    private Transform _cylinderLoc;

    protected override void Init()
    {
        PatternName = "KingJumpEndEffect";
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            if (_particle != null) Managers.Effect.Stop(_particle);
            if (_cylinderLoc != null) Managers.Resource.Destroy(_cylinderLoc.gameObject);
        }
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.transform;
        yield return new WaitForSeconds(0.1f);

        _cylinderLoc = Managers.Resource.Instantiate("Patterns/KingHitDownCollider").transform;
        _cylinderLoc.GetComponent<PatternObject>().Init(Root, attackDamage, _seq);
        _cylinderLoc.rotation = Quaternion.identity;

        // pattern obj À§Ä¡
        Vector3 rootForward = Root.TransformDirection(Vector3.forward * 5.0f);
        _cylinderLoc.position = Root.position + rootForward;
        Vector3 tempCylinder = _cylinderLoc.position;
        tempCylinder.y += 2.0f;
        _cylinderLoc.position = tempCylinder;

        _particle = Managers.Effect.Play(Define.Effect.KingHitDownEndEffect, _cylinderLoc);
    }

    public override void SetCollider(int attackDamage)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CheckPatternObject(attackDamage));
        }
    }
}
