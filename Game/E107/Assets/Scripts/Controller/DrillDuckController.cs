using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrillDuckController : MonsterController
{
    // ��Ÿ
    // �������
    private float _slideCoolTime;   // ��Ÿ�� �߰�


    public override void Init()
    {
        // MonsterController Init()���� Ȯ�� �ʿ�
        base.Init();

        // Other Class
        _stat = new MonsterStat(_unitType);


    // DectPlayer ����
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
            _statemachine.ChangeState(new DrillDuckSlideBeforeState(this));
            //_statemachine.ChangeState(new DrillDuckSlideState(this));
        }
        else if (rand <= 100)
        {
            _statemachine.ChangeState(new SkillState(this));
        }
    }

    // Idle
    public override void EnterIdle()
    {
        base.EnterIdle();
    }

    public override void ExcuteIdle()
    {
        base.ExcuteIdle();
    }

    public override void ExitIdle()
    {
        base.ExitIdle();
    }

    // Move
    public override void EnterMove()
    {
        base.EnterMove();
    }
    public override void ExcuteMove()
    {
        base.ExcuteMove();
    }
    public override void ExitMove()
    {
        base.ExitMove();
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
    public override void ExitSkill()
    {
        base.ExitSkill();
    }


    // Before Slide
    public override void EnterDrillDuckSlideBeforeState()
    {
        _agent.velocity = Vector3.zero;
        _agent.speed = 0;

        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient)
        {
            Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
            Vector3 destPos = transform.position + dirTarget * _stat.DetectRange;
            _agent.SetDestination(destPos);
            photonView.RPC("RPC_ChangeDrillDuckSlideBeforeState", RpcTarget.Others);
        }
        _monsterInfo.Patterns[1].SetCollider(_stat.PatternDamage);
        _animator.CrossFade("BeforeSlide", 0.2f, -1, 0);
        _animator.speed = _animator.speed / 2.5f;

    }
    public override void ExcuteDrillDuckSlideBeforeState()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("BeforeSlide"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime >= 1.0f)
            {
                _monsterInfo.Patterns[1].DeActiveCollider();
                _statemachine.ChangeState(new DrillDuckSlideState(this));
            }
        }
    }
    public override void ExitDrillDuckSlideBeforeState()
    {
        _animator.speed *= 2.5f;
    }

    // Silde
    public override void EnterDrillDuckSlideState()
    {
        //_agent.velocity = Vector3.zero;
        //_agent.speed = 0;
        //Vector3 dirTarget = (_detectPlayer.position - transform.position).normalized;
        //Vector3 destPos = transform.position + dirTarget * _stat.DetectRange;

        // ��λ��� �÷��̾ ���ĳ��鼭 ����
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        _agent.radius *= 2;
        _agent.avoidancePriority = 0;

        _monsterInfo.Patterns[0].SetCollider(_stat.PatternDamage);

        //_agent.SetDestination(destPos);
        _animator.CrossFade("Slide", 0.2f, -1, 0);
        if (PhotonNetwork.IsConnected && PhotonNetwork.IsMasterClient) photonView.RPC("RPC_ChangeDrillDuckSlideState", RpcTarget.Others);

    }
    public override void ExcuteDrillDuckSlideState()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime <= 0.2f)
            {
                _agent.speed = _stat.MoveSpeed;
                //Managers.Effect.Play
            }
            else if (aniTime <= 0.5f)
            {
                _agent.speed = _stat.MoveSpeed * 3.0f;
            }
            else if (aniTime < 1.0f)
            {
                _agent.speed = _stat.MoveSpeed / 2;
            }
            else if (aniTime >= 1.0f)
            {
                _monsterInfo.Patterns[0].DeActiveCollider();
                _statemachine.ChangeState(new IdleState(this));
            }
        }
    }
    public override void ExitDrillDuckSlideState()
    {
        _agent.radius /= 2;
        _agent.avoidancePriority = 50;
        _agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
    }
    [PunRPC]
    void RPC_ChangeDrillDuckSlideBeforeState()
    {
        _statemachine.ChangeState(new DrillDuckSlideBeforeState(this));
    }

    [PunRPC]
    void RPC_ChangeDrillDuckSlideState()
    {
        _statemachine.ChangeState(new DrillDuckSlideState(this));
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
}
