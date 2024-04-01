using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSkillObject : SkillObject
{
    private IEnumerator _onBreak;

    protected override void OnBreak()
    {
        if (_onBreak != null) Managers.Coroutine.Run(_onBreak);
    }

    public void SetUp(Transform attacker, int damage, int id, IEnumerator onBreakCoroutine)
    {
        base.SetUp(attacker, damage, id);
        _onBreak = onBreakCoroutine;
    }

    public void SetUp(Transform attacker, int damage, int id, int penetration, IEnumerator onBreakCoroutine)
    {
        base.SetUp(attacker, damage, id, penetration);
        _onBreak = onBreakCoroutine;
    }
}
