using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

// Execute에서 Collider가 필요한 순간에만 사용
// ex. 도끼를 내려찍고 폭발 이펙트가 생긴 시점 -> 공격 판정
public class MonsterKingHitDownPattern : Pattern
{
    private MonsterKingController _controller;
    private ParticleSystem _ps;
    private Coroutine _coroutine;
    private Transform _cylinderLoc;

    private Transform[] _colliders;
    private MeshCollider _meshCol;

    protected override void Init()
    {
        PatternName = "HitDownEndEffect";
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            if (_ps != null) Managers.Effect.Stop(_ps);
            if (_cylinderLoc != null) Managers.Resource.Destroy(_cylinderLoc.gameObject);
        }
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.transform;
        yield return new WaitForSeconds(0.1f);

        _cylinderLoc = Managers.Resource.Instantiate("Patterns/HitDownCollider").transform;
        _cylinderLoc.GetComponent<PatternObject>().Init(Root, attackDamage, _seq);
        _cylinderLoc.position = Root.position;
        _cylinderLoc.rotation = Quaternion.identity;

        Vector3 rootForward = Root.TransformDirection(Vector3.forward * 4.0f);
        _cylinderLoc.position = Root.position + rootForward;
        _ps = Managers.Effect.Play(Define.Effect.HitDownEndEffect, _cylinderLoc);
    }

    public override void SetCollider(int attackDamage)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CheckPatternObject(attackDamage));
        }
    }
}
