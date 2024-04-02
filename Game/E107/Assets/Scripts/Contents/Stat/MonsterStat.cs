using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterStat : Stat
{
    protected float _detectRange;
    protected float _detectTime;        // Idle 상태에서 적을 탐색하는 시간
    protected int _patternDamage;
    
    public float DetectRange { set => _detectRange = value; get => _detectRange; }
    public float DetectTime { set => _detectTime = value;  get => _detectTime; }
    public int PatternDamage { set => _patternDamage = value; get => _patternDamage; }

    public MonsterStat(UnitType unitType) : base(unitType) 
    {
        InitStat(unitType);
    }

    public override void InitStat(UnitType unitType)
    {
        base.InitStat(unitType);

        _detectTime = 0.01f;
        _detectRange = 15.0f;

        switch (unitType)
        {
            // Boss
            case UnitType.DrillDuck:
                _detectRange = 25.0f;
                _patternDamage = 30;
                break;
            case UnitType.Crocodile:
                _detectRange = 22.0f;
                _patternDamage = 40;
                break;
            case UnitType.IceKing:
                _detectRange = 25.0f;
                _patternDamage = 40;
                break;
            case UnitType.MonsterKing:
                _detectRange = 30.0f;
                _patternDamage = 40;
                // HitDownEnd: PatternDamage - 10
                // HitDownAfter: PatternDamage - 10
                // Slash: PatternDamage - 5
                // Stab: PatternDamage - 15
                // JumpStart: PatternDamage - 30
                // JumpEnd: PatternDamage + 20
                break;
        }
    }
}
