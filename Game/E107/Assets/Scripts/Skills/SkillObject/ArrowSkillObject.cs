using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSkillObject : SkillObject
{
    protected override void OnBreak()
    {
        Debug.Log("Broken!");
    }
}
