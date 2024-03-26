using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingStabPattern : Pattern
{
    private MonsterKingController _controller;
    private ParticleSystem _ps;
    private Coroutine _coroutine;
    private Transform[] _colliders;
    private MeshCollider _meshCol;

    protected override void Init()
    {
        PatternName = "Stab";
        _controller = GetComponent<MonsterKingController>();
    }
    public override void DeActiveCollider()
    {

    }



    public override void SetCollider(int attackDamage)
    {

    }
}
