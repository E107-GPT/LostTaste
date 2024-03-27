using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 점프하기 직전에 캐릭터 공격 및 먼지 이펙트
public class MonsterKingJumpStartPattern : Pattern
{
    private MonsterKingController _controller;
    private Coroutine _coroutine;

    protected override void Init()
    {
        PatternName = "KingJumpStartEffect";
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
        if (_controller != null ) _coroutine = null;
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.transform;

        Transform _cylinderLoc = Managers.Resource.Instantiate("Patterns/KingJumpStartCollider").transform;
        _cylinderLoc.GetComponent<PatternObject>().Init(Root, attackDamage, _seq);

        _cylinderLoc.rotation = Quaternion.identity;
        
        Vector3 rootUp = Root.TransformDirection(Vector3.up * 0.5f);
        _cylinderLoc.position = Root.position + rootUp;
        ParticleSystem _particle = Managers.Effect.Play(Define.Effect.KingJumpStartEffect, _cylinderLoc);

        yield return new WaitForSeconds(0.1f);
        Managers.Resource.Destroy(_cylinderLoc.gameObject);

        yield return new WaitForSeconds(1.0f);
        Managers.Effect.Stop(_particle);
    }

    public override void SetCollider(int attackDamage)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CheckPatternObject(attackDamage));
        }
    }
}
