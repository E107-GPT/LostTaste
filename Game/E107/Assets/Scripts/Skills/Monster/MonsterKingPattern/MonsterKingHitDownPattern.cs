using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MonsterKingHitDownPattern : Pattern
{
    private MonsterKingController _controller;
    private Transform _cylinderLoc;

    protected override void Init()
    {
        PatternName = "KingHitDownEndEffect";
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.transform;
        yield return new WaitForSeconds(0.1f);

        _cylinderLoc = Managers.Resource.Instantiate("Patterns/KingHitDownCollider").transform;
        _cylinderLoc.GetComponent<PatternObject>().Init(Root, attackDamage, _seq);
        _cylinderLoc.rotation = Quaternion.identity;

        Vector3 rootForward = Root.TransformDirection(Vector3.forward * 5.0f);
        _cylinderLoc.position = Root.position + rootForward;
        Vector3 tempCylinder = _cylinderLoc.position;
        tempCylinder.y += 2.0f;
        _cylinderLoc.position = tempCylinder;

        ParticleSystem _particle = Managers.Effect.Play(Define.Effect.KingHitDownEndEffect, _cylinderLoc);
        Managers.Sound.Play("Monster/KingHitDownEndEffect", Define.Sound.Effect);

        yield return new WaitForSeconds(0.2f);
        Managers.Resource.Destroy(_cylinderLoc.gameObject);

        yield return new WaitForSeconds(0.8f);
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
