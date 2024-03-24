using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileSwordPattern : Pattern
{
    private CrocodileController _controller;
    private ParticleSystem _particleSystem;
    private Coroutine _fireSword;

    protected override void Init()
    {
        PatternName = "Sword";
        _controller = GetComponent<CrocodileController>();
    }

    IEnumerator FireSword(int attackDamage)
    {
        Debug.Log("Sword - Crocodile");

        yield return new WaitForSeconds(1.2f);
        Root = _controller.transform;
        Vector3 dir = Root.forward;

        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, attackDamage, _seq);

        skillObj.localScale = new Vector3(10.0f, 5.0f, 3.0f);
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * 2.5f);
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 1.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        float moveDuration = 1.5f;
        float timer = 0;
        float speed = 13.0f;

        _particleSystem = Managers.Effect.Play(Define.Effect.CrocodileSwordEffect, Root);
        _particleSystem.transform.position = new Vector3(_particleSystem.transform.position.x, _particleSystem.transform.position.y + 1.5f, _particleSystem.transform.position.z);
        // new Vector3(skillObj.position.x - 1.0f, skillObj.position.y, skillObj.position.z - 1.0f);
        // 

        while (timer < moveDuration)
        {
            // 투사체와 파티클 시스템을 앞으로 움직입니다.
            Vector3 moveStep = dir * speed * Time.deltaTime;
            skillObj.position += moveStep;
            _particleSystem.transform.position += moveStep;

            timer += Time.deltaTime;
            yield return null;
        }

        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Resource.Destroy(_particleSystem.gameObject);
        Managers.Effect.Stop(_particleSystem);
    }

    public override void DeActiveCollider()
    {
        //StopCoroutine(_fireSword);
    }

    public override void SetCollider(int attackDamage)
    {
        _fireSword = StartCoroutine(FireSword(attackDamage));
    }
}
