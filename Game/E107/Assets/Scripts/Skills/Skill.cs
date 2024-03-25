using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public static int _seq;
    protected float _lastCastTime;

    public string Name { get; set; }
    public string Description { get; set; }
    public Sprite Sprite { get; set; }
    public float SkillCoolDownTime { get; set; }

    public int RequiredMp { get; set; }

    public Transform Root { get; set; }

    private void Start()
    {
        Root = transform.root;

        // TOOD: Sprite�� null�̸� �⺻ Sprite �Ҵ�

        Init();
    }

    // ��ų ��ٿ� ����
    protected abstract void Init();

    [Obsolete("\n====================\n" +
        "��ų ���� �⺻ ���� ������� ������ �޶� �� �޼ҵ�� ���� �����Դϴ�.\n" +
        "�� ��ų���� �ϵ��ڵ��Ͽ� �߰����ֽð�, �ʿ�� ���� ����� ����, ���� ���� ���� ���ڷ� �ѱ�� �޼ҵ带 �߰��ϰڽ��ϴ�.\n" +
        "�� �޼ҵ� ��� ���ڰ� ���� Cast()�� ������ּ���."
    )]
    public int Cast(int attackDamage, float attackRange)
    {
        StartCoroutine(SkillCoroutine(attackDamage, attackRange));   // TODO: �ǹ̾��� �Ķ���� ����
        _lastCastTime = Time.time;
        return RequiredMp;
    }
    public int Cast()
    {
        return Cast(1, 1.0f);
    }

    protected abstract IEnumerator SkillCoroutine(int _attackDamage, float _attackRange);
}
