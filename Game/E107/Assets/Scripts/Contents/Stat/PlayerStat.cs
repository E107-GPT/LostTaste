using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _gold;

    // InitStat() 사용하는 곳 - MonsterStat 참고!
    public PlayerStat(Define.UnitType unitType) : base(unitType)
    {
        InitStat(unitType);
    }

    public int Gold { get { return _gold; } set { _gold = value; } }
}
