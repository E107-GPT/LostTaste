using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public static int _seq;
    public float LastCastTime { get; set; }

    [field: SerializeField]
    [ReadOnly(false)]
    public string Name { get; set; }

    [field: SerializeField]
    [ReadOnly(false)]
    public string Description { get; set; }

    [field: SerializeField]
    [ReadOnly(false)]
    public Sprite Icon { get; set; }

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
        Managers.Coroutine.Run(SkillCoroutine(attackDamage, attackRange));   // TODO: �ǹ̾��� �Ķ���� ����
        LastCastTime = Time.time;
        return RequiredMp;
    }
    public int Cast()
    {
        Managers.Coroutine.Run(SkillCoroutine());
        LastCastTime = Time.time;
        return RequiredMp;
    }

    [Obsolete("\n====================\n" +
        "�� �޼ҵ� ��� ���ڰ� ���� SkillCoroutine()�� �������ּ���."
    )]
    protected virtual IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        return SkillCoroutine();
    }

    protected virtual IEnumerator SkillCoroutine()
    {
#pragma warning disable 0618
        return SkillCoroutine(1, 1.0f);
#pragma warning restore 0618
    }

    public virtual bool IsMonsterCastable()
    {
        return LastCastTime == 0 || Time.time - LastCastTime >= SkillCoolDownTime;
    }

    public virtual bool IsPlayerCastable(PlayerController playerController)
    {
        return IsMonsterCastable() && (playerController.Stat.Mp >= RequiredMp);
    }
}
