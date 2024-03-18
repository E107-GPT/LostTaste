using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckController : MonsterController
{
    private DrillDuckItem _item;

    [SerializeField]
    private Transform _targetPlayer;        // 패턴 타겟팅

    private Coroutine _checkMonsterState;
    private bool _isDonePattern;            // 패턴이 끝났나?
    private float _patternDelay = 0f;       // pattern 수행 주기

    private DrillDuckSlideState _slideState;
    private DrillDuckSkillState _skillState;

    public Transform TargetPlayer { get { return _targetPlayer; } set { _targetPlayer = value; } }
    public bool IsDonePattern { get { return _isDonePattern; } set { _isDonePattern = value; } }
    public DrillDuckItem Item { get { return _item; } }

    public override void Init()
    {
        // BaseController
        _agent.stoppingDistance = 1.5f;
        _agent.angularSpeed = 500.0f;
        _agent.acceleration = 40.0f;
        _statemachine.CurState = new IdleState(this);

        // Editor Init
        _existPlayer = GameObject.FindGameObjectsWithTag("Player");

        // Other Class
        _stat = new MonsterStat(_unitType);
        _item = GetComponent<DrillDuckItem>();
        _slideState = new DrillDuckSlideState(this);
        _skillState = new DrillDuckSkillState(this);

        // Cur Class
        _isDonePattern = true;

        // State
        _checkMonsterState = StartCoroutine(CheckDrillDuckState());
        InvokeRepeating("UpdateDectPlayer", 0, 20.0f);              // 0초 후 호출, 20초마다 이동 타겟팅 수정 -> 여기서 가장 큰 데미지를 넣은 플레이어를 따라가게 할 수 있음
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    // 보스 패턴을 위한 타겟팅
    private void UpdateTargetPlayer()
    {
        _patternDelay++;
        if (_patternDelay > _stat.PatternDelay)      // 이 함수가 PatternDelay 호출되면 패턴 수행
        {
            Collider[] targetPlayers = Physics.OverlapSphere(transform.position, _stat.TargetRange, 1 << 7);

            // 패턴 타겟팅 조건 추가
            PrintText($"패턴 공격 범위내의 플레이어: {targetPlayers.Length}");
            if (targetPlayers.Length > 0)
            {
                for (int i = 0; i < targetPlayers.Length; ++i)
                {
                    _targetPlayer = targetPlayers[i].transform;
                }

                _patternDelay = 0;
            }
        }
    }

    IEnumerator CheckDrillDuckState()
    {
        while (_stat.Hp > 0)
        {
            yield return new WaitForSeconds(0.3f);

            UpdateAttackPlayer();
            // Hp가 70% 이하라면 일정 시간마다 패턴 공격
            if ((_unitType is Define.UnitType.DrillDuck) && _isDonePattern == true && (_stat.Hp <= _stat.MaxHp))
            {
                UpdateTargetPlayer();
            }


            if (_targetPlayer != null)
            {
                if (CurState is DrillDuckSlideState) continue;

                _statemachine.ChangeState(_slideState);
            }
            else if (_attackPlayer != null)
            {
                if (CurState is DrillDuckSkillState) continue;

                _statemachine.ChangeState(_skillState);
            }
            else if (_existPlayer.Length != 0)
            {
                if (CurState is MoveState) continue;

                _statemachine.ChangeState(new MoveState(this));
            }
            else
            {
                if (CurState is IdleState) continue;
                _detectPlayer = null;
                _attackPlayer = null;
                _statemachine.ChangeState(new IdleState(this));
            }
        }

        _statemachine.ChangeState(new DieState(this));
        CancelInvoke("UpdateDectPlayer");
    }
}
