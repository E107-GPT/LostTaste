using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _gold;

    public int Gold { get { return _gold; } set { _gold = value; } }
}
