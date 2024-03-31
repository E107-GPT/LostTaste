using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkill : Skill
{
    [field: SerializeField]
    public int Damage { get; set; }
}
