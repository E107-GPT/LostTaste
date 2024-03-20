using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckController : MonsterController
{
    [SerializeField]
    private Transform _targetPlayer;        // ���� Ÿ����
    private bool _isDonePattern;            // ������ ������?
    private float _lastPatternTime;

    private DrillDuckSlideState _slideState;

    public Transform TargetPlayer { get { return _targetPlayer; } set { _targetPlayer = value; } }
    public bool IsDonePattern { get { return _isDonePattern; } set { _isDonePattern = value; } }

    public override void Init()
    {
        // MonsterController Init()���� Ȯ�� �ʿ�
        base.Init();

        // Other Class
        _stat = new MonsterStat(_unitType);
        _slideState = new DrillDuckSlideState(this);

        // Cur Pattern
        _isDonePattern = true;
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    // ���� ������ ���� Ÿ����
    private void UpdateTargetPlayer()
    {
        Collider[] targetPlayers = Physics.OverlapSphere(transform.position, _stat.TargetRange, 1 << 7);

        // ���� Ÿ���� ���� �߰�
        PrintText($"���� ���� �������� �÷��̾�: {targetPlayers.Length}");
        foreach (Collider player in targetPlayers)
        {
            _targetPlayer = player.transform;
            return;
        }

        // ���� ������ �÷��̾ ������
        _targetPlayer = null;
    }

    protected override void ChangeStateFromMove()
    {
        float distToDetectPlayer = (transform.position - _detectPlayer.position).magnitude;
        // float distToTargetPlayer = (transform.position - _targetPlayer.position).magnitude;
        // �Ÿ��� UpdateTargetPlayer���� �Ǵ���

        _agent.SetDestination(_detectPlayer.position);

        // targetPlayer���� detectPlayer�� �� �� ���� �� ����
        //if (_targetPlayer != null)
        //{
        //    _statemachine.ChangeState(_slideState);
        //}
        if (distToDetectPlayer <= _stat.AttackRange)
        {
            _statemachine.ChangeState(new SkillState(this));
        }
        else if (distToDetectPlayer > _stat.DetectRange)
        {
            _targetPlayer = null;
            _detectPlayer = null;
            _statemachine.ChangeState(new IdleState(this));
        }
    }

    // Idle
    public override void ExcuteIdle()
    {
        base.ExcuteIdle();

        // Hp�� 70% ���϶�� ���� �ð����� ���� ������ ���� TargetPlayer ����
        //if ((Time.time - _lastPatternTime >= _stat.PatternDelay) && _isDonePattern == true && (_stat.Hp <= _stat.MaxHp))
        //{
        //    UpdateTargetPlayer();
        //    _lastPatternTime = Time.time;
        //}
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

    // Silde

}
