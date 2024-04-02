using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKingController : MonsterController
{
    public override void Init()
    {
        base.Init();

        _stat = new MonsterStat(_unitType);
        
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
        if (rand <= 0)
        {
            _statemachine.ChangeState(new IceKingSpikeState(this));
        }
        else if (rand <= 100)
        {
            _statemachine.ChangeState(new SkillState(this));
        }
    }

    #region State

    // Normal Attack
    public override void EnterSkill()
    {
        base.EnterSkill();
        _agent.avoidancePriority = 1;
    }

    public override void ExcuteSkill()
    {
        base.ExcuteSkill();
    }
    public override void ExitSkill()
    {
        base.ExitSkill();
        _agent.avoidancePriority = 50;
    }


    // IceSpike
    public override void EnterIceKingSpikeState()
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;
        _agent.avoidancePriority = 1;

        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(dirTarget.normalized, Vector3.up);
            photonView.RPC("RPC_ChangeIceKingSpikeState", RpcTarget.Others);
        }

        _monsterInfo.Patterns[0].SetCollider(_stat.PatternDamage);
        _animator.SetFloat("SpikeSpeed", 0.8f);
        _animator.CrossFade("Spike", 0.2f, -1, 0);
    }

    public override void ExcuteIceKingSpikeState()
    {
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Spike"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            
            if (aniTime >= 1.0f)
            {
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitIceKingSpikeState()
    {
        base.ExitIceKingSpikeState();
        _agent.avoidancePriority = 50;
    }
    #endregion

    #region Photon

    [PunRPC]
    void RPC_ChangeIceKingSpikeState()
    {
        _statemachine.ChangeState(new IceKingSpikeState(this));
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
    #endregion
}
