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
    [SerializeField]
    protected Define.UnitType _unitType;
    [SerializeField]
    protected GameObject[] _existPlayer;      // 필드 위에 존재하는 플레이어 수

    protected MonsterStat _stat;
    private MonsterItem _curItem;

    [SerializeField]
    protected Transform _detectPlayer;        // 일반 공격 범위를 벗어나면 랜덤한 플레이어에게 이동 -> 지금은 가까운 플레이어에게 이동
    [SerializeField]
    protected Transform _attackPlayer;        // 일반 공격 타겟팅

    private Coroutine _checkMonsterState;
    protected Ray _ray;                       // Gizmos에 사용

    public MonsterStat Stat { get { return _stat; } }
    public Transform AttackPlayer { get { return _attackPlayer; } }

    public override void Init()
    {
        // BaseController
        _agent.stoppingDistance = 1.5f;
        _agent.angularSpeed = 500.0f;
        _agent.acceleration = 40.0f;
        _statemachine.CurState = new IdleState(this);

        // Editor Init
        _existPlayer = GameObject.FindGameObjectsWithTag("Player");

        // Other Class
        _stat = new MonsterStat(_unitType);
        _curItem = GetComponent<MonsterItem>();

        // State
        _checkMonsterState = StartCoroutine(CheckMonsterState());
        InvokeRepeating("UpdateDectPlayer", 0, 20.0f);              // 0초 후 호출, 20초마다 이동 타겟팅 수정 -> 여기서 가장 큰 데미지를 넣은 플레이어를 따라가게 할 수 있음
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
        if (CurState is SkillState) Gizmos.DrawWireSphere(_ray.origin, _stat.AttackRange);  // 일반 공격 범위
        if (CurState is MoveState) Gizmos.DrawWireSphere(_ray.origin, _stat.TargetRange);   // 패턴 공격 범위
    }


    // 이동 타겟팅 기능
    protected void UpdateDectPlayer()
    {
        int rand = -1;
        _detectPlayer = _existPlayer[0].transform;
        for (int i = 1; i < _existPlayer.Length; ++i)
        {
            rand = Random.Range(0, 2);     // 0 또는 1
            if (rand == 0)
            {
                _detectPlayer = _existPlayer[i].transform;
            }
        }
    }

    // 공격 범위 내의 플레이어 갱신
    protected void UpdateAttackPlayer()
    {
        Collider[] attackPlayers = Physics.OverlapSphere(transform.position, _stat.AttackRange, 1 << 7);

        //float minDistAttack = _stat.AttackRange;
        PrintText($"공격 범위내의 플레이어: {attackPlayers.Length}");
        if (attackPlayers.Length > 0)
        {
            for (int i = 0; i < attackPlayers.Length; ++i)
            {
                _attackPlayer = attackPlayers[i].gameObject.transform;
                //float dist = Vector3.Distance(transform.position, targetPlayers[i].transform.position);
                //if (minDistAttack > dist)
                //{
                //    minDistAttack = dist;
                //    AttackPlayer = targetPlayers[i].gameObject.transform;
                //}
            }
        }
        else
        {
            _attackPlayer = null;
        }

        //minDistAttack = _stat.AttackRange;
    }

    IEnumerator CheckMonsterState()
    {
        while (_stat.Hp > 0)
        {
            yield return new WaitForSeconds(0.3f);

            UpdateAttackPlayer();
            
            if (_attackPlayer != null)
            {
                if (CurState is SkillState) continue;

                _statemachine.ChangeState(new SkillState(this));
            }
            else if (_existPlayer.Length != 0)
            {
                if (CurState is MoveState) continue;

                _statemachine.ChangeState(new MoveState(this));
            }
            else
            {
                if (CurState is IdleState) continue;
                _detectPlayer = null;
                _attackPlayer = null;
                _statemachine.ChangeState(new IdleState(this));
            }
        }

        _statemachine.ChangeState(new DieState(this));
        CancelInvoke("UpdateDectPlayer");
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

        _animator.CrossFade("Move", 1.0f);
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

        _agent.SetDestination(_detectPlayer.position);
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
    }
    public override void ExcuteSkill()
    {
        base.ExcuteSkill();

        Vector3 thisToTargetDist = _attackPlayer.position - transform.position;
        Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);
        // Quaternion rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirToTarget.normalized, Vector3.up), 0.5f);

        _curItem.NormalAttack();
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
}

