using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingController : MonsterController
{
    private float _jumpCoolDown;    // 점프 쿨타임
    private bool _isJumping;        // 지금 점프 중?

    public override void Init()
    {
        base.Init();

        // Other Class
        _stat = new MonsterStat(_unitType);

        _jumpCoolDown = 10.0f;
        _isJumping = false;
    }

    private void Update()
    {
        // _statemachine.Execute();
    }


    protected override void ChangeStateFromMove()
    {
        float distToDetectPlayer = (transform.position - _detectPlayer.position).magnitude;

        _agent.SetDestination(_detectPlayer.position);


        if (distToDetectPlayer <= _stat.AttackRange)
        {
            RandomPatternSelector();
        }
        else if (distToDetectPlayer > _stat.DetectRange)
        {
            _detectPlayer = null;
            _statemachine.ChangeState(new IdleState(this));
        }
    }

    private void RandomPatternSelector()
    {
        int rand = Random.Range(0, 101);
        if (_stat.Hp <= _stat.MaxHp / 2)
        {
            _statemachine.ChangeState(new MonsterKingHitDownState(this));
        }
        else if (_stat.Hp <= _stat.MaxHp)
        {
            _statemachine.ChangeState(new MonsterKingHitDownState(this));
        }
    }

    #region State Method
    public override void EnterMonsterKingHitDownState()         // HitDown
    {
        // 이것도 함수로 통합
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;

        ToDetectPlayer(0.8f);

        _monsterInfo.Patterns[0].SetCollider(_stat.AttackDamage);
        _animator.CrossFade("HitDown", 0.3f, -1, 0);
    }      
    public override void ExecuteMonsterKingHitDownState() 
    {
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("HitDown"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime >= 1.0f)
            {
                PrintText("공격 -> IDLE");
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitMonsterKingHitDownState() { }
    public override void EnterMonsterKingSlashState() { }        // Slash
    public override void ExecuteMonsterKingSlashState() { }
    public override void ExitMonsterKingSlashState() { }
    public override void EnterMonsterKingStabState() { }         // Stab
    public override void ExecuteMonsterKingStabState() { }
    public override void ExitMonsterKingStabState() { }
    public override void EnterMonsterKingJumpStartState() { }    // JumpStart
    public override void ExecuteMonsterKingJumpStartState() { }
    public override void ExitMonsterKingJumpStartState() { }
    public override void EnterMonsterKingJumpAirState() { }      // JumpAir
    public override void ExecuteMonsterKingJumpAirState() { }
    public override void ExitMonsterKingJumpAirState() { }
    public override void EnterMonsterKingJumpEndState() { }      // JumpEnd
    public override void ExecuteMonsterKingJumpEndState() { }
    public override void ExitMonsterKingJumpEndState() { }

    #endregion
}
