using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareSteakSkill : ConsumingSkill
{
    public static int HpRecoveryAmount { get; set; }

    protected override IEnumerator OnConsume(PlayerController playerController)
    {
        Managers.Sound.Play("bite1", Define.Sound.Effect, 0.6f);

        playerController.Stat.Hp = Mathf.Min(100, playerController.Stat.Hp + HpRecoveryAmount);

        yield return null;
    }
}
