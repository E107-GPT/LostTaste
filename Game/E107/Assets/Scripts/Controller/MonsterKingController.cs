using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingController : MonsterController
{
    [SerializeField]
    private GameObject _weapon;
    [SerializeField]
    private GameObject _leftArm;

    private ParticleSystem _particle;
    private Coroutine _hitDownStart;
    private Coroutine _slashStart;

    private Vector3 _detectPlayerLoc;
    private float _jumpCoolDown;
    private float _jumpLastTime;

    private Vector3 _tracePosition;

    public GameObject Weapon { get => _weapon; }
    public GameObject LeftArm { get => _leftArm; }
    public ParticleSystem Particle { get => _particle; set => _particle = value; }

    public override void Init()
    {
        base.Init();

        _stat = new MonsterStat(_unitType);
        _jumpCoolDown = 15;
    }

    protected override void ChangeStateFromMove()
    {
        float distToDetectPlayer = (transform.position - _detectPlayer.position).magnitude;

        _agent.SetDestination(_detectPlayer.position);
        
        if (distToDetectPlayer <= _stat.AttackRange)
        {
            // phase
            if (_stat.Hp <= _stat.MaxHp / 2)
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
            _statemachine.ChangeState(new MonsterKingStabState(this));
        }
        else if (rand <= 60)
        {
            _statemachine.ChangeState(new MonsterKingHitDownState(this));
        }
        else if (rand <= 100)
        {
            _statemachine.ChangeState(new MonsterKingSlashState(this));
        }
    }

    IEnumerator ChargeEffect(Define.Effect effectName, Transform root, float seconds)
    {
        _particle = Managers.Effect.Play(effectName, root);
        
        yield return new WaitForSeconds(seconds);

        if (_particle != null) Managers.Effect.Stop(_particle);
    }

    #region State Method
    public override void EnterMonsterKingHitDownState()         // HitDown
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;

        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dirTarget.normalized, Vector3.up);
            photonView.RPC("RPC_ChangeMonsterKingHitDownState", RpcTarget.Others);
        }
        // 둘 다 똑같음
        //ToDetectPlayer(0.8f);
        //Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirTarget.normalized, Vector3.up), 0.8f);

        _animator.CrossFade("HitDown", 0.3f, -1, 0);
    }      
    public override void ExecuteMonsterKingHitDownState() 
    {
        if (CurState is DieState) return;

        _animator.SetFloat("HitDownSpeed", 0.1f);
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("HitDown"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime <= 0.4f)
            {
                _animator.SetFloat("HitDownSpeed", 0.41f);
                if (_hitDownStart == null) _hitDownStart = StartCoroutine(ChargeEffect(Define.Effect.KingHitDownStartEffect, _weapon.transform, 1.8f));
            }
            else if (aniTime > 0.4f && _hitDownStart != null)
            {
                _animator.SetFloat("HitDownSpeed", 1.0f);
                if (_hitDownStart != null)
                {
                    StopCoroutine(_hitDownStart);
                    _hitDownStart = null;
                    if (_particle != null) Managers.Effect.Stop(_particle);
                }
            }
            else if (aniTime < 0.53f)
            {
                _animator.SetFloat("HitDownSpeed", 1.0f);
            }
            else if (aniTime < 0.8f)
            {
                _animator.SetFloat("HitDownSpeed", 1.0f);
                _monsterInfo.Patterns[0].SetCollider(_stat.AttackDamage);       // 내려찍기
            }
            else if (aniTime <= 1.0f)
            {
                _animator.SetFloat("HitDownSpeed", 1.0f);
            }    
            else if (aniTime >= 1.0f)
            {
                _monsterInfo.Patterns[0].DeActiveCollider();
                _monsterInfo.Patterns[6].SetCollider(_stat.AttackDamage);       // 후속타
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitMonsterKingHitDownState() 
    {
        _monsterInfo.Patterns[6].DeActiveCollider();
    }

    public override void EnterMonsterKingSlashState()           // Slash
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;

        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dirTarget.normalized, Vector3.up);
            photonView.RPC("RPC_ChangeMonsterKingSlashState", RpcTarget.Others);
        }
        //ToDetectPlayer(0.8f);

        _animator.CrossFade("Slash", 0.3f, -1, 0);
    }
    public override void ExecuteMonsterKingSlashState() 
    {
        if (CurState is DieState) return;

        _animator.SetFloat("SlashSpeed", 0.1f);
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime <= 0.4f)        // 공격전 
            {
                _animator.SetFloat("SlashSpeed", 0.38f);
                if (_slashStart == null)    _slashStart = StartCoroutine(ChargeEffect(Define.Effect.KingSlashStartEffect, transform, 2.0f));
                if (_particle != null)      _particle.transform.parent = transform;
            }
            else if (aniTime > 0.4f && _slashStart != null)
            {
                Managers.Sound.Play("Monster/KingSlashSwordEffect", Define.Sound.Effect);
                _animator.SetFloat("SlashSpeed", 1.0f);
                StopCoroutine(_slashStart);
                _slashStart = null;
                if (_particle != null)
                    Managers.Effect.Stop(_particle);
            }
            else if (aniTime <= 0.7f)   // 칼을 휘두르는 중
            {
                _animator.SetFloat("SlashSpeed", 1.0f);
                _monsterInfo.Patterns[1].SetCollider(_stat.PatternDamage);
            }
            else if (aniTime <= 1.0f)
            {
                _animator.SetFloat("SlashSpeed", 1.0f);
            }
            else if (aniTime > 1.0f)
            {
                _monsterInfo.Patterns[1].DeActiveCollider();
                _statemachine.ChangeState(new IdleState(this));
            }
        }  
    }
    public override void ExitMonsterKingSlashState() 
    {
    }

    public override void EnterMonsterKingStabState()            // Stab
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;

        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dirTarget.normalized, Vector3.up);
            photonView.RPC("RPC_ChangeMonsterKingStabState", RpcTarget.Others);
        }
        //ToDetectPlayer(0.8f);

        _animator.CrossFade("Stab", 0.3f, -1, 0);
    }         
    public override void ExecuteMonsterKingStabState() 
    {
        if (CurState is DieState) return;

        _animator.SetFloat("StabSpeed", 0.1f);
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Stab"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime <= 0.2f)        // 공격 준비
            {
                _animator.SetFloat("StabSpeed", 0.39f);
                _monsterInfo.Patterns[2].SetCollider(_stat.PatternDamage);
            }
            else if (aniTime <= 0.4f)   // 공격 진행
            {
                _animator.SetFloat("StabSpeed", 0.8f);
                _monsterInfo.Patterns[2].DeActiveCollider();
                _monsterInfo.Patterns[3].SetCollider(_stat.PatternDamage);
            }
            else if (aniTime <= 0.52f)  // 공격 후 뒷걸음질 전
            {
                _monsterInfo.Patterns[3].DeActiveCollider();
                _animator.SetFloat("StabSpeed", 1.0f);
            }
            else if (aniTime <= 0.58f)  // 뒷걸음질 시작
            {
                _animator.SetFloat("StabSpeed", 1.0f);
            }
            else if (aniTime <= 0.8f)   // 원위치로 이동
            {
                _animator.SetFloat("StabSpeed", 1.0f);
            }
            else if (aniTime <= 1.0f)
            {
                _animator.SetFloat("StabSpeed", 1.0f);
            }
            else if (aniTime > 1.0f)
            {
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitMonsterKingStabState() { }

    public override void EnterMonsterKingJumpStartState()       // JumpStart
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            //Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            //transform.rotation = Quaternion.LookRotation(dirTarget.normalized, Vector3.up);
            photonView.RPC("RPC_ChangeMonsterKingJumpStartState", RpcTarget.Others);
        }

        _animator.CrossFade("JumpStart", 0.3f, -1, 0);
    }    
    public override void ExecuteMonsterKingJumpStartState() 
    {
        if (CurState is DieState) return;

        _animator.SetFloat("JumpStartSpeed", 0.1f);
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("JumpStart"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            //PrintText($"{aniTime}");

            if (aniTime <= 0.4f)        // 점프 준비
            {
                _animator.SetFloat("JumpStartSpeed", 0.2f);
            }
            else if (aniTime <= 0.6f)   // 피격 판정
            {
                _animator.SetFloat("JumpStartSpeed", 1.0f);
                _monsterInfo.Patterns[4].SetCollider(_stat.PatternDamage);
            }
            else if (aniTime <= 1.0f)
            {
                _animator.SetFloat("JumpStartSpeed", 1.0f);
            }
            else if (aniTime > 1.0f)
            {
                _monsterInfo.Patterns[4].DeActiveCollider();
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
        _monsterInfo.Patterns[5].SetCollider(_stat.PatternDamage);

        _animator.CrossFade("JumpEnd", 0.3f, -1, 0);
    }      
    public override void ExecuteMonsterKingJumpEndState() 
    {
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("JumpEnd"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime > 1.0f)
            {
                _monsterInfo.Patterns[5].DeActiveCollider();
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitMonsterKingJumpEndState() 
    {
        _jumpLastTime = Time.time;
        GetComponent<Collider>().enabled = true;
    }

    #endregion


    #region RPC
    [PunRPC]
    void RPC_ChangeMonsterKingHitDownState()
    {
        _statemachine.ChangeState(new MonsterKingHitDownState(this));
    }
    [PunRPC]
    void RPC_ChangeMonsterKingSlashState()
    {
        _statemachine.ChangeState(new MonsterKingSlashState(this));
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
        // ???�ٲ�� ������
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
    #endregion
}
