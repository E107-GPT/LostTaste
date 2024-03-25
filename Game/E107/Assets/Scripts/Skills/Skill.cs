using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public static int _seq;
    protected float _lastCastTime;

    [field: SerializeField]
    [ReadOnly(false)]
    public string Name { get; set; }

    [field: SerializeField]
    [ReadOnly(false)]
    public string Description { get; set; }

    [field: SerializeField]
    [ReadOnly(false)]
    public Sprite Sprite { get; set; }

    [field: SerializeField]
    public float SkillCoolDownTime { get; set; }

    [field: SerializeField]
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
        return Cast(1, 1.0f);
    }
    public int Cast()
    {
        StartCoroutine(SkillCoroutine());   // TODO: �ǹ̾��� �Ķ���� ����
        _lastCastTime = Time.time;
        return RequiredMp;
    }

    [Obsolete("\n====================\n" +
        "�� �޼ҵ� ��� ���ڰ� ���� SkillCoroutine()�� �������ּ���."
    )]
    protected virtual IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        yield return null;
    }

    protected virtual IEnumerator SkillCoroutine()
    {
        return SkillCoroutine(1, 1.0f);
    }
}
