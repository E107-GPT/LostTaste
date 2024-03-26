using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingSlashPattern : Pattern
{
    private MonsterKingController _controller;
    private Coroutine _coroutine;
    private ParticleSystem _particle;
    private Transform _sectorLoc;

    protected override void Init()
    {
        PatternName = "SlashLurkerEffect";
        _controller = GetComponent<MonsterKingController>();
    }
    public override void DeActiveCollider()
    {
        if (_coroutine != null)
        {
            // wait for seconds로 없애는 타이밍을 맞추기 힘들다
            StopCoroutine(_coroutine);
            _coroutine = null;
            if (_particle != null) Managers.Effect.Stop(_particle);
            if (_sectorLoc != null) Managers.Resource.Destroy(_sectorLoc.gameObject);
        }
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.transform;
        yield return new WaitForSeconds(0.1f);

        _particle = Managers.Effect.Play(Define.Effect.SlashLurkerEffect, Root);
        _sectorLoc = Managers.Resource.Instantiate("Patterns/SlashLurkerCollider").transform;
        _sectorLoc.position = Root.position;
        _sectorLoc.rotation = Root.rotation;

        Transform[] patternObjs = _sectorLoc.GetComponentsInChildren<Transform>();
        // 초기화
        for (int i = 1; i < patternObjs.Length; i++)
        {
            patternObjs[i].position = Root.position;
            patternObjs[i].GetComponent<PatternObject>().Init(Root, attackDamage, _seq);    // 부모 객체에서 한 번에 적용 못함
            patternObjs[i].localScale = new Vector3(patternObjs[i].localScale.x, patternObjs[i].localScale.y, 1.0f * 5.0f);
        }

        // 각 collider의 z축을 기준으로 앞으로 이동
        float speed = 50.0f;
        while(true)
        {
            for (int i = 1; i < patternObjs.Length; i++)
            {
                Vector3 moveStep = patternObjs[i].forward * speed * Time.deltaTime;
                patternObjs[i].position += moveStep;
            }
            yield return null;
        }

    }

    public override void SetCollider(int attackDamage)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(CheckPatternObject(attackDamage));
        }
    }
}
