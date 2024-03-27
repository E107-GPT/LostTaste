using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceKingController : MonsterController
{
    public override void Init()
    {
        base.Init();

        // Other Class
        _stat = new MonsterStat(_unitType);
        
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
            _statemachine.ChangeState(new IceKingSpikeState(this));
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

    }

    public override void ExcuteSkill()
    {
        base.ExcuteSkill();
    }

    // IceSpike
    public override void EnterIceKingSpikeState()
    {
        //base.EnterIceKingSpikeState();
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;
        ToDetectPlayer(0.8f);

        //_monsterInfo.Patterns[0].SetCollider(_stat.PatternDamage);
        _animator.CrossFade("Spike", 0.2f, -1, 0);
    }

    public override void ExcuteIceKingSpikeState()
    {
        //base.ExcuteIceKingSpikeState();
        _animator.SetFloat("SpikeSpeed", 1.0f);
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Spike"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime <= 0.1f)
            {
                _animator.SetFloat("SpikeSpeed", 1.0f);
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destPos.normalized, Vector3.up), 0.2f);
                //Managers.Effect.Play
            }
            else if (aniTime <= 0.9f)
            {
                _animator.SetFloat("SpikeSpeed", 1.0f);
                _monsterInfo.Patterns[0].SetCollider(_stat.PatternDamage);
                
            }
            else if (aniTime <= 0.95f)
            {
                _animator.SetFloat("SpikeSpeed", 0.8f);
                //Managers.Effect.Stop
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
        //base.ExitIceKingSpikeState();
    }
}
