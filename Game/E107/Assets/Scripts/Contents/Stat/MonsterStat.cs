using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterStat : Stat
{
    [SerializeField]
    protected int _level;
    //[SerializeField]
    //protected float _detectRange;
    [SerializeField]
    protected float _targetRange;
    [SerializeField]
    protected float _attackDelay;
    [SerializeField]
    protected float _patternCDT;    // Cooldown Time
    

    public int Level { get { return _level; } set { _level = value; } }
    //public float DetectRange { set => _detectRange = value; get => _detectRange; }
    public float TargetRange { set => _targetRange = value; get => _targetRange; }
    public float AttackDelay { set => _attackDelay = value; get => _attackDelay; }
    public float PatternCDT { set => _patternCDT = value; get => _patternCDT; }

    public MonsterStat(UnitType unitType) : base(unitType) 
    {
        InitStat(unitType);
    }

    public override void InitStat(UnitType unitType)
    {
        base.InitStat(unitType);

        _targetRange = 0;

        switch (unitType)
        {
            case UnitType.Slime:
                _level = 1;
                //_detectRange = 15.0f;
                _attackDelay = 1.0f;
                _patternCDT = int.MaxValue;
                break;
            case UnitType.DrillDuck:
                _level = 1;
                //_detectRange = 15.0f;
                _attackDelay = 10.0f;
                _targetRange = 15.0f;
                _patternCDT = 10.0f;
                break;
        }
    }
}
