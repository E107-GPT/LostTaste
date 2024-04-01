using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controller에서 관리
// DectPlayer의 위치를 갱신해야함
public class MonsterKingJumpAirPattern : Pattern
{
    //private MonsterKingController _controller;
    //private Coroutine _coroutine;
    //private ParticleSystem _particle;
    //private Transform _detectPlayerLoc;

    //private float _lastTime;

    protected override void Init()
    {
        //PatternName = "KingJumpAirEffect";
        //_controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
        //if (_controller != null)
        //{
        //    _coroutine = null;
        //}
    }

    //private IEnumerator CheckParticleAndChangeState(float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //}

    //IEnumerator CheckAnimationFinish()
    //{
    //    _detectPlayerLoc = _controller.DetectPlayer.transform;
    //    _particle = Managers.Effect.Play(Define.Effect.KingJumpAirEffect, _detectPlayerLoc);

    //    while (true)
    //    {
    //        _detectPlayerLoc = _controller.DetectPlayer.transform;
    //        _particle.transform.position = _detectPlayerLoc.position;
    //        yield return null;
    //    }
    //}

    public override void SetCollider(int attackDamage)
    {
        //if (_coroutine == null)
        //{
        //    _coroutine = StartCoroutine(CheckAnimationFinish());
        //}
    }

    public override void SetCollider()
    {
        throw new System.NotImplementedException();
    }
}
