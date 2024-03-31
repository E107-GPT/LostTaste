using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MonsterKingHitDownAfterPattern : Pattern
{
    private MonsterKingController _controller;
    private Coroutine _coroutine;
    private Transform _donutLoc;

    private int _colliderCnt = 14;
    private float _radius = 8.0f;


    protected override void Init()
    {
        PatternName = "KingHitDownAfterEffect";
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
        if (_coroutine != null)
        {
            _coroutine = null;
        }
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.transform;               // 내려찍은 상태를 가져와야함
        
        _donutLoc = Managers.Resource.Instantiate("Patterns/KingDonutCenter").transform;

        Vector3 rootForward = Root.TransformDirection(Vector3.forward * 5.0f);
        _donutLoc.position = Root.position + rootForward;
        Vector3 tempCenter = _donutLoc.position;
        tempCenter.y += 2.0f;
        _donutLoc.position = tempCenter;

        for (int i = 0; i < _colliderCnt; i++)
        {
            float angle = i * Mathf.PI * 2 / _colliderCnt;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _radius;
            GameObject go = Managers.Resource.Instantiate("Patterns/KingDonutCollider");
            go.GetComponent<PatternObject>().Init(Root, attackDamage, _seq);
            go.transform.parent = _donutLoc;
            go.transform.localPosition = pos;
        }

        // 소리 재생
        Managers.Sound.Play("Monster/KingHitDownAfterEffect", Define.Sound.Effect);

        yield return new WaitForSeconds(0.3f);
        // hit box 안에 effect가 존재
        Managers.Resource.Destroy(_donutLoc.gameObject);
    }

    public override void SetCollider(int attackDamage)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CheckPatternObject(attackDamage));
        }
    }
}
