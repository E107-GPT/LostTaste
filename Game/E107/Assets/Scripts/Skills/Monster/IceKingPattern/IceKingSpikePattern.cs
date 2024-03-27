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
    private Coroutine _IceSpike;
    private Transform _sectorLoc;

    protected override void Init()
    {
        PatternName = "Spike";
        _controller = GetComponent<IceKingController>();
    }

    IEnumerator IceSpike(int attackDamage)
    {
        Debug.Log("IceKing Attack");

        yield return new WaitForSeconds(0.5f);
        Root = _controller.transform;
        Vector3 dir = Root.forward;

        // SkillObject에서 관리
        _sectorLoc = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        _sectorLoc.GetComponent<SkillObject>().SetUp(Root, attackDamage, _seq);

        // Managers.Sound.Play("swing1");

        _sectorLoc.localScale = new Vector3(9.0f, 4.0f, 9.0f);    // 5.0f
        _sectorLoc.position = Root.transform.TransformPoint(Vector3.forward * (5.2f));
        _sectorLoc.position = new Vector3(_sectorLoc.position.x, Root.position.y, _sectorLoc.position.z);
        _sectorLoc.rotation = Root.rotation;

        float moveDuration = 1.5f;
        float timer = 0;
        float speed = 13.0f;

        _particleSystem = Managers.Effect.Play(Define.Effect.IceKingSpikeEffect, Root);
        //_particleSystem.transform.parent = skillObj.transform;
        //ps.transform.position = new Vector3(skillObj.position.x - 5.0f, skillObj.position.y, skillObj.position.z - 0.9f);
        _particleSystem.transform.position = _sectorLoc.transform.position /*+ skillObj.transform.forward * 6.0f*/;
        //_particleSystem.transform.position = skillObj.transform.position + skillObj.transform.right * 3.0f;
        //ps.position = skillObj.position / 10.0f;

        yield return new WaitForSeconds(1.0f);

        Managers.Resource.Destroy(_sectorLoc.gameObject);
        Managers.Effect.Stop(_particleSystem);
    }

    public override void DeActiveCollider()
    {
        if (_coroutine != null)
        {
            // wait for seconds로 없애는 타이밍을 맞추기 힘들다
            StopCoroutine(_coroutine);
            _coroutine = null;
            if (_particleSystem != null) Managers.Effect.Stop(_particleSystem);
            if (_sectorLoc != null) Managers.Resource.Destroy(_sectorLoc.gameObject);

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

