using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterStat : Stat
{
    protected int _level;
    protected float _detectRange;
    protected float _detectTime;        // Idle 상태에서 적을 탐색하는 시간
    protected float _patternkDelay;
    protected int _patternDamage;
    

    public int Level { set => _level = value; get => _level; }
    public float DetectRange { set => _detectRange = value; get => _detectRange; }
    public float DetectTime { set => _detectTime = value;  get => _detectTime; }
    public float PatternDelay { set => _patternkDelay = value; get => _patternkDelay; }
    public int PatternDamage { set => _patternDamage = value; get => _patternDamage; }

    public MonsterStat(UnitType unitType) : base(unitType) 
    {
        InitStat(unitType);
    }

    public override void InitStat(UnitType unitType)
    {
        base.InitStat(unitType);

        _detectTime = 0.01f;

        switch (unitType)
        {
            case UnitType.Slime:
                _level = 1;
                _detectRange = 10.0f;
                break;
            case UnitType.DrillDuck:
                _level = 1;
                _detectRange = 20.0f;
                _patternkDelay = 10.0f;     // 수정 필요
                _patternDamage = 40;
                break;
        }
    }
}
