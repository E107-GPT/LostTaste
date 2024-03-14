using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState    // 피격 당하면 공격을 가한 플레이어를 추적하는 기능
{
    IDLE = 0,
    // PATROL,
    CHASE,
    ATTACK,
    DIE,
    GLOBAL,
}

public class MonsterController : BaseController
{
    [SerializeField]
    private Transform _detectPlayer;
    [SerializeField]
    private Transform _attackPlayer;

    MonsterStat _stat;

    private Coroutine updateDetectPlayer;
    private Coroutine updateAttackPlayer;
    private Coroutine checkMonsterState;

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
        _stat = gameObject.GetOrAddComponent<MonsterStat>();
        _statemachine.ChangeState(new IdleState(this));
        _agent.stoppingDistance = 1.5f;
        checkMonsterState = StartCoroutine(CheckMonsterState());
    }
    //public override void Setup(string name)
    //{
    //    //updateAttackPlayer = StartCoroutine(UpdateAttackPlayer());
    //    //updateDetectPlayer = StartCoroutine(UpdateDetectPlayer());
        
    //}

    private void FixedUpdate()
    {
        FreezeVelocity();
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
        if (targetPlayers.Length > 0)
        {
            for (int i = 0; i < targetPlayers.Length; ++i)
            {
                float dist = Vector3.Distance(this.transform.position, targetPlayers[i].transform.position);
                PrintText($"dist: {dist}");
                PrintText($"공격 사정거리 내의 플레이어를 {targetPlayers.Length}만큼 인식");
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
        if (detectPlayers.Length > 0)
        {
            for (int i = 0; i < detectPlayers.Length; ++i)
            {
                float dist = Vector3.Distance(this.transform.position, detectPlayers[i].transform.position);
                PrintText($"플레이어를 {detectPlayers.Length}만큼 인식");
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
    IEnumerator UpdateAttackPlayer()
    {
        yield return null;

        Collider[] attackPlayers = Physics.OverlapSphere(transform.position, _stat.AttackRange, 1 << 7);

        float minDist = _stat.AttackRange;
        for (int i = 0; i < attackPlayers.Length; i++)
        {
            Transform target = attackPlayers[i].gameObject.transform;
            if (minDist > Vector3.Distance(this.transform.position, target.position))
            {
                AttackPlayer = target;
                Debug.Log("target: " + AttackPlayer);
            }
        }
    }

    // Monster State를 바꿔주는 함수
    IEnumerator CheckMonsterState()
    {
        while (_stat.Hp > 0)
        {
            // yield return new WaitForSeconds(0.3f);
            yield return null;

            UpdateDetectPlayer();

            _animator.SetBool("isDie", false);

            if (AttackPlayer != null)
            {
                //if (curState == MonsterState.ATTACK) continue;
                if (CurState is SkillState) continue;

                _animator.SetBool("isDetect", true);
                _animator.SetBool("isPlayerInAttackRange", true);
                DetectPlayer = null;
                //ChangeState(MonsterState.ATTACK);
                _statemachine.ChangeState(new SkillState(this));
            }
            else if (DetectPlayer != null)
            {
                //if (curState == MonsterState.CHASE) continue;
                if (CurState is MoveState) continue;

                _animator.SetBool("isDetect", true);
                _animator.SetBool("isPlayerInAttackRange", false);
                AttackPlayer = null;
                //ChangeState(MonsterState.CHASE);
                _statemachine.ChangeState(new MoveState(this));
            }
            else
            {
                //if (curState == MonsterState.IDLE) continue;
                if (CurState is IdleState) continue;

                _animator.SetBool("isDetect", false);
                _animator.SetBool("isPlayerInAttackRange", false);
                DetectPlayer = null;
                AttackPlayer = null;
                //ChangeState(MonsterState.IDLE);
                _statemachine.ChangeState(new IdleState(this));
            }

        }


        _animator.SetBool("isDie", true);
        //ChangeState(MonsterState.DIE);
        _statemachine.ChangeState(new DieState(this));
    }
}