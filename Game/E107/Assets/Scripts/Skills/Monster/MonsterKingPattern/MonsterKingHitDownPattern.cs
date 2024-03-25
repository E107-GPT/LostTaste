using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Execute에서 Collider가 필요한 순간에만 사용
// ex. 도끼를 내려찍고 폭발 이펙트가 생긴 시점 -> 공격 판정
public class MonsterKingHitDownPattern : Pattern
{
    private MonsterKingController _controller;
    private ParticleSystem _ps;
    private Coroutine _coroutine;
    private Transform[] _colliders;
    private MeshCollider _meshCol;

    protected override void Init()
    {
        PatternName = "HitDownEndEffect";
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        if (_colliders != null)
        {
            foreach (Transform col in _colliders)
            {
                if (col == null || col.gameObject == null) continue;

                _meshCol = col.GetComponent<MeshCollider>();
                if (_meshCol != null)
                {
                    col.GetComponent<MeshCollider>().enabled = false;
                    _meshCol = null;
                }
            }
            
        }
        Debug.Log("DeActiveCollider");
    }

    IEnumerator CheckParticle(int attackDamage)
    {
        while (true)
        {
            _ps = _controller.Particle;
            
            if (_ps != null && _ps.name.Equals(PatternName))
            {
                _colliders = _ps.GetComponentsInChildren<Transform>();
                foreach (Transform col in _colliders)
                {
                    _meshCol = col.GetComponent<MeshCollider>();
                    if (_meshCol == null) continue;

                    Debug.Log("_meshCol: " + _meshCol.name);
                    col.GetComponent<PatternObject>().Init(_controller.transform, attackDamage, _seq);
                    _meshCol.enabled = true;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void SetCollider(int attackDamage)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CheckParticle(attackDamage));
        }
    }
}
