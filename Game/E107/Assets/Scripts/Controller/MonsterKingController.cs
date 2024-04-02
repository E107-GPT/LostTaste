using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingController : MonsterController
{
    [SerializeField] private GameObject _weapon;
    [SerializeField] private GameObject _leftArm;
    [SerializeField] private GameObject _rightArm;

    private ParticleSystem _particle;

    private Vector3 _detectPlayerLoc;
    private float _jumpCoolDown;
    private float _jumpLastTime;

    //private Vector3 _tracePosition;

    public GameObject Weapon { get => _weapon; }
    public GameObject LeftArm { get => _leftArm; }
    public GameObject RightArm { get => _rightArm; }
    public ParticleSystem Particle { get => _particle; set => _particle = value; }

    public override void Init()
    {
        base.Init();

        _stat = new MonsterStat(_unitType);
        _jumpCoolDown = 13;      // 15
        //_stat.Hp = _stat.MaxHp / 2;
    }

    protected override void ChangeStateFromMove()
    {
        if (_detectPlayer == null)
        {
            _statemachine.ChangeState(new IdleState(this));
            return;
        }
        float distToDetectPlayer = (transform.position - _detectPlayer.position).magnitude;

        _agent.SetDestination(_detectPlayer.position);
        
        if (distToDetectPlayer <= _stat.AttackRange)
        {
            // phase
            if (_stat.Hp <= _stat.MaxHp / 1.6f)     // 3125
            {
                PhaseTwePatternSelector(); 
            }
            else if (_stat.Hp <= _stat.MaxHp)
            {
                PhaseOnePatternSelector();
            }
            
        }
        else if (distToDetectPlayer > _stat.DetectRange)
        {
            _detectPlayer = null;
            _statemachine.ChangeState(new IdleState(this));
        }
    }

    private void PhaseTwePatternSelector()
    {
        if (Time.time - _jumpLastTime >= _jumpCoolDown)
        {
            _statemachine.ChangeState(new MonsterKingJumpStartState(this));
        }
        else
        {
            PhaseOnePatternSelector();
        }
    }

    private void PhaseOnePatternSelector()
    {
        int rand = Random.Range(0, 101);
        if (rand <= 20)
        {
            _statemachine.ChangeState(new MonsterKingStabChargeState(this));
        }
        else if (rand <= 60)
        {
            _statemachine.ChangeState(new MonsterKingHitDownChargeState(this));
        }
        else if (rand <= 100)
        {
            _statemachine.ChangeState(new MonsterKingSlashChargeState(this));
        }
    }

    #region State Method
    public override void EnterMonsterKingHitDownChargeState()   // HitDown Charge
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;
        _agent.avoidancePriority = 1;

        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dirTarget.normalized, Vector3.up);
            photonView.RPC("RPC_ChangeMonsterKingHitDownChargeState", RpcTarget.Others);
        }

        _animator.SetFloat("HitDownChargeSpeed", 0.4f);
        _animator.CrossFade("HitDownCharge", 0.3f, -1, 0);
        _monsterInfo.Patterns[0].SetCollider();
    }
    public override void ExecuteMonsterKingHitDownChargeState()
    {
        if (CurState is DieState)
        {
            _statemachine.ChangeState(new DieState(this));
        }

        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("HitDownCharge"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime >= 1.0f)
            {
                _statemachine.ChangeState(new MonsterKingHitDownState(this));
            }
        }
    }
    public override void ExitMonsterKingHitDownChargeState()
    {
    }

    public override void EnterMonsterKingHitDownState()         // HitDown
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ChangeMonsterKingHitDownState", RpcTarget.Others);
        }

        _animator.SetFloat("HitDownSpeed", 1.0f);
        _animator.CrossFade("HitDown", 0.02f, -1, 0);
        _monsterInfo.Patterns[1].SetCollider(_stat.PatternDamage - 10);
    }      
    public override void ExecuteMonsterKingHitDownState() 
    {
        if (CurState is DieState)
        {
            _statemachine.ChangeState(new DieState(this));
        }
        
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("HitDown"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime >= 1.0f)
            {
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitMonsterKingHitDownState() 
    {
        _agent.avoidancePriority = 50;
    }

    public override void EnterMonsterKingSlashChargeState()     // Slash Charge
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;
        _agent.avoidancePriority = 1;

        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dirTarget.normalized, Vector3.up);
            photonView.RPC("RPC_ChangeMonsterKingSlashChargeState", RpcTarget.Others);
        }

        _animator.SetFloat("SlashChargeSpeed", 0.3f);
        _animator.CrossFade("SlashCharge", 0.1f, -1, 0);
        _monsterInfo.Patterns[2].SetCollider();
    }
    public override void ExecuteMonsterKingSlashChargeState()
    {
        if (CurState is DieState)
        {
            _statemachine.ChangeState(new DieState(this));
        }

        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("SlashCharge"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            
            if (aniTime >= 1.0f)
            {
                _statemachine.ChangeState(new MonsterKingSlashState(this)); 
            }
        }
    }
    public override void ExitMonsterKingSlashChargeState()
    {
        _monsterInfo.Patterns[2].DeActiveCollider();
    }

    public override void EnterMonsterKingSlashState()           // Slash
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ChangeMonsterKingSlashState", RpcTarget.Others);
        }

        _animator.SetFloat("SlashSpeed", 1.0f);
        _animator.CrossFade("Slash", 0.1f, -1, 0);
        _monsterInfo.Patterns[3].SetCollider(_stat.PatternDamage - 5);
    }
    public override void ExecuteMonsterKingSlashState() 
    {
        if (CurState is DieState)
        {
            _statemachine.ChangeState(new DieState(this));
        }

        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime >= 1.0f)
            {
                _statemachine.ChangeState(new IdleState(this));
            }
        }  
    }
    public override void ExitMonsterKingSlashState() 
    {
        _agent.avoidancePriority = 50;
    }

    public override void EnterMonsterKingStabChargeState()      // Stab Charge
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;
        _agent.avoidancePriority = 1;

        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dirTarget.normalized, Vector3.up);
            photonView.RPC("RPC_ChangeMonsterKingStabChargeState", RpcTarget.Others);
        }

        _animator.SetFloat("StabChargeSpeed", 0.3f);
        _animator.CrossFade("StabCharge", 0.3f, -1, 0);
        _monsterInfo.Patterns[4].SetCollider(_stat.PatternDamage);
    }

    public override void ExecuteMonsterKingStabChargeState()
    {
        if (CurState is DieState)
        {
            _statemachine.ChangeState(new DieState(this));
        }


        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("StabCharge"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime >= 1.0f)
            {
                _statemachine.ChangeState(new MonsterKingStabState(this));
            }
        }
    }

    public override void ExitMonsterKingStabChargeState()
    {
    }

    public override void EnterMonsterKingStabState()            // Stab
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ChangeMonsterKingStabState", RpcTarget.Others);
        }

        _animator.SetFloat("StabSpeed", 1.0f);
        _animator.CrossFade("Stab", 0.3f, -1, 0);
        _monsterInfo.Patterns[5].SetCollider(_stat.PatternDamage - 15);
    }         
    public override void ExecuteMonsterKingStabState() 
    {
        if (CurState is DieState)
        {
            _statemachine.ChangeState(new DieState(this));
        }

        
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Stab"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            
            if (aniTime >= 1.0f)
            {
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitMonsterKingStabState() 
    {
        _agent.avoidancePriority = 50;
    }

    public override void EnterMonsterKingJumpStartState()       // JumpStart
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;
        _agent.avoidancePriority = 1;

        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            //Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            //transform.rotation = Quaternion.LookRotation(dirTarget.normalized, Vector3.up);
            photonView.RPC("RPC_ChangeMonsterKingJumpStartState", RpcTarget.Others);
        }

        _animator.SetFloat("JumpStartSpeed", 0.6f);
        _animator.CrossFade("JumpStart", 0.3f, -1, 0);
        _monsterInfo.Patterns[6].SetCollider(_stat.PatternDamage - 30);
    }    
    public override void ExecuteMonsterKingJumpStartState() 
    {
        if (CurState is DieState)
        {
            _statemachine.ChangeState(new DieState(this));
        }

        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("JumpStart"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime >= 1.0f)
            {
                _statemachine.ChangeState(new MonsterKingJumpAirState(this));
            }
        }
    }
    public override void ExitMonsterKingJumpStartState()
    {
        // 이동
        GetComponent<Collider>().enabled = false;
        Agent.Warp(new Vector3(transform.position.x, transform.position.y + 100.0f, transform.position.z));
    }


    private IEnumerator CheckParticleAndChangeState(float duration)
    {
        yield return new WaitForSeconds(duration);
        _statemachine.ChangeState(new MonsterKingJumpEndState(this));
    }

    public override void EnterMonsterKingJumpAirState()         // JumpAir
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ChangeMonsterKingJumpAirState", RpcTarget.Others);
            photonView.RPC("RPC_JumpTrace", RpcTarget.Others, DetectPlayer.transform.position);
            _particle = Managers.Effect.Play(Define.Effect.KingJumpAirEffect, DetectPlayer.transform);
            Managers.Sound.Play("Monster/KingJumpAirEffect", Define.Sound.Effect);
            StartCoroutine(CheckParticleAndChangeState(_particle.main.duration));
        }
        else
        {
            // 테스트를 위함
            // Managers.Sound.Play("Monster/KingJumpAirEffect", Define.Sound.Effect);
            _particle = Managers.Effect.Play(Define.Effect.KingJumpAirEffect, new GameObject().transform);  // 이 부분만 남겼음
            // StartCoroutine(CheckParticleAndChangeState(_particle.main.duration));
            PrintText("PhotonNetwork 연결 필요!");
        }
        
        
    }
    public override void ExecuteMonsterKingJumpAirState() 
    {
        // 애니메이션은 필요 없음
        if(PhotonNetwork.IsMasterClient)
        {
            _particle.transform.position = DetectPlayer.transform.position;
            photonView.RPC("RPC_JumpTrace", RpcTarget.Others, DetectPlayer.transform.position);
        }
        else
        {
            //Debug.Log($"{_particle.transform.position} {_tracePosition}");
            //_particle.transform.position = _tracePosition;
        }

    }
    public override void ExitMonsterKingJumpAirState() 
    {
        _detectPlayerLoc = _particle.transform.position;
        Managers.Effect.Stop(_particle);
    }

    public override void EnterMonsterKingJumpEndState()         // JumpEnd
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ChangeMonsterKingJumpEndState", RpcTarget.Others);

        }
        // 착지
        _agent.Warp(new Vector3(_detectPlayerLoc.x, _detectPlayerLoc.y, _detectPlayerLoc.z));
        _monsterInfo.Patterns[7].SetCollider(_stat.PatternDamage + 20);

        _animator.CrossFade("JumpEnd", 0.3f, -1, 0);
    }      
    public override void ExecuteMonsterKingJumpEndState() 
    {
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("JumpEnd"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime > 1.0f)
            {
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitMonsterKingJumpEndState() 
    {
        _jumpLastTime = Time.time;
        GetComponent<Collider>().enabled = true;
        _agent.avoidancePriority = 50;
    }

    #endregion


    #region RPC
    [PunRPC]
    void RPC_ChangeMonsterKingHitDownChargeState()
    {
        _statemachine.ChangeState(new MonsterKingHitDownChargeState(this));
    }
    [PunRPC]
    void RPC_ChangeMonsterKingHitDownState()
    {
        _statemachine.ChangeState(new MonsterKingHitDownState(this));
    }
    [PunRPC]
    void RPC_ChangeMonsterKingSlashChargeState()
    {
        _statemachine.ChangeState(new MonsterKingSlashChargeState(this));
    }
    [PunRPC]
    void RPC_ChangeMonsterKingSlashState()
    {
        _statemachine.ChangeState(new MonsterKingSlashState(this));
    }
    [PunRPC]
    void RPC_ChangeMonsterKingStabChargeState()
    {
        _statemachine.ChangeState(new MonsterKingStabChargeState(this));
    }
    [PunRPC]
    void RPC_ChangeMonsterKingStabState()
    {
        _statemachine.ChangeState(new MonsterKingStabState(this));
    }
    [PunRPC]
    void RPC_ChangeMonsterKingJumpStartState()
    {
        _statemachine.ChangeState(new MonsterKingJumpStartState(this));
    }
    [PunRPC]
    void RPC_ChangeMonsterKingJumpAirState()
    {
        _statemachine.ChangeState(new MonsterKingJumpAirState(this));
    }
    [PunRPC]
    void RPC_ChangeMonsterKingJumpEndState()
    {
        _statemachine.ChangeState(new MonsterKingJumpEndState(this));
    }
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
        _statemachine.ChangeState(new SkillState(this));
    }
    [PunRPC]
    void RPC_ChangeDieState()
    {
        _statemachine.ChangeState(new DieState(this));
    }

    [PunRPC]
    void RPC_JumpTrace(Vector3 _pos)
    {
        //_tracePosition = _pos;
        Debug.Log(_pos);
        _particle.transform.position = _pos;
    }

    [PunRPC]
    void RPC_MonsterAttacked(int damage)
    {
        MonsterAttacked(damage);

    }


    #endregion
}
