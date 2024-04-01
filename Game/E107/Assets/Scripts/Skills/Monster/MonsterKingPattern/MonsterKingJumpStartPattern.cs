using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 점프하기 직전에 캐릭터 공격 및 먼지 이펙트
public class MonsterKingJumpStartPattern : Pattern
{
    private MonsterKingController _controller;

    protected override void Init()
    {
        PatternName = "KingJumpStartEffect";
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
        
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        yield return new WaitForSeconds(0.1f);
        Root = _controller.transform;

        Transform _cylinderLoc = Managers.Resource.Instantiate("Patterns/KingJumpStartCollider").transform;
        _cylinderLoc.GetComponent<PatternObject>().Init(Root, attackDamage, _seq);

        _cylinderLoc.rotation = Quaternion.identity;
        
        Vector3 rootUp = Root.TransformDirection(Vector3.up * 0.5f);
        _cylinderLoc.position = Root.position + rootUp;
        ParticleSystem _particle = Managers.Effect.Play(Define.Effect.KingJumpStartEffect, _cylinderLoc);
        Managers.Sound.Play("Monster/KingJumpStartEffect", Define.Sound.Effect);

        yield return new WaitForSeconds(0.2f);
        Managers.Resource.Destroy(_cylinderLoc.gameObject);

        yield return new WaitForSeconds(_particle.main.duration);
        Managers.Effect.Stop(_particle);
    }

    public override void SetCollider(int attackDamage)
    {
        StartCoroutine(CheckPatternObject(attackDamage));
    }

    public override void SetCollider()
    {
        throw new System.NotImplementedException();
    }
}
