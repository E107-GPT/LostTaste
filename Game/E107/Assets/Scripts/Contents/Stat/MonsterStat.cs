using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    protected int _level;
    protected float _attackRange;
    protected float _detectRange;
    protected Define.MonsterType _monsterType;

    public int Level { set => _level = value; get => _level; }
    public float AttackRange { set => _attackRange = value; get => _attackRange; }
    public float DetectRange { set => _detectRange = value; get => _detectRange; }
    public Define.MonsterType MonsterType
    {
        set => _monsterType = value;
        get => _monsterType;
    }

    public void InitStat(Define.MonsterType monsterType)
    {
        _monsterType = monsterType;
        switch (_monsterType)
        {
            case Define.MonsterType.Slime:
                _hp = 100;
                _maxHp = 100;
                _attack = 5;
                _moveSpeed = 2.0f;
                _attackRange = 1.8f;
                // _agent.stoppingDistance = 1.5f; 컴포넌트 지정값은 어디에..?
                _detectRange = 15.0f;
                break;
            case Define.MonsterType.DrillDuck:
                _hp = 500;
                _maxHp = 500;
                _attack = 15;
                _moveSpeed = 4.0f;
                _attackRange = 2.3f;
                _detectRange = 15.0f;
                break;
        }
    }
}
