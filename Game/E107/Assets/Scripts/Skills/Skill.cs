using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{

    protected float _lastCastTime;
    protected float _skillCoolDownTime;
    protected int _requiredMp;
    public float SkillCoolDownTime
    {
        get { return _skillCoolDownTime; } private set { _skillCoolDownTime = value; }
    }

    public float RequiredMp
    {
        get { return _requiredMp; }
        private set { _skillCoolDownTime = value; }
    }

    private void Start()
    {
        // 스킬 쿨다운 설정
        Init();
    }
    protected abstract void Init();
    protected abstract int Cast(int _attackDamage);
}
