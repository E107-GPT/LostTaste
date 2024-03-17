using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

//public enum MonsterState    // 피격 당하면 공격을 가한 플레이어를 추적하는 기능
//{
//    IDLE = 0,
//    // PATROL,
//    CHASE,
//    ATTACK,
//    DIE,
//    GLOBAL,
//}

// Item을 상속받는 SlimeItem 컴포넌트를 넣는다.


public class MonsterController : BaseController
{
    [SerializeField]
    private Define.UnitType _unitType;


    [SerializeField]
    private GameObject[] _existPlayer;      // 필드 위에 존재하는 플레이어 수
    [SerializeField]    
    private Transform _detectPlayer;        // 일반 공격 범위를 벗어나면 랜덤한 플레이어에게 이동 -> 지금은 가까운 플레이어에게 이동
    [SerializeField]
    private Transform _attackPlayer;        // 일반 공격 타겟팅
    [SerializeField]
    private Transform _targetPlayer;        // 패턴 타겟팅

    // 공격 collider를 사용하기 위함
    // Item을 상속받는 객체 생성
    // 스킬 공격은 event로 처리한다.
    // _agent를 가져와서 speed 값을 애니메이션에 맞춰서 조정한다. SetDestination 이용

    DrillDuckItem _curItem;

    protected MonsterStat _stat;
    //private Coroutine updateAttackPlayer;
    private Coroutine _checkMonsterState;
    private Ray _ray;
    private bool _isDonePattern;
    private float _playAniTime;

    public NavMeshAgent Agent { private set; get; }
    public Transform DetectPlayer { private set; get; }
    public Transform AttackPlayer { private set; get; }
    public Transform TargetPlayer { private set; get; }

    public override void Init()
    {
        _curItem = GetComponentInChildren<DrillDuckItem>();

        _existPlayer = GameObject.FindGameObjectsWithTag("Player");
        PrintText($"플레이어가 {_existPlayer.Length}만큼 필드 위에 존재");

        _statemachine.ChangeState(new IdleState(this));
        _stat = new MonsterStat(_unitType);
        _agent.stoppingDistance = 1.5f;
        _agent.angularSpeed = 500.0f;
        _agent.acceleration = 40.0f;
        _isDonePattern = true;

        _checkMonsterState = StartCoroutine(CheckMonsterState());
        InvokeRepeating("UpdateDectPlayer", 0, 20.0f);      // 0초 후 호출, 20초마다 이동 타겟팅 수정
    }
    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    private void OnDrawGizmos()
    {
        Gizmos attackRangeGizmo = new Gizmos();
        Gizmos targetRangeGizmo = new Gizmos();

        _ray.origin = transform.position;
        Gizmos.color = Color.red;
        if (CurState is SkillState) Gizmos.DrawWireSphere(_ray.origin, _stat.AttackRange);
        if (CurState is MoveState) Gizmos.DrawWireSphere(_ray.origin, _stat.TargetRange);
    }

    // 캐릭터에게 물리력을 받아도 밀려나는 가속도로 인해 이동에 방해받지 않는다.
    public void FreezeVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    // 보스 패턴을 위한 타겟팅
    // 애니메이션 event 함수로 옮기자.
    private void UpdateTargetPlayer()
    {
        Collider[] targetPlayers = Physics.OverlapSphere(transform.position, _stat.TargetRange, 1 << 7);

        // 패턴 타겟팅 조건 추가
        PrintText($"패턴 공격 범위내의 플레이어: {targetPlayers.Length}");
        if (targetPlayers.Length > 0 )
        {
            for (int i = 0; i < targetPlayers.Length; ++i)
            {
                // float dist = Vector3.Distance(transform.position, targetPlayers[i].transform.position);
                _targetPlayer = targetPlayers[i].transform;
                PrintText($"{_targetPlayer.gameObject.name}");
            }
        }
    }

    // 이동 타겟팅 기능
    private void UpdateDectPlayer()
    {
        int rand = -1;
        DetectPlayer = _existPlayer[0].transform;
        for (int i = 1; i < _existPlayer.Length; ++i)
        {
            rand = Random.Range(0, 2);     // 0 또는 1
            if (rand == 0)
            {
                DetectPlayer = _existPlayer[i].transform;
            }
            
        }

    }

    // 공격 범위 내의 플레이어 갱신
    private void UpdateAttackPlayer()
    {
        Collider[] attackPlayers = Physics.OverlapSphere(transform.position, _stat.AttackRange, 1 << 7);

        //float minDistAttack = _stat.AttackRange;
        PrintText($"공격 범위내의 플레이어: {attackPlayers.Length}");
        if (attackPlayers.Length > 0)
        {
            for (int i = 0; i < attackPlayers.Length; ++i)
            {
                AttackPlayer = attackPlayers[i].gameObject.transform;
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
            AttackPlayer = null;
        }

        //minDistAttack = _stat.AttackRange;
    }

    private float delay = 0f;
    private void DrillDuckPatternAttack()
    {
        delay++;
        if (delay > _stat.AttackDelay)      // 이 함수가 AttackDelay만큼 호출되면 패턴 수행
        {
            if (_targetPlayer != null)
            {
                _isDonePattern = false;
                delay = 0;

                _statemachine.ChangeState(new DrillDuckSlideState(this));
            }
        }
    }

    IEnumerator CheckMonsterState()
    {
        while (_stat.Hp > 0)
        {
            yield return new WaitForSeconds(0.3f);

            UpdateAttackPlayer();
            UpdateTargetPlayer();

            // Hp가 70% 이하라면 일정 시간마다 패턴 공격
            //if ((_unitType is Define.UnitType.DrillDuck) && (_stat.Hp <= _stat.MaxHp * 0.7) && _isDonePattern == true)   // (_stat.Hp <= _stat.MaxHp * 0.7)
            //{
            //    if (CurState is DrillDuckSlideState) continue;
            //    DrillDuckPatternAttack();
            //}

            if (AttackPlayer != null)
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
                DetectPlayer = null;
                AttackPlayer = null;

                _statemachine.ChangeState(new IdleState(this));
            }
        }

        _statemachine.ChangeState(new DieState(this));
        CancelInvoke("UpdateDectPlayer");
        // 코루틴 종료
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

        _agent.SetDestination(DetectPlayer.position);
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
        _curItem.NormalAttack();

        _animator.CrossFade("Attack", 0.3f);
    }
    public override void ExcuteSkill()
    {
        base.ExcuteSkill();

        Vector3 thisToTargetDist = AttackPlayer.position - transform.position;
        Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);
        // Quaternion rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirToTarget.normalized, Vector3.up), 0.5f);

        // _attackPlayer = null;       // Item은 코루틴에 맞추기 때문에 Skill 상태를 유지하면 안 된다.
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

    // DrillDuck - Slide
    public override void EnterSlide()
    {
        base.EnterSlide();
        _agent.ResetPath();
        _agent.SetDestination(transform.position);  
        //_agent.speed = _stat.MoveSpeed * 2.0f;
        _agent.velocity = Vector3.zero;
        _isDonePattern = false;

        //_animator.speed = 0.5f;
        _animator.CrossFade("Slide", 0.5f);
    }
    public override void ExcuteSlide()
    {
        base.ExcuteSlide();

        Vector3 thisToTargetDist = _targetPlayer.position - transform.position;
        Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);

        transform.Translate(thisToTargetDist.normalized * _stat.MoveSpeed * 2.0f * Time.deltaTime);

        //_agent.SetDestination(dirToTarget);     // 해당 방향으로 이동
    }
    public override void ExitSlide()
    {
        base.ExitSlide();
        //_animator.speed = 1f;
        //_agent.speed = _stat.MoveSpeed;
        _isDonePattern = true;
    }
}

