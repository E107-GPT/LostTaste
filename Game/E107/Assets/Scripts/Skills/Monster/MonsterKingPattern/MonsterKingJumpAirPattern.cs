using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controller���� ����
// DectPlayer�� ��ġ�� �����ؾ���
public class MonsterKingJumpAirPattern : Pattern
{
    private MonsterKingController _controller;
    private Coroutine _coroutine;
    private ParticleSystem _particle;

    protected override void Init()
    {
        PatternName = "KingJumpAirEffect";
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
        if (_controller != null)
        {
            _coroutine = null;
        }
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.DetectPlayer.transform;
        yield return null;

        _particle = Managers.Effect.Play(Define.Effect.KingJumpAirEffect, Root);
    }

    public override void SetCollider(int attackDamage)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CheckPatternObject(attackDamage));
        }
    }
}
