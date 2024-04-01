using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingHitDownChargePattern : Pattern
{
    private MonsterKingController _controller;

    protected override void Init()
    {
        PatternName = "KingHitDownStartEffect";
        _controller = GetComponent<MonsterKingController>();
    }
    public override void DeActiveCollider()
    {
    }

    IEnumerator CheckPatternObject()
    {
        Root = _controller.Weapon.transform;
        ParticleSystem particle = Managers.Effect.Play(Define.Effect.KingHitDownStartEffect, Root.transform);
        Vector3 rootBack = Root.TransformDirection(Vector3.back * 3.0f);
        particle.transform.position += rootBack;

        float moveDuration = 2.4f;
        float timer = 0;
        float speed = 2.2f;
        while (timer < moveDuration)
        {
            Vector3 moveStep = Vector3.up * speed * Time.deltaTime;
            particle.transform.position += moveStep;

            timer += Time.deltaTime;
            yield return null;
        }
        Managers.Effect.Stop(particle);
    }

    public override void SetCollider(int attackDamage)
    {
    }

    public override void SetCollider()
    {
        StartCoroutine(CheckPatternObject());
    }
}
