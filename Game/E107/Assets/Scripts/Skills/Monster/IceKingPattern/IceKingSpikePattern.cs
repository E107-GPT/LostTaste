using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class IceKingSpikePattern : Pattern
{
    private IceKingController _controller;
    private ParticleSystem _particleSystem;
    private Coroutine _coroutine;
    private Transform _sectorLoc;

    protected override void Init()
    {
        PatternName = "Spike";
        _controller = GetComponent<IceKingController>();
    }

    IEnumerator IceSpike(int attackDamage)
    {
        //Debug.Log("HIT BOX START");
        //yield return new WaitForSeconds(0.5f);
        Root = _controller.transform;

        // SkillObject에서 관리
        _sectorLoc = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        _sectorLoc.GetComponent<SkillObject>().SetUp(Root, attackDamage, _seq);

        _sectorLoc.localScale = new Vector3(10.0f, 5.0f, 10.0f);    // 5.0f
        _sectorLoc.position = Root.transform.TransformPoint(Vector3.zero);
        _sectorLoc.position = new Vector3(_sectorLoc.position.x, Root.position.y, _sectorLoc.position.z);
        _sectorLoc.rotation = Root.rotation;

        _particleSystem = Managers.Effect.Play(Define.Effect.IceKingSpikeEffect, Root);
        _particleSystem.transform.position = _sectorLoc.transform.position;

        yield return new WaitForSeconds(0.2f);

        Managers.Resource.Destroy(_sectorLoc.gameObject);
        //Debug.Log("HIT BOX STOP");

        yield return new WaitForSeconds(1.6f);
        

        Managers.Effect.Stop(_particleSystem);
        //Debug.Log("EFFECT STOP");
    }

    public override void DeActiveCollider()
    {
        if (_coroutine != null)
        {
            // wait for seconds로 없애는 타이밍을 맞추기 힘들다
            //StopCoroutine(_coroutine);
            _coroutine = null;
            //if (_particleSystem != null) Managers.Effect.Stop(_particleSystem);
            //if (_sectorLoc != null) Managers.Resource.Destroy(_sectorLoc.gameObject);
            
        }
    }
    public override void SetCollider(int attackDamage)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(IceSpike(attackDamage));
        }
    }

}

