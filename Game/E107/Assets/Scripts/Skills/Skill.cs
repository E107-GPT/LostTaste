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

        // TOOD: Sprite가 null이면 기본 Sprite 할당

        Init();
    }

    // 스킬 쿨다운 설정
    protected abstract void Init();

    [Obsolete("\n====================\n" +
        "스킬 별로 기본 공격 대미지와 범위가 달라 이 메소드는 없앨 예정입니다.\n" +
        "각 스킬마다 하드코딩하여 추가해주시고, 필요시 추후 대미지 배율, 범위 배율 등을 인자로 넘기는 메소드를 추가하겠습니다.\n" +
        "이 메소드 대신 인자가 없는 Cast()를 사용해주세요."
    )]
    public int Cast(int attackDamage, float attackRange)
    {
        Managers.Coroutine.Run(SkillCoroutine(attackDamage, attackRange));   // TODO: 의미없는 파라미터 제거
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
        "이 메소드 대신 인자가 없는 SkillCoroutine()을 구현해주세요."
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
}
