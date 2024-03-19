using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterStat : Stat
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected float _targetRange;
    [SerializeField]
    protected float _patternkDelay;
    //[SerializeField]
    //protected float _patternCDT;    // Cooldown Time
    [SerializeField]
    protected int _patternDamage;
    

    public int Level { get { return _level; } set { _level = value; } }
    public float TargetRange { set => _targetRange = value; get => _targetRange; }
    public float PatternDelay { set => _patternkDelay = value; get => _patternkDelay; }
    public int PatternDamage { set => _patternDamage = value; get => _patternDamage; }

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
                break;
            case UnitType.DrillDuck:
                _level = 1;
                _targetRange = 15.0f;
                _patternkDelay = 10.0f;
                _patternDamage = 40;
                break;
        }
    }
}
