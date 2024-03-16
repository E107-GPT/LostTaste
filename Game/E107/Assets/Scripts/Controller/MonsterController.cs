using System.Collections;
using System.Collections.Generic;
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

public class MonsterController : BaseController
{
    [SerializeField]
    private Transform _detectPlayer;
    [SerializeField]
    private Transform _attackPlayer;

    MonsterStat _stat;

    //private Coroutine updateAttackPlayer;
    private Coroutine checkMonsterState;
    private Ray _ray;

    public NavMeshAgent Agent { private set; get; }
    public Transform DetectPlayer
    {
        set => _detectPlayer = value;
        get => _detectPlayer;
    }
    public Transform AttackPlayer
    {
        set => _attackPlayer = value;
        get => _attackPlayer;
    }

    public override void Init()
    {
        _statemachine.ChangeState(new IdleState(this));
        _agent.stoppingDistance = 1.5f;

        // 만약 TYPE을 controller에서 세팅하면
        // 현재 객체가 DrillDuck인지 Slime인지 판단한다.
        _stat = new MonsterStat(Define.UnitType.DrillDuck);

        checkMonsterState = StartCoroutine(CheckMonsterState());
    }
    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    private void OnDrawGizmos()
    {
        _ray.origin = transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_ray.origin, _stat.AttackRange);
    }

    // 캐릭터에게 물리력을 받아도 밀려나는 가속도로 인해 이동에 방해받지 않는다.
    public void FreezeVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    // 인식 범위 내의 플레이어를 갱신
    private void UpdateDetectPlayer()
    {
        // 범위 내의 Player layer의 객체를 저장
        Collider[] detectPlayers = Physics.OverlapSphere(transform.position, _stat.DetectRange, 1 << 7);
        Collider[] targetPlayers = Physics.OverlapSphere(transform.position, _stat.AttackRange, 1 << 7);
        //Physics.OverlapBoxNonAlloc() 인식하는 범위가 명확하면 메모리를 아낄 수 있기 때문에 더 좋다.

        float minDistAttack = _stat.AttackRange;
        PrintText($"공격 범위내의 플레이어: {targetPlayers.Length}");
        if (targetPlayers.Length > 0)
        {
            for (int i = 0; i < targetPlayers.Length; ++i)
            {
                float dist = Vector3.Distance(transform.position, targetPlayers[i].transform.position);
                if (minDistAttack > dist)
                {
                    minDistAttack = dist;
                    AttackPlayer = targetPlayers[i].gameObject.transform;
                }
            }
        }
        else
        {
            AttackPlayer = null;
        }

        float minDistDetect = _stat.DetectRange;
        PrintText($"인식 범위내의 플레이어: {detectPlayers.Length}");
        if (detectPlayers.Length > 0)
        {
            for (int i = 0; i < detectPlayers.Length; ++i)
            {
                float dist = Vector3.Distance(this.transform.position, detectPlayers[i].transform.position);
                if (minDistDetect > dist)
                {
                    minDistDetect = dist;
                    DetectPlayer = detectPlayers[i].gameObject.transform;
                }
            }
        }
        else
        {
            DetectPlayer = null;
        }

        minDistAttack = _stat.AttackRange;
        minDistDetect = _stat.DetectRange;
    }

    // 공격 범위 내의 플레이어 갱신
    //IEnumerator UpdateAttackPlayer()
    //{
    //    yield return null;

    //    Collider[] attackPlayers = Physics.OverlapSphere(transform.position, _stat.AttackRange, 1 << 7);

    //    float minDist = _stat.AttackRange;
    //    for (int i = 0; i < attackPlayers.Length; i++)
    //    {
    //        Transform target = attackPlayers[i].gameObject.transform;
    //        if (minDist > Vector3.Distance(this.transform.position, target.position))
    //        {
    //            AttackPlayer = target;
    //            Debug.Log("target: " + AttackPlayer);
    //        }
    //    }
    //}

    IEnumerator CheckMonsterState()
    {
        while (_stat.Hp > 0)
        {
            yield return new WaitForSeconds(0.3f);
            //yield return null;

            UpdateDetectPlayer();

            _animator.SetBool("isDie", false);

            if (AttackPlayer != null)
            {
                if (CurState is SkillState) continue;

                _animator.SetBool("isDetect", true);
                _animator.SetBool("isPlayerInAttackRange", true);

                _statemachine.ChangeState(new SkillState(this));
            }
            else if (DetectPlayer != null)
            {
                if (CurState is MoveState) continue;

                _animator.SetBool("isDetect", true);
                _animator.SetBool("isPlayerInAttackRange", false);

                _statemachine.ChangeState(new MoveState(this));
            }
            else
            {
                if (CurState is IdleState) continue;

                _animator.SetBool("isDetect", false);
                _animator.SetBool("isPlayerInAttackRange", false);
                DetectPlayer = null;
                AttackPlayer = null;

                _statemachine.ChangeState(new IdleState(this));
            }

        }

        _animator.SetBool("isDie", true);
        _statemachine.ChangeState(new DieState(this));
    }

    // 현재 객체의 UnitType을 세팅
    

    // IDLE
    public override void EnterIdle()
    {
        base.EnterIdle();
        //_animator.CrossFade("Idle", 0.1f);      // 기본적으로 base layer의 state를 나타냄
        _agent.speed = 0;
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
        //_animator.CrossFade("Move", 1.0f);
        _agent.speed = _stat.MoveSpeed;
    }
    public override void ExcuteMove()
    {
        base.ExcuteMove();
        Vector3 thisToTargetDist = DetectPlayer.position - transform.position;
        Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);

        Quaternion rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
        transform.rotation = rotation;

        //entity.transform.Translate(dirToTarget.normalized * entity.MoveSpeed * Time.deltaTime, Space.World);
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
        //_animator.CrossFade("Attack", 0.1f);
        _agent.speed = 0;
        _agent.velocity = Vector3.zero;
    }
    public override void ExcuteSkill()
    {
        base.ExcuteSkill();
        // 플레이어 바라보기
        Vector3 thisToTargetDist = AttackPlayer.position - transform.position;
        Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);

        Quaternion rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
        transform.rotation = rotation;

        // 만약 휙휙 돌아가는게 어색하면
        // 현재 몬스터가 플레이어를 자연스럽게 바라보도록 천천히 회전함
        // 현재 몬스터 객체가 바라보는 방향에 플레이어가 있는 경우 공격 animation 재생
        // 이 기능을 넣으려면 animation event에서 피격 이벤트가 있어야 함

        // 플레이어 공격시 스탯 변경 이벤트

    }
    public override void ExitSkill()
    {
        base.ExitSkill();
    }

    // DIE
    public override void EnterDie() 
    {
        base.EnterDie();
        //_animator.CrossFade("Die", 0.1f);
        // 스폰에서 몬스터 배열을 통해 null 처리 + destroy
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
