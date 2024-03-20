using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class DrillDuckController : MonsterController
{
    [SerializeField]
    private bool _isDonePattern;            // 패턴이 끝났나?
    private float _lastPatternTime;

    public bool IsDonePattern { get { return _isDonePattern; } set { _isDonePattern = value; } }

    public override void Init()
    {
        // MonsterController Init()인지 확인 필요
        base.Init();

        // Other Class
        _stat = new MonsterStat(_unitType);

        // Cur Pattern
        _isDonePattern = true;
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }


    protected override void ChangeStateFromMove()
    {
        float distToDetectPlayer = (transform.position - _detectPlayer.position).magnitude;

        _agent.SetDestination(_detectPlayer.position);


        if (distToDetectPlayer <= _stat.TargetRange && (_stat.Hp <= _stat.MaxHp))
        {
            _statemachine.ChangeState(new DrillDuckReadyState(this));
        }
        else if (distToDetectPlayer <= _stat.AttackRange)
        {
            _statemachine.ChangeState(new SkillState(this));
        }
        else if (distToDetectPlayer > _stat.DetectRange)
        {
            _detectPlayer = null;
            _statemachine.ChangeState(new IdleState(this));
        }
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

    // Slide Ready: skill 생성 info 생성
    public override void EnterDrillDuckReadyState()
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;
        Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
        Vector3 destPos = transform.position + dirTarget * _stat.TargetRange;
        _agent.SetDestination(destPos);

        _animator.CrossFade("Grab", 0.5f, -1, 0);
    }
    public override void ExcuteDrillDuckReadyState()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Grab"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;    // 속도: 0.3
            if (aniTime >= 0.1f)
            {
                _statemachine.ChangeState(new DrillDuckSlideState(this));
            }
        }
    }
    public override void ExitDrillDuckReadyState()
    {
        _agent.speed = _stat.MoveSpeed;
    }

    // Silde
    public override void EnterDrillDuckSlideState()
    {
        //_isDonePattern = false;
        //_agent.velocity = Vector3.zero;
        //_agent.speed = 0;

        Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
        Vector3 destPos = transform.position + dirTarget * _stat.TargetRange;

        // 경로상의 플레이어를 뚫고 가거나 밀쳐내고 가기 위함
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        _agent.radius *= 2;
        _agent.avoidancePriority = 0;

        _agent.SetDestination(destPos);
        _animator.CrossFade("Slide", 0.2f, -1, 0);
    }
    public override void ExcuteDrillDuckSlideState()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            _monsterInfo.Patterns[0].SetCollider(_stat.PatternDamage, _stat.AttackRange);

            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime <= 0.1f)
            {
                _agent.speed = _stat.MoveSpeed;
            }
            else if (aniTime <= 0.5f)
            {
                _agent.speed = _stat.MoveSpeed * 3.0f;
            }
            else if (aniTime < 1.0f)
            {
                _agent.speed = _stat.MoveSpeed / 2;
            }
            else if (aniTime >= 1.0f)
            {
                _monsterInfo.Patterns[0].DeActiveCollider();
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitDrillDuckSlideState()
    {
        _agent.radius /= 2;
        _agent.avoidancePriority = 50;
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        //GetComponent<Collider>().isTrigger = false;
        //_isDonePattern = true;

    }

}
