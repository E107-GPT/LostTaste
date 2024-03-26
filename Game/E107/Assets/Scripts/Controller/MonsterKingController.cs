using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingController : MonsterController
{
    [SerializeField]
    private GameObject _weapon;     // Editor

    private float _jumpCoolDown;    // 점프 쿨타임
    private bool _isJumping;        // 지금 점프 중?

    private ParticleSystem _particle;
    private Coroutine _hitDownStart;
    private Coroutine _hitDownEnd;
    private Coroutine _slashStart;
    private Coroutine _slash;

    public GameObject Weapon { get => _weapon; set => _weapon = value; }
    public ParticleSystem Particle { get => _particle; set => _particle = value; }

    public override void Init()
    {
        base.Init();

        _stat = new MonsterStat(_unitType);

        _jumpCoolDown = 10.0f;
        _isJumping = false;
    }


    protected override void ChangeStateFromMove()
    {
        float distToDetectPlayer = (transform.position - _detectPlayer.position).magnitude;

        _agent.SetDestination(_detectPlayer.position);

        //if (_stat.Hp <= _stat.MaxHp / 2)
        //{
        //    _statemachine.ChangeState(new MonsterKingHitDownState(this));
        //}
        //else if (_stat.Hp <= _stat.MaxHp)
        //{
        //    _statemachine.ChangeState(new MonsterKingHitDownState(this));
        //}
        if (distToDetectPlayer <= _stat.AttackRange)
        {
            RandomPatternSelector();
        }
        else if (distToDetectPlayer > _stat.DetectRange)
        {
            _detectPlayer = null;
            _statemachine.ChangeState(new IdleState(this));
        }
    }

    private void RandomPatternSelector()
    {
        int rand = Random.Range(0, 101);
        if (rand <= 99)
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
        _animator.speed = 1.0f;

        ToDetectPlayer(0.8f);

        _animator.CrossFade("HitDown", 0.3f, -1, 0);
    }      
    public override void ExecuteMonsterKingHitDownState() 
    {
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("HitDown"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime <= 0.4f)
            {
                _animator.speed = 0.3f;
                if (_hitDownStart == null) _hitDownStart = StartCoroutine(ChargeEffect(Define.Effect.HitDownStartEffect, _weapon.transform, 1.8f));
            }
            else if (aniTime > 0.4f && _hitDownStart != null)
            {
                if (_hitDownStart != null)
                {
                    StopCoroutine(_hitDownStart);
                    _hitDownStart = null;
                    if (_particle != null) Managers.Effect.Stop(_particle);
                }
            }
            else if (aniTime < 0.53f)
            {
                _animator.speed = 1.0f;
            }
            else if (aniTime < 0.8f)
            {
                _monsterInfo.Patterns[0].SetCollider(_stat.AttackDamage);
            }
            else if (aniTime >= 1.0f)
            {
                _monsterInfo.Patterns[0].DeActiveCollider();
                PrintText("HitDown -> IDLE");
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitMonsterKingHitDownState() 
    {
    }

    public override void EnterMonsterKingSlashState()           // Slash
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;
        _animator.speed = 1.0f;

        ToDetectPlayer(0.8f);

        _animator.CrossFade("Slash", 0.3f, -1, 0);
    }
    public override void ExecuteMonsterKingSlashState() 
    {
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Slash"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime <= 0.4f)        // 공격전 
            {
                _animator.speed = 0.1f;
                if (_slashStart == null) _slashStart = StartCoroutine(ChargeEffect(Define.Effect.SlashStartEffect, transform, 2.0f));
                if (_particle != null) _particle.transform.parent = transform;
            }
            else if (aniTime > 0.4f && _slashStart != null)
            {
                StopCoroutine(_slashStart);
                _slashStart = null;
                if (_particle != null) Managers.Effect.Stop(_particle);
            }
            else if (aniTime <= 0.7f)   // 칼을 휘두르는 중
            {
                _animator.speed = 1.0f;
                _monsterInfo.Patterns[1].SetCollider(_stat.PatternDamage);
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
        _animator.speed = 1.0f;

        ToDetectPlayer(0.8f);

        _animator.CrossFade("Stab", 0.3f, -1, 0);
    }         
    public override void ExecuteMonsterKingStabState() 
    {
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Stab"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (aniTime <= 0.2f)        // 공격 준비
            {
                
            }
            else if (aniTime <= 0.4f)   // 공격 진행
            {
                _monsterInfo.Patterns[2].SetCollider(_stat.PatternDamage);
            }
            else if (aniTime <= 0.52f)  // 공격 후 뒷걸음질 전
            {

            }
            else if (aniTime <= 0.58f)  // 뒷걸음질 시작
            {

            }
            else if (aniTime <= 0.8f)   // 원위치로 이동
            {

            }
            else if (aniTime > 1.0f)
            {
                _monsterInfo.Patterns[2].DeActiveCollider();
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitMonsterKingStabState() { }

    public override void EnterMonsterKingJumpStartState() { }    // JumpStart
    public override void ExecuteMonsterKingJumpStartState() { }
    public override void ExitMonsterKingJumpStartState() { }
    public override void EnterMonsterKingJumpAirState() { }      // JumpAir
    public override void ExecuteMonsterKingJumpAirState() { }
    public override void ExitMonsterKingJumpAirState() { }
    public override void EnterMonsterKingJumpEndState() { }      // JumpEnd
    public override void ExecuteMonsterKingJumpEndState() { }
    public override void ExitMonsterKingJumpEndState() { }

    #endregion
}
