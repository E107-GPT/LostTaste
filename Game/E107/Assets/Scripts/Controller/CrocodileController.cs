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
        _swordPS.Play();

        _agent.velocity = Vector3.zero;
        _agent.speed = 0;
        ToDetectPlayer(0.8f);
        //Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
        //Vector3 destPos = new Vector3(dirTarget.x, 0, dirTarget.z);
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destPos.normalized, Vector3.up), 0.2f);

        _monsterInfo.Patterns[0].SetCollider(_stat.PatternDamage);
        _animator.CrossFade("Sword", 0.2f, -1, 0);
    }
    public override void ExcuteCrocodileSwordState()
    {
        base.ExcuteCrocodileSwordState();
        if (_animator.IsInTransition(0) == false && _animator.GetCurrentAnimatorStateInfo(0).IsName("Sword"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            //Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            //Vector3 destPos = new Vector3(dirTarget.x, 0, dirTarget.z);
            
            if (aniTime <= 0.2f)
            {
                _animator.speed = 0.2f;
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destPos.normalized, Vector3.up), 0.2f);
                //Managers.Effect.Play
            }
            else if (aniTime <= 0.23f)
            {
                _animator.speed = 0.06f;
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destPos.normalized, Vector3.up), 0.2f);
            }
            else if (aniTime < 1.0f)
            {
                _animator.speed = 1.0f;
                //Managers.Effect.Stop
            }
            else if (aniTime >= 1.0f)
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

}
