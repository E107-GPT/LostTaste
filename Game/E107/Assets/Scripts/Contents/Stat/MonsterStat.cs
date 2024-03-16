using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class MonsterStat : Stat
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected float _detectRange;

    public int Level { get { return _level; } set { _level = value; } }
    public float DetectRange { set => _detectRange = value; get => _detectRange; }

    public MonsterStat(UnitType unitType) : base(unitType) 
    {
        InitStat(unitType);
    }

    public override void InitStat(UnitType unitType)
    {
        base.InitStat(unitType);

        switch (unitType)
        {
            case UnitType.Slime:
                _level = 1;
                _detectRange = 15.0f;
                break;
            case UnitType.DrillDuck:
                _level = 1;
                _detectRange = 15.0f;
                break;
        }
    }
}
