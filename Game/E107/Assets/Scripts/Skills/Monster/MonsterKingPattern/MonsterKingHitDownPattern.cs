using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

// Execute에서 Collider가 필요한 순간에만 사용
// ex. 도끼를 내려찍고 폭발 이펙트가 생긴 시점 -> 공격 판정
public class MonsterKingHitDownPattern : Pattern
{
    private MonsterKingController _controller;
    private ParticleSystem _particle;
    private Coroutine _coroutine;
    private Transform _cylinderLoc;

    protected override void Init()
    {
        PatternName = "KingHitDownEndEffect";
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

        // pattern obj 위치
        Vector3 rootForward = Root.TransformDirection(Vector3.forward * 5.0f);
        _cylinderLoc.position = Root.position + rootForward;
        Vector3 tempCylinder = _cylinderLoc.position;
        tempCylinder.y += 2.0f;
        _cylinderLoc.position = tempCylinder;

        _particle = Managers.Effect.Play(Define.Effect.KingHitDownEndEffect, _cylinderLoc);
        Managers.Sound.Play("Monster/KingHitDownEndEffect", Define.Sound.Effect);
    }

    public override void SetCollider(int attackDamage)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CheckPatternObject(attackDamage));
        }
    }
}
