using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKingSpikePattern : Pattern
{
    private IceKingController _controller;
    private ParticleSystem _particleSystem;
    private Coroutine _IceSpike;

    protected override void Init()
    {
        PatternName = "Spike";
        _controller = GetComponent<IceKingController>();
    }

    IEnumerator IceSpike(int attackDamage)
    {
        Debug.Log("IceKing Attack");

        yield return new WaitForSeconds(2.5f);
        Root = _controller.transform;
        Vector3 dir = Root.forward;

        // SkillObject에서 관리
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, attackDamage, _seq);

        // Managers.Sound.Play("swing1");

        skillObj.localScale = new Vector3(9.0f, 4.0f, 9.0f);    // 5.0f
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (5.2f));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        float moveDuration = 1.5f;
        float timer = 0;
        float speed = 13.0f;

        _particleSystem = Managers.Effect.Play(Define.Effect.IceKingSpikeEffect, Root);
        //_particleSystem.transform.parent = skillObj.transform;
        //ps.transform.position = new Vector3(skillObj.position.x - 5.0f, skillObj.position.y, skillObj.position.z - 0.9f);
        _particleSystem.transform.position = skillObj.transform.position /*+ skillObj.transform.forward * 6.0f*/;
        //_particleSystem.transform.position = skillObj.transform.position + skillObj.transform.right * 3.0f;
        //ps.position = skillObj.position / 10.0f;

        yield return new WaitForSeconds(1.0f);

        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(_particleSystem);
    }

    public override void DeActiveCollider()
    {
        //StopCoroutine(_fireSword);
    }
    public override void SetCollider(int attackDamage)
    {
        _IceSpike = StartCoroutine(IceSpike(attackDamage));
    }

}
