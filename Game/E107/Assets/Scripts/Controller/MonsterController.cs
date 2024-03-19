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

    //protected GameObject[] _existPlayer;      // 필드 위에 존재하는 플레이어 수

    protected MonsterStat _stat;
    private MonsterItem _curItem;

    [SerializeField]
    protected Transform _detectPlayer;        // 이동 타겟팅, 일반 공격 범위를 벗어나면 랜덤한 플레이어에게 이동 -> 지금은 가까운 플레이어에게 이동
    //protected Transform _attackPlayer;        // 일반 공격 타겟팅

    //private Coroutine _checkMonsterState;
    protected Ray _ray;                       // Gizmos에 사용

    public MonsterStat Stat { get { return _stat; } }
    //public Transform AttackPlayer { get { return _attackPlayer; } }

    public override void Init()
    {
        // BaseController
        _agent.stoppingDistance = 1.5f;
        _agent.angularSpeed = 500.0f;
        _agent.acceleration = 40.0f;
        _statemachine.CurState = new IdleState(this);

        // Other Class
        _stat = new MonsterStat(_unitType);
        _curItem = GetComponent<MonsterItem>();

        // Editor Init
        //_existPlayer = GameObject.FindGameObjectsWithTag("Player");
        //StartCoroutine(CheckExistPlayer());

        // State
        //_checkMonsterState = StartCoroutine(CheckMonsterState());
        //_detectPlayer = _existPlayer[0].transform;
        //InvokeRepeating("UpdateDectPlayer", 0, 20.0f);
    }

    // DrillDuck에서 사용할 때 죽었는지 확인하는 if문에서 Null 에러가 발생한다.
    // Slime도 확인

    //protected IEnumerator CheckExistPlayer()
    //{
    //    yield return new WaitForSeconds(0.3f);

    //    while (_existPlayer.Length != 0)
    //    {
    //        PrintText("CheckExistPlayer");
    //        _existPlayer = GameObject.FindGameObjectsWithTag("Player");
    //        for (int i = 0; i < _existPlayer.Length; ++i)
    //        {
    //            PlayerController playerController = _existPlayer[i].GetComponent<PlayerController>();
    //            if (playerController.StateMachine.CurState is DieState)
    //            {
    //                PrintText("죽었나?");
    //                _existPlayer[i] = null;
    //            }
    //        }
    //    }
    //}

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
        if (CurState is IdleState) Gizmos.DrawWireSphere(_ray.origin, _stat.DetectRange);        // 탐색 범위
        //else if (CurState is MoveState) Gizmos.DrawWireSphere(_ray.origin, _stat.TargetRange);   // 패턴 공격 범위
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

    // 공격 범위 내의 플레이어 갱신
    //protected void UpdateAttackPlayer()
    //{
    //    Collider[] attackPlayers = Physics.OverlapSphere(transform.position, _stat.AttackRange, 1 << 7);

    //    foreach (Collider player in attackPlayers)
    //    {
    //        _attackPlayer = player.transform;
    //        _statemachine.ChangeState(new SkillState(this));
    //        return;
    //    }

    //    _attackPlayer = null;

    //    //float minDistAttack = _stat.AttackRange;
    //    PrintText($"공격 범위내의 플레이어: {attackPlayers.Length}");
    //    if (attackPlayers.Length > 0)
    //    {
    //        for (int i = 0; i < attackPlayers.Length; ++i)
    //        {
    //            _attackPlayer = attackPlayers[i].gameObject.transform;
    //            //float dist = Vector3.Distance(transform.position, targetPlayers[i].transform.position);
    //            //if (minDistAttack > dist)
    //            //{
    //            //    minDistAttack = dist;
    //            //    AttackPlayer = targetPlayers[i].gameObject.transform;
    //            //}
    //        }
    //    }
    //    else
    //    {
    //        _attackPlayer = null;
    //    }

    //    //minDistAttack = _stat.AttackRange;
    //}

    //IEnumerator CheckMonsterState()
    //{
    //    while (_stat.Hp > 0)
    //    {
    //        yield return new WaitForSeconds(0.3f);

    //        UpdateAttackPlayer();
            
    //        if (_attackPlayer != null)
    //        {
    //            if (CurState is SkillState) continue;

    //            _statemachine.ChangeState(new SkillState(this));
    //        }
    //        else if (_existPlayer.Length != 0 && _detectPlayer != null)
    //        {
    //            if (CurState is MoveState) continue;

    //            _statemachine.ChangeState(new MoveState(this));
    //        }
    //        else
    //        {
    //            if (CurState is IdleState) continue;
    //            _detectPlayer = null;
    //            _attackPlayer = null;
    //            _statemachine.ChangeState(new IdleState(this));
    //        }
    //    }

    //    _statemachine.ChangeState(new DieState(this));
    //    CancelInvoke("UpdateDectPlayer");
    //}

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
        UpdateDetectPlayer();
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

        float distanceToPlayer = (transform.position - _detectPlayer.position).magnitude;

        _agent.SetDestination(_detectPlayer.position);

        if (distanceToPlayer <= _stat.AttackRange)
        {
            _statemachine.ChangeState(new SkillState(this));
        }
        else if (distanceToPlayer > _stat.DetectRange)
        {
            _detectPlayer = null;
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

        Vector3 thisToTargetDist = _detectPlayer.position - transform.position;
        Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);
        // Quaternion rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirToTarget.normalized, Vector3.up), 0.5f);

        _curItem.NormalAttack();
        _animator.CrossFade("Attack", 0.3f, -1, 0);
    }
    public override void ExcuteSkill()
    {
        base.ExcuteSkill();

        // 상태 전환이 완벽하게 이뤄졌을 때 "Attack" 애니메이션이 끝났는지 확인
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime % 1 >= 0.95f)
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

