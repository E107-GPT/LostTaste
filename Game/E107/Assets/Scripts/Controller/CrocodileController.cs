using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrocodileController : MonsterController
{
    private ParticleSystem _swordPS;

    public override void Init()
    {
        base.Init();

        _stat = new MonsterStat(_unitType);
        _swordPS = GetComponentInChildren<ParticleSystem>();
        _swordPS.Stop();
    }
    
    protected override void ChangeStateFromMove()
    {
        float distToDetectPlayer = (transform.position - _detectPlayer.position).magnitude;

        _agent.SetDestination(_detectPlayer.position);


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
        if (rand <= 30)
        {
            _statemachine.ChangeState(new CrocodileSwordState(this));
        }
        else if (rand <= 100)
        {
            _statemachine.ChangeState(new SkillState(this));
        }
    }

    // Normal Attack
    public override void EnterSkill()
    {
        base.EnterSkill();
        _swordPS.Play();
    }

    public override void ExitSkill()
    {
        base.ExitSkill();
        _swordPS.Stop();
    }

    // Sword
    public override void EnterCrocodileSwordState()
    {
        base.EnterCrocodileSwordState();
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dirTarget.normalized, Vector3.up);
            photonView.RPC("RPC_ChangeCrocodileSwordState", RpcTarget.Others);
        }
        

        _agent.velocity = Vector3.zero;
        _agent.speed = 0;
        _swordPS.Play();
        _monsterInfo.Patterns[0].SetCollider(_stat.PatternDamage);
        _animator.CrossFade("Sword", 0.2f, -1, 0);
    }
    public override void ExcuteCrocodileSwordState()
    {
        if (CurState is DieState)
        {
            //_monsterInfo.Patterns[0].DeActiveCollider();
            return;
        }

        base.ExcuteCrocodileSwordState();

        _animator.SetFloat("SwordSpeed", 0.1f);
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Sword"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            
            if (aniTime <= 0.2f)
            {
                _animator.SetFloat("SwordSpeed", 0.2f);
            }
            else if (aniTime <= 0.23f)
            {
                _animator.SetFloat("SwordSpeed", 0.06f);
            }
            else if (aniTime <= 1.0f)
            {
                _animator.SetFloat("SwordSpeed", 1.0f);
            }
            else if (aniTime > 1.0f)
            {
                _monsterInfo.Patterns[0].DeActiveCollider();
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }

    public override void ExitCrocodileSwordState()
    {
        base.ExitCrocodileSwordState();
        _swordPS.Stop();
    }

    [PunRPC]
    void RPC_ChangeCrocodileSwordState()
    {
        _statemachine.ChangeState(new CrocodileSwordState(this));
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
    void RPC_MonsterAttacked(int damage)
    {
        MonsterAttacked(damage);

    }
}
