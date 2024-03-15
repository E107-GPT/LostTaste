using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : Stat
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected float _detectRange;

    public int Level { get { return _level; } set { _level = value; } }
    public float DetectRange { set => _detectRange = value; get => _detectRange; }

    public override void InitStat(Define.UnitType unitType)
    {
        base.InitStat(unitType);

        switch (unitType)
        {
            case Define.UnitType.Slime:
                _level = 1;
                _detectRange = 15.0f;
                break;
            case Define.UnitType.DrillDuck:
                _level = 1;
                _detectRange = 15.0f;
                break;
        }
    }
}
