using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class DrillDuckController : MonsterController
{
    [SerializeField]
    private float _lastPatternTime;


    public override void Init()
    {
        // MonsterController Init()인지 확인 필요
        base.Init();

        // Other Class
        _stat = new MonsterStat(_unitType);
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
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
        if (rand <= 30)
        {
            _statemachine.ChangeState(new DrillDuckSlideState(this));
        }
        else if (rand <= 100)
        {
            _statemachine.ChangeState(new SkillState(this));
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

    // Silde
    public override void EnterDrillDuckSlideState()
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;

        Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
        Vector3 destPos = transform.position + dirTarget * _stat.DetectRange;

        // 경로상의 플레이어를 밀쳐내면서 돌진
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        _agent.radius *= 2;
        _agent.avoidancePriority = 0;

        _monsterInfo.Patterns[0].SetCollider(_stat.PatternDamage);

        _agent.SetDestination(destPos);
        _animator.CrossFade("Slide", 0.2f, -1, 0);
    }
    public override void ExcuteDrillDuckSlideState()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            

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
    }

}
