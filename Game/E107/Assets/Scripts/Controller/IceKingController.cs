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
        //if (rand <= 30)
        //{
        //    _statemachine.ChangeState(new CrocodileSwordState(this));
        //}
        if (rand <= 100)
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
}
