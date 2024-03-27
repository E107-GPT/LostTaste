using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingStabPattern : Pattern
{
    private MonsterKingController _controller;
    private ParticleSystem _particle;
    private Coroutine _coroutine;

    private Transform _leftArm;
    private Transform _stabLoc;

    protected override void Init()
    {
        PatternName = "KingStabEffect";
        _controller = GetComponent<MonsterKingController>();
    }
    public override void DeActiveCollider()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
            if (_particle != null) Managers.Effect.Stop(_particle);
            if (_stabLoc != null) Managers.Resource.Destroy(_stabLoc.gameObject);
        }
    }

    IEnumerator CheckPatternObject(int attackDamage)
    {
        Root = _controller.transform;

        yield return new WaitForSeconds(0.1f);

        // hit box ����
        //_leftArm = _controller.LeftArm.transform;
        _stabLoc = Managers.Resource.Instantiate("Patterns/StabCollider").transform;
        _stabLoc.GetComponent<PatternObject>().Init(Root, attackDamage, _seq);

        // ���� ��ġ�� ��� �������� ��ġ�ϴ��� Ȯ��
        Vector3 rootLeft = Root.TransformDirection(Vector3.left);              // root�� local ��ǥ�踦 �������� �������� �̵�
        Vector3 rootUp = Root.TransformDirection(Vector3.up);                  // root�� local ��ǥ�踦 �������� �������� �̵�
        Vector3 tempPos = Root.position + (rootUp) + (rootLeft * 0.7f);        // root�� ��ġ���� �߰��� �������� ��ĭ, �������� ��ĭ�� �̵�
        _stabLoc.position = tempPos;
        _stabLoc.rotation = Root.rotation;

        _particle = Managers.Effect.Play(Define.Effect.KingStabEffect, _stabLoc);      // stabLoc�̶� �Բ� �̵�


        // ����Ʈ�� hit box�� ��� �ִϸ��̼ǰ� ���缭 �̵�
        float speed = 43.0f;
        while (true)
        {
            Vector3 moveStep = _stabLoc.forward * speed * Time.deltaTime;
            _stabLoc.position += moveStep;
            _particle.transform.position = _stabLoc.position;

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
