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

    private Animator _animator;
    protected StateMachine _statemachine;
    public State CurState
    {
        get { return _statemachine.CurState; }
        set { CurState = value; }
    }
    public Animator Animator { get => _animator; set => _animator = value; }

    private void Start()
    {
        Root = transform.root;

        // TOOD: Sprite�� null�̸� �⺻ Sprite �Ҵ�

        Init();
    }

    void Update()
    {
        _statemachine.Execute();
    }

    protected abstract void Init();

    public int Cast()
    {
        Managers.Coroutine.Run(SkillCoroutine());
        LastCastTime = Time.time;
        return RequiredMp;
    }

    protected abstract IEnumerator SkillCoroutine();

    public virtual bool IsMonsterCastable()
    {
        return LastCastTime == 0 || Time.time - LastCastTime >= SkillCoolDownTime;
    }

    public virtual bool IsPlayerCastable(PlayerController playerController)
    {
        return IsMonsterCastable() && (playerController.Stat.Mp >= RequiredMp);
    }

    #region WeaponState
    public virtual void EnterIdle() { }
    public virtual void ExecuteIdle() { }
    public virtual void ExitIdle() { }
    public virtual void EnterAttack() { }
    public virtual void ExecuteAttack() { }
    public virtual void ExitAttack() { }
    #endregion
}
