using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NagaWizardInfo : MonsterInfo
{
    protected override void Init()
    {
        base.Init();
        Skill = gameObject.GetOrAddComponent<NagaWizardAttackSkill>();
    }
}
