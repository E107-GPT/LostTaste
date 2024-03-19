using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

// 일반 몬스터
public class MonsterControllerV2 : BaseController
{
    protected MonsterStat _stat;
    private MonsterItemV2 _curItem;

    protected Transform _detectedPlayer;        // 일반 공격 범위를 벗어나면 랜덤한 플레이어에게 이동 -> 지금은 가까운 플레이어에게 이동
    protected Transform _attackPlayer;        // 일반 공격 타겟팅


    public MonsterStat Stat { get { return _stat; } }
    public Transform AttackPlayer { get { return _attackPlayer; } }

    public override void Init()
    {
        // BaseController
        _agent.stoppingDistance = 1.5f;
        _agent.angularSpeed = 500.0f;
        _agent.acceleration = 40.0f;
        _statemachine.CurState = new IdleState(this);

        // Other Class
        _stat = new MonsterStat(_unitType);
        _curItem = GetComponent<MonsterItemV2>();
    }
    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    // 캐릭터에게 물리력을 받아도 밀려나는 가속도로 인해 이동에 방해받지 않는다.
    protected void FreezeVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    protected void DetectPlayer()
    {
        Collider[] detectedPlayers = Physics.OverlapSphere(transform.position, 7.0f, LayerMask.GetMask("Player"));
        
        foreach(var player in detectedPlayers)
        {
            _detectedPlayer = player.transform;
            _statemachine.ChangeState(new MoveState(this));
            return;
        }

        _detectedPlayer = null;
    }

    // IDLE
    public override void EnterIdle()
    {
        base.EnterIdle();
        _agent.speed = 0;
        _agent.velocity = Vector3.zero;
        _animator.CrossFade("Idle", 0.5f);      // 기본적으로 base layer의 state를 나타냄
    }
    public override void ExcuteIdle()
    {
        base.ExcuteIdle();
        DetectPlayer();

    }
    public override void ExitIdle()
    {
        base.ExitIdle();
    }

    // MOVE
    public override void EnterMove()
    {
        base.EnterMove();
        _agent.speed = _stat.MoveSpeed;
        _agent.velocity = Vector3.zero;

        _animator.CrossFade("Move", 1.0f, -1, 0);
    }
    public override void ExcuteMove()
    {
        base.ExcuteMove();
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Move"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (aniTime <= 0.1f)
            {
                _agent.speed = _stat.MoveSpeed * 2.0f;
            }
            else if (aniTime <= 1.0f)
            {
                _agent.speed = _stat.MoveSpeed;
            }
        }


        float distanceToPlayer = (transform.position - _detectedPlayer.position).magnitude;

        _agent.SetDestination(_detectedPlayer.position);

        if (distanceToPlayer <= _stat.AttackRange)
        {
            _statemachine.ChangeState(new SkillState(this));
        }
        else if (distanceToPlayer > 7.0f)
        {
            _detectedPlayer = null;
            _statemachine.ChangeState(new IdleState(this));
            
        }
        
    }
    public override void ExitMove()
    {
        base.ExitMove();
    }

    // SKILL
    public override void EnterSkill()
    {
        base.EnterSkill();
        _agent.speed = 0;
        _agent.velocity = Vector3.zero;
        
        _animator.CrossFade("Attack", 0.3f);
        Vector3 thisToTargetDist = _detectedPlayer.position - transform.position;
        Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);
        // Quaternion rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirToTarget.normalized, Vector3.up), 0.5f);

        _curItem.NormalAttack();
    }
    public override void ExcuteSkill()
    {
        base.ExcuteSkill();

        
    }
    public override void ExitSkill()
    {
        base.ExitSkill();
    }

    // DIE
    public override void EnterDie()
    {
        base.EnterDie();
        _agent.speed = 0;
        _agent.velocity = Vector3.zero;

        _animator.CrossFade("Die", 0.5f);

        // 스폰에서 몬스터 배열을 통해 null 처리 또는 destroy
        Destroy(gameObject, 5.0f);
    }
    public override void ExcuteDie()
    {
        base.ExcuteDie();
    }
    public override void ExitDie()
    {
        base.ExitDie();
    }

    public override void TakeDamage(int skillObjectId, int damage)
    {
        base.TakeDamage(skillObjectId, damage);

        float lastAttackTime;
        lastAttackTimes.TryGetValue(skillObjectId, out lastAttackTime);

        if (Time.time - lastAttackTime < damageCooldown)
        {
            // 쿨다운 중이므로 피해를 주지 않음
            return;
        }

        _stat.Hp -= damage;
        lastAttackTimes[skillObjectId] = Time.time; // 해당 공격자의 마지막 공격 시간 업데이트
        PrintText($"{_stat.Hp}!!!");

        if (_stat.Hp <= 0)
        {
            _statemachine.ChangeState(new DieState(this));
        }
    }
}

