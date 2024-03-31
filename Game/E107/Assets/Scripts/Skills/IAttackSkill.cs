using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }
}
