using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberSkill : ConsumingSkill
{
    [field: SerializeField]
    public int MpRecoveryAmount { get; set; } = 100;

    protected override IEnumerator OnConsume(PlayerController playerController)
    {
        Debug.Log("ø¿¿Ã ≥»");
        Managers.Sound.Play("bite1");

        playerController.Stat.Mp = Mathf.Min(100, playerController.Stat.Mp + MpRecoveryAmount);

        yield return null;
    }
}
