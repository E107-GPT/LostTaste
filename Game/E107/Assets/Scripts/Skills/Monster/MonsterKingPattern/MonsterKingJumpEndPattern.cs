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
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.transform;

        _cylinderLoc = Managers.Resource.Instantiate("Patterns/KingJumpEndCollider").transform;
        _cylinderLoc.GetComponent<PatternObject>().Init(Root, attackDamage, _seq);
        _cylinderLoc.rotation = Quaternion.identity;

        Vector3 rootUp = Root.TransformDirection(Vector3.up);
        _cylinderLoc.position = Root.position + rootUp;

        _particle = Managers.Effect.Play(Define.Effect.KingJumpEndEffect, _cylinderLoc);
        Managers.Sound.Play("Monster/KingJumpEndEffect", Define.Sound.Effect);

        yield return new WaitForSeconds(0.1f);
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
