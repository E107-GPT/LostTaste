using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MonsterKingHitDownPattern : Pattern
{
    private MonsterKingController _controller;

    private const int _colliderCnt = 14;
    private const float _radius = 8.0f;

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
        Vector3 afterPos = Root.position;
        yield return new WaitForSeconds(0.1f);

        Transform _cylinderLoc = Managers.Resource.Instantiate("Patterns/KingHitDownCollider").transform;
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
        
        // Pattern After

        Transform _donutLoc = Managers.Resource.Instantiate("Patterns/KingDonutCenter").transform;
        ParticleSystem[] particles = new ParticleSystem[_colliderCnt];

        _donutLoc.position = afterPos + rootForward;
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

            particles[i] = Managers.Effect.Play(Define.Effect.KingHitDownAfterEffect, go.transform);
        }

        // effect를 생성 

        // 소리 재생
        Managers.Sound.Play("Monster/KingHitDownAfterEffect", Define.Sound.Effect);

        yield return new WaitForSeconds(0.2f);
        // hit box 안에 effect가 존재
        Managers.Resource.Destroy(_donutLoc.gameObject);

        // effect가 끝나는 시간까지 대기하다가 없앰
        yield return new WaitForSeconds(particles[0].main.duration);
        foreach (ParticleSystem ps in  particles)
        {
            Managers.Effect.Stop(ps);
        }
        particles = null;
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
