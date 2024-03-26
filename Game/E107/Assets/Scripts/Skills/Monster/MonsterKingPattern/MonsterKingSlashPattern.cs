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
            // wait for seconds�� ���ִ� Ÿ�̹��� ���߱� �����
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
        // �ʱ�ȭ
        for (int i = 1; i < patternObjs.Length; i++)
        {
            patternObjs[i].position = Root.position;
            patternObjs[i].GetComponent<PatternObject>().Init(Root, attackDamage, _seq);    // �θ� ��ü���� �� ���� ���� ����
            patternObjs[i].localScale = new Vector3(patternObjs[i].localScale.x, patternObjs[i].localScale.y, 1.0f * 5.0f);
        }

        // �� collider�� z���� �������� ������ �̵�
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
