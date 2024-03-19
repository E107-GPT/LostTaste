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

    private void Start()
    {
        Root = transform.root;
        Init();
    }

    // 스킬 쿨다운 설정
    protected abstract void Init();
    protected int Cast(int _attackDamage) {
        if (Time.time - _lastCastTime < SkillCoolDownTime) return 0;
        StartCoroutine(SkillCoroutine(_attackDamage));
        return RequiredMp; 
    }

    protected abstract IEnumerator SkillCoroutine(int _attackDamage, int _attackRange);


}
