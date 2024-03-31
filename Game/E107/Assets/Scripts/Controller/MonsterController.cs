using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;
using Photon.Pun;

// 몬스터의 audio source component 순서
public enum SoundOrder
{
    DIE = 0,
    ATTACKED,
    LENGTH
}

// 일반 몬스터
public class MonsterController : BaseController
{
    protected MonsterStat _stat;
    protected MonsterInfo _monsterInfo;
    private string _curSkillName;             // 현재 공격의 이름 - Info에서 가져옴
    private float _lastDetectTime;

    [SerializeField]
    protected Transform _detectPlayer;        // 이동 타겟팅, 일반 공격 범위를 벗어나면 랜덤한 플레이어에게 이동 -> 지금은 가까운 플레이어에게 이동
    protected Ray _ray;                       // Gizmos에 사용
    private Renderer[] _allRenderers;         // 캐릭터의 모든 Renderer 컴포넌트 -> 모든 render의 색을 변경!
    private Color[] _originalColors;          // 원래의 머티리얼 색상 저장용 배열
    private AudioSource[] _audioSource;       // MainCamera의 Audio Listener가 필요

    [SerializeField]
    private bool _isDie;

    public MonsterStat Stat { get { return _stat; } }
    public MonsterInfo MonsterInfo { get { return _monsterInfo; } }
    public Transform DetectPlayer { get { return _detectPlayer; } set { _detectPlayer = value; } }
    public AudioSource[] Audio { get => _audioSource; }
    public bool IsDie { get => _isDie; }


    public override void Init()
    {
        _agent.stoppingDistance = 1.0f;
        _agent.angularSpeed = 500.0f;
        _agent.acceleration = 40.0f;
        _statemachine.CurState = new IdleState(this);

        _stat = new MonsterStat(_unitType);
        _monsterInfo = GetComponent<MonsterInfo>();

        // 현재 몬스터의 모든 Renderer 컴포넌트를 찾는다. 각 Renderer의 원래 머티리얼 색상 저장
        _allRenderers = GetComponentsInChildren<Renderer>();
        _originalColors = new Color[_allRenderers.Length];
        for (int i = 0; i < _allRenderers.Length; i++)
        {
            _originalColors[i] = _allRenderers[i].material.color;
        }

        _audioSource = GetComponents<AudioSource>();
        foreach (AudioSource audio in _audioSource)        // audio clip이 none이면 audio에 저장되지 않음
        {
            audio.playOnAwake = false;
            audio.Stop();
        }
    }

    protected void OnDrawGizmos()
    {
        _ray.origin = transform.position;
        Gizmos.color = Color.red;
        if (CurState is IdleState) Gizmos.DrawWireSphere(_ray.origin, _stat.DetectRange);
    }

    #region State Method
    // IDLE
    public override void EnterIdle()
    {
        base.EnterIdle();

        _agent.speed = 0;
        _agent.velocity = Vector3.zero;

        _animator.Play("Idle", -1);
        
        // 마스터 클래스에서만 전송
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient) photonView.RPC("RPC_ChangeIdleState", RpcTarget.Others);
    }
    
    public override void ExcuteIdle()
    {
        base.ExcuteIdle();

        if (PhotonNetwork.IsConnected &&PhotonNetwork.IsMasterClient == false) return;

        if (Time.time - _lastDetectTime >= _stat.DetectTime)
        {
            UpdateDetectPlayer();
            _lastDetectTime = Time.time;
        }
    }
    public override void ExitIdle()
    {
        base.ExitIdle();
        if (CurState is DieState)
        {
            _statemachine.ChangeState(new DieState(this));
        }
    }

    // MOVE
    public override void EnterMove()
    {
        base.EnterMove();
        _agent.speed = _stat.MoveSpeed;
        _agent.velocity = Vector3.zero;

        _animator.CrossFade("Move", 1.0f, -1, 0);
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient) photonView.RPC("RPC_ChangeMoveState", RpcTarget.Others);
    }
    public override void ExcuteMove()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient == false) return;

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
        //if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient == false) return;

        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Vector3 thisToTargetDist = _detectPlayer.position - transform.position;
            Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);
            transform.rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
            photonView.RPC("RPC_ChangeSkillState", RpcTarget.Others);
        }
        else if(!PhotonNetwork.IsConnected)
        {
            Vector3 thisToTargetDist = _detectPlayer.position - transform.position;
            Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);
            transform.rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
        }

        _monsterInfo.Skill.Cast();
    }

    public override void ExcuteSkill()
    {
        base.ExcuteSkill();

        // 상태 전환이 완벽하게 이뤄졌을 때 "Attack" 애니메이션이 끝났는지 확인
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            //if (CurState is DieState)
            //{
            //    _statemachine.ChangeState(new DieState(this));
            //}

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

        _audioSource[(int)SoundOrder.DIE].Play();
        _animator.Play("Die", -1);
        _isDie = true;

        Destroy(gameObject, 3.0f);
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient) photonView.RPC("RPC_ChangeDieState", RpcTarget.Others);
    }
    public override void ExcuteDie()
    {
        base.ExcuteDie();
    }
    public override void ExitDie()
    {
        base.ExitDie();
    }
    #endregion

    #region RPC Method

    [PunRPC]
    void RPC_ChangeIdleState()
    {
        _statemachine.ChangeState(new IdleState(this));
    }
    [PunRPC]
    void RPC_ChangeMoveState()
    {
        _statemachine.ChangeState(new MoveState(this));
    }
    [PunRPC]
    void RPC_ChangeSkillState()
    {
        // ???바꿔야 할지도
        //gameObject.transform.rotation = rotation;
        _statemachine.ChangeState(new SkillState(this));
    }
    [PunRPC]
    void RPC_ChangeDieState()
    {
        _statemachine.ChangeState(new DieState(this));
    }

    #endregion

    protected void UpdateDetectPlayer()
    {
        _detectPlayer = null;

        Collider[] detectedPlayers = Physics.OverlapSphere(transform.position, _stat.DetectRange, 1 << 7);

        //int rand = -1;
        float closeDist = Mathf.Infinity;
        foreach (var player in detectedPlayers)
        {
            float distToPlayer = Vector3.Distance(transform.position, player.transform.position);
            //rand = Random.Range(0, 2);     // 0 또는 1
            //if (rand == 0)
            if (distToPlayer < closeDist)
            {
                closeDist = distToPlayer;
                _detectPlayer = player.transform;
            }
        }

        if (_detectPlayer != null)
        {
            _statemachine.ChangeState(new MoveState(this));
        }
    }

    // DetectPlayer를 바라보는 코드
    protected virtual void ToDetectPlayer(float turnSpeed)
    {
        Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirTarget.normalized, Vector3.up), turnSpeed);
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
        if (PhotonNetwork.IsMasterClient == false) return;
        base.TakeDamage(skillObjectId, damage);

        float lastAttackTime;
        lastAttackTimes.TryGetValue(skillObjectId, out lastAttackTime);

        if (Time.time - lastAttackTime < damageCooldown)
        {
            // 쿨다운 중이므로 피해를 주지 않음
            return;
        }

        StartCoroutine(ChangeColorFromDamage());

        // 보스는 피격음이 없다.
        if (_audioSource.Length == (int)SoundOrder.LENGTH)
        {
            _audioSource[(int)SoundOrder.ATTACKED].Play();
        }

        _stat.Hp -= damage;
        if (_stat.Hp < 0) _stat.Hp = 0;
        lastAttackTimes[skillObjectId] = Time.time; // 해당 공격자의 마지막 공격 시간 업데이트
        photonView.RPC("RPC_MonsterAttacked", RpcTarget.Others, damage);
        if (_stat.Hp <= 0)
        {
            _statemachine.ChangeState(new DieState(this));
        }
    }

    IEnumerator ChangeColorFromDamage()
    {
        foreach (Renderer renderer in _allRenderers)
        {
            renderer.material.color = Color.red;
        }

        // 지정된 시간만큼 기다림
        yield return new WaitForSeconds(0.2f);

        // 모든 Renderer의 머티리얼 색상을 원래 색상으로 복구
        for (int i = 0; i < _allRenderers.Length; i++)
        {
            _allRenderers[i].material.color = _originalColors[i];
        }
    }
    [PunRPC]
    void RPC_MonsterAttacked(int damage)
    {
        MonsterAttacked(damage);

    }

    protected void MonsterAttacked(int damage)
    {
        StartCoroutine(ChangeColorFromDamage());

        // 보스는 피격음이 없다.
        if (_audioSource.Length == (int)SoundOrder.LENGTH)
        {
            _audioSource[(int)SoundOrder.ATTACKED].Play();
        }

        _stat.Hp -= damage;
        if (_stat.Hp < 0) _stat.Hp = 0;
    }
}

