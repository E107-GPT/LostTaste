using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSkillObject : SkillObject
{
    public delegate IEnumerator OnBreakCallback(Collider other);

    private OnBreakCallback _onBreakCallback;

    protected override void OnBreak(Collider other)
    {
        if (_onBreakCallback != null) Managers.Coroutine.Run(_onBreakCallback(other));
    }

    public void SetUp(Transform attacker, int damage, int id, OnBreakCallback onBreakCallback)
    {
        base.SetUp(attacker, damage, id);
        _onBreakCallback = onBreakCallback;
    }

    public void SetUp(Transform attacker, int damage, int id, int penetration, OnBreakCallback onBreakCallback)
    {
        base.SetUp(attacker, damage, id, penetration);
        _onBreakCallback = onBreakCallback;
    }
}
