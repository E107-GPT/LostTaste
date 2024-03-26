using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingStabPattern : Pattern
{
    private MonsterKingController _controller;
    private ParticleSystem _particle;
    private Coroutine _coroutine;

    private Transform _leftArm;
    private Transform _stabLoc;

    protected override void Init()
    {
        PatternName = "KingStabEffect";
        _controller = GetComponent<MonsterKingController>();
    }
    public override void DeActiveCollider()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            if (_particle != null) Managers.Effect.Stop(_particle);
            if (_stabLoc != null) Managers.Resource.Destroy(_stabLoc.gameObject);
        }
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.transform;

        yield return new WaitForSeconds(0.1f);

        // hit box »ý¼º
        _leftArm = _controller.LeftArm.transform;
        _stabLoc = Managers.Resource.Instantiate("Patterns/StabCollider").transform;
        _stabLoc.GetComponent<PatternObject>().Init(Root, attackDamage, _seq);

        // ¿ÞÆÈ¿¡ hit box¿Í ÀÌÆåÆ® ºÎÂø
        _stabLoc.parent = _leftArm;
        _stabLoc.rotation = Root.rotation;
        _particle = Managers.Effect.Play(Define.Effect.KingStabEffect, _stabLoc);      // stabLocÀÌ¶û ÇÔ²² ÀÌµ¿


        // ÀÌÆåÆ®¿Í hit box¸¦ Âî¸£´Â ¾Ö´Ï¸ÞÀÌ¼Ç°ú ¸ÂÃç¼­ ÀÌµ¿
        //float speed = 50.0f;
        //while (true)
        //{
        //    _stabLoc.Translate(Root.forward * speed * Time.deltaTime, Space.Self);
        //    _particle.transform.Translate(Root.forward * speed * Time.deltaTime, Space.Self);

        //    yield return null;
        //}

    }

    public override void SetCollider(int attackDamage)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CheckPatternObject(attackDamage));
        }
    }
}
