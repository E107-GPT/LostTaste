using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckController : MonsterController
{
    [SerializeField]
    private Transform _targetPlayer;        // 패턴 타겟팅
    private bool _isDonePattern;            // 패턴이 끝났나?
    private float _lastPatternTime;

    private DrillDuckSlideState _slideState;

    public Transform TargetPlayer { get { return _targetPlayer; } set { _targetPlayer = value; } }
    public bool IsDonePattern { get { return _isDonePattern; } set { _isDonePattern = value; } }

    public override void Init()
    {
        // MonsterController Init()인지 확인 필요
        base.Init();

        // Other Class
        _stat = new MonsterStat(_unitType);
        _slideState = new DrillDuckSlideState(this);

        // Cur Pattern
        _isDonePattern = true;
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    // 보스 패턴을 위한 타겟팅
    private void UpdateTargetPlayer()
    {
        Collider[] targetPlayers = Physics.OverlapSphere(transform.position, _stat.TargetRange, 1 << 7);

        // 패턴 타겟팅 조건 추가
        PrintText($"패턴 공격 범위내의 플레이어: {targetPlayers.Length}");
        foreach (Collider player in targetPlayers)
        {
            _targetPlayer = player.transform;
            return;
        }

        // 패턴 범위에 플레이어가 없으면
        _targetPlayer = null;
    }

    protected override void ChangeStateFromMove()
    {
        float distToDetectPlayer = (transform.position - _detectPlayer.position).magnitude;
        // float distToTargetPlayer = (transform.position - _targetPlayer.position).magnitude;
        // 거리는 UpdateTargetPlayer에서 판단중

        _agent.SetDestination(_detectPlayer.position);

        // targetPlayer말고 detectPlayer로 할 수 있을 것 같음
        //if (_targetPlayer != null)
        //{
        //    _statemachine.ChangeState(_slideState);
        //}
        if (distToDetectPlayer <= _stat.AttackRange)
        {
            _statemachine.ChangeState(new SkillState(this));
        }
        else if (distToDetectPlayer > _stat.DetectRange)
        {
            _targetPlayer = null;
            _detectPlayer = null;
            _statemachine.ChangeState(new IdleState(this));
        }
    }

    // Idle
    public override void ExcuteIdle()
    {
        base.ExcuteIdle();

        // Hp가 70% 이하라면 일정 시간마다 패턴 공격을 위한 TargetPlayer 세팅
        //if ((Time.time - _lastPatternTime >= _stat.PatternDelay) && _isDonePattern == true && (_stat.Hp <= _stat.MaxHp))
        //{
        //    UpdateTargetPlayer();
        //    _lastPatternTime = Time.time;
        //}
    }

    // Move
    public override void EnterMove()
    {
        base.EnterMove();
    }
    public override void ExcuteMove()
    {
        base.ExcuteMove();
    }
    public override void ExitMove()
    {
        base.ExitMove();
    }

    // Normal Attack
    public override void EnterSkill()
    {
        base.EnterSkill();
    }
    public override void ExcuteSkill()
    {
        base.ExcuteSkill();
    }
    public override void ExitSkill()
    {
        base.ExitSkill();
    }

    // Silde

}
