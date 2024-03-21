using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

// 일반 몬스터
public class MonsterController : BaseController
{
    protected MonsterStat _stat;
    protected MonsterInfo _monsterInfo;
    private string _curSkillName;           // 현재 공격의 이름 - Info에서 가져옴
    private float _lastDetectTime;

    [SerializeField]
    protected Transform _detectPlayer;        // 이동 타겟팅, 일반 공격 범위를 벗어나면 랜덤한 플레이어에게 이동 -> 지금은 가까운 플레이어에게 이동
    protected Ray _ray;                       // Gizmos에 사용

    public MonsterStat Stat { get { return _stat; } }
    public MonsterInfo MonsterInfo { get { return _monsterInfo; } }
    public Transform DetectPlayer { get { return _detectPlayer; } }
    

    public override void Init()
    {
        // BaseController
        _agent.stoppingDistance = 1.5f;
        _agent.angularSpeed = 500.0f;
        _agent.acceleration = 40.0f;
        _statemachine.CurState = new IdleState(this);

        // Other Class
        _stat = new MonsterStat(_unitType);
        _monsterInfo = GetComponent<MonsterInfo>();
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

    protected void OnDrawGizmos()
    {
        _ray.origin = transform.position;
        Gizmos.color = Color.red;
        if (CurState is MoveState) Gizmos.DrawWireSphere(_ray.origin, _stat.DetectRange);        // 탐색 범위
    }

    //이동 타겟팅 기능
    //가장 큰 데미지를 넣은 플레이어를 추격하는 기능을 넣을 수 있다.
    protected void UpdateDetectPlayer()
    {
        Collider[] detectedPlayers = Physics.OverlapSphere(transform.position, _stat.DetectRange, 1 << 7);

        int rand = -1;
        foreach (var player in detectedPlayers)
        {
            rand = Random.Range(0, 2);     // 0 또는 1
            if (rand == 0)
            {
                _detectPlayer = player.transform;
                _statemachine.ChangeState(new MoveState(this));
                return;
            }
        }
        _detectPlayer = null;
    }

    // Move 상태에서 다른 상태로 바꾸는 조건
    protected virtual void ChangeStateFromMove()
    {
        float distanceToPlayer = (transform.position - _detectPlayer.position).magnitude;

        _agent.SetDestination(_detectPlayer.position);


        if (distanceToPlayer <= _monsterInfo.AttackRange)
        {
            _statemachine.ChangeState(new SkillState(this));
        }
        else if (distanceToPlayer > _stat.DetectRange)
        {
            _detectPlayer = null;
            _statemachine.ChangeState(new IdleState(this));
        }
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
        if (_stat.Hp < 0) _stat.Hp = 0;
        lastAttackTimes[skillObjectId] = Time.time; // 해당 공격자의 마지막 공격 시간 업데이트
        PrintText($"{_stat.Hp}!!!");

        if (_stat.Hp <= 0)
        {
            _statemachine.ChangeState(new DieState(this));
        }
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
        // Time.time: 게임이 시작된 후부터 시간(초)을 반환
        // _lastTime: 마지막으로 호출된 시간(초)을 가진다.
        // _coolDownTime: 스킬 사용 시간(초)을 나타낸다.
        if (Time.time - _lastDetectTime >= _stat.DetectTime)
        {
            UpdateDetectPlayer();
            _lastDetectTime = Time.time;
        }
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

        // -1: 이진수 11111 모든 layer를 선택
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

        ChangeStateFromMove();
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

        Vector3 thisToTargetDist = _detectPlayer.position - transform.position;
        Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);
        // Quaternion rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirToTarget.normalized, Vector3.up), 0.5f);

        // 상속
        _monsterInfo.Skill.Cast(_stat.AttackDamage, _stat.AttackRange);
        _animator.CrossFade("Attack", 0.3f, -1, 0);
    }

    public override void ExcuteSkill()
    {
        base.ExcuteSkill();

        // 상태 전환이 완벽하게 이뤄졌을 때 "Attack" 애니메이션이 끝났는지 확인
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime >= 1.0f)
            {
                _statemachine.ChangeState(new IdleState(this));
            }
        }

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
        GetComponent<Collider>().enabled = false;
        _agent.enabled = false;

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
}

