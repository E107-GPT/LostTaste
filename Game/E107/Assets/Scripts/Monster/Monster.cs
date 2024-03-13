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

public class Monster : EnemyBaseEntity
{
    [SerializeField]
    private Transform _detectPlayer;
    [SerializeField]
    private Transform _attackPlayer;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private NavMeshAgent _agent;

    private int _level;         // 스테이지 올라가면 몬스터도 강해지는 경우 필요
    [SerializeField]
    private int _hp;
    private int _maxHp;
    private int _damage;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private float _detectRange;

    private State<Monster>[] states;     // Monster의 모든 상태 정보
    private StateMachine<Monster> stateMachine;     // 상태 관리를 StateMachine에 위임

    private Coroutine updateDetectPlayer;
    private Coroutine updateAttackPlayer;
    private Coroutine checkMonsterState;

    public MonsterState curState { private set; get; }  // 현재 상태, Global State와 prievState를 대비
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
    public int Level
    {
        set => _level = value;
        get => _level;
    }
    public int Hp
    {
        set => _hp = value;
        get => _hp;
    }
    public int MaxHp
    {
        set => _maxHp = value;
        get => _maxHp;
    }
    public int Damage
    {
        set => _damage = value;
        get => _damage;
    }
    public float MoveSpeed
    {
        set => _moveSpeed = value;
        get => _moveSpeed;
    }
    public float AttackRange
    {
        set => _attackRange = value;
        get => _attackRange;
    }
    public float DetectRange
    {
        set => _detectRange = value;
        get => _detectRange;
    }

    public override void Setup(string name)
    {
        base.Setup(name);

        gameObject.name = $"{ID:D2}_Monster_{name}";    // 00_Monster_name 으로 hierarchy 창에서 보임

        states = new State<Monster>[5];
        states[(int)MonsterState.IDLE] = new MonsterStateItem.IDLE();
        states[(int)MonsterState.CHASE] = new MonsterStateItem.CHASE();
        states[(int)MonsterState.ATTACK] = new MonsterStateItem.ATTACK();
        states[(int)MonsterState.DIE] = new MonsterStateItem.DIE();
        states[(int)MonsterState.GLOBAL] = new MonsterStateItem.StateGlobal();

        // stateMachine 초기화
        stateMachine = new StateMachine<Monster>();
        stateMachine.Setup(this, states[(int)MonsterState.IDLE]);
        stateMachine.SetGlobalState(states[(int)MonsterState.GLOBAL]);  // 전역 상태 설정

        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();

        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _damage = 5;
        _moveSpeed = 2.0f;
        _attackRange = 1.8f;
        _agent.stoppingDistance = 1.5f;
        _detectRange = 15.0f;

        //updateAttackPlayer = StartCoroutine(UpdateAttackPlayer());
        //updateDetectPlayer = StartCoroutine(UpdateDetectPlayer());
        checkMonsterState = StartCoroutine(CheckMonsterState());
    }

    public override void Updated()
    {

        // stateMachine 실행
        stateMachine.Execute();
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
        Collider[] detectPlayers = Physics.OverlapSphere(transform.position, DetectRange, 1 << 7);
        Collider[] targetPlayers = Physics.OverlapSphere(transform.position, AttackRange, 1 << 7);
        //Physics.OverlapBoxNonAlloc() 인식하는 범위가 명확하면 메모리를 아낄 수 있기 때문에 더 좋다.

        float minDistAttack = AttackRange;
        if (targetPlayers.Length > 0)
        {
            for (int i = 0; i < targetPlayers.Length; ++i)
            {
                float dist = Vector3.Distance(this.transform.position, targetPlayers[i].transform.position);
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

        float minDistDetect = DetectRange;
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

        minDistAttack = AttackRange;
        minDistDetect = DetectRange;
    }

    // 공격 범위 내의 플레이어 갱신
    IEnumerator UpdateAttackPlayer()
    {
        yield return null;

        Collider[] attackPlayers = Physics.OverlapSphere(transform.position, AttackRange, 1 << 7);

        float minDist = AttackRange;
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
        while (Hp > 0)
        {
            // yield return new WaitForSeconds(0.3f);
            yield return null;

            UpdateDetectPlayer();

            _animator.SetBool("isDie", false);

            if (AttackPlayer != null)
            {
                if (curState == MonsterState.ATTACK) continue;

                _animator.SetBool("isDetect", true);
                _animator.SetBool("isPlayerInAttackRange", true);
                ChangeState(MonsterState.ATTACK);
            }
            else if (DetectPlayer != null)
            {
                if (curState == MonsterState.CHASE) continue;

                _animator.SetBool("isDetect", true);
                _animator.SetBool("isPlayerInAttackRange", false);
                ChangeState(MonsterState.CHASE);
            }
            else
            {
                if (curState == MonsterState.IDLE) continue;

                _animator.SetBool("isDetect", false);
                _animator.SetBool("isPlayerInAttackRange", false);
                DetectPlayer = null;
                AttackPlayer = null;
                ChangeState(MonsterState.IDLE);
            }

        }


        _animator.SetBool("isDie", true);
        ChangeState(MonsterState.DIE);
    }

    public void ChangeState(MonsterState newState)
    {
        // 새로 바뀌는 상태를 저장
        curState = newState;

        // stateMachine에 상태 변경을 위임
        stateMachine.ChangeState(states[(int)newState]);
    }

    public void RevertToPreviousState()
    {
        stateMachine.RevertToPreviousState();
    }
}
