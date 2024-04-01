using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingSlashChargePattern : Pattern
{
    private ParticleSystem _particle;
    private MonsterKingController _controller;

    protected override void Init()
    {
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
        if (_particle != null)
        {
            Managers.Effect.Stop(_particle);
            _particle = null;
        }
    }

    public override void SetCollider()
    {
        Root = _controller.transform;

        if ( _particle == null )
        {
            _particle = Managers.Effect.Play(Define.Effect.KingSlashStartEffect, Root);
            _particle.Play();
        }
    }

    public override void SetCollider(int attackDamage)
    {
        throw new System.NotImplementedException();
    }
}
