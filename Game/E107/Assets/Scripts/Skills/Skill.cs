using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public static int _seq;
    protected string _skillName;
    protected float _lastCastTime;
    protected float _skillCoolDownTime;
    protected int _requiredMp;
    protected Transform _root;
    protected Transform _effect;

    public string SkillName
    {
        get { return _skillName; } set { _skillName = value; }
    }
    public float SkillCoolDownTime
    {
        get { return _skillCoolDownTime; } set { _skillCoolDownTime = value; }
    }

    public int RequiredMp
    {
        get { return _requiredMp; }
        set { _requiredMp = value; }
    }

    public Transform Root
    {
        get { return _root; }
        set { _root = value; }
    }
    public Transform Effect
    {
        get { return _effect; }
        set { _effect = value; }
    }

    private void Start()
    {
        Root = transform.root;
        Effect = transform.root.Find("Effect");

        Init();
    }

    // 스킬 쿨다운 설정
    protected abstract void Init();
    public int Cast(int _attackDamage, float _attackRange) {
        if (Time.time - _lastCastTime < SkillCoolDownTime) return 0;
        StartCoroutine(SkillCoroutine(_attackDamage, _attackRange));
        return RequiredMp; 
    }

    protected abstract IEnumerator SkillCoroutine(int _attackDamage, float _attackRange);


}
