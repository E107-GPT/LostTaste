using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAppleSkill : ConsumingSkill
{
    [field: SerializeField]
    [field: Header("회복 주기(초)")]
    public float RecoveryPeriod { get; set; }

    [field: SerializeField]
    [field: Header("주기당 MP 회복량")]
    public int MpRecoveryAmountPerPeriod { get; set; }

    [field: SerializeField]
    [field: Header("주기 횟수")]
    public int PeriodCount { get; set; } 

    protected override IEnumerator OnConsume(PlayerController playerController)
    {
        Managers.Sound.Play("bite1");

        for (int i = 0; i < PeriodCount; i++)
        {
            yield return new WaitForSeconds(RecoveryPeriod);
            Debug.Log("period");

            playerController.Stat.Mp = Mathf.Min(playerController.Stat.MaxMp, playerController.Stat.Mp + MpRecoveryAmountPerPeriod);
        }
    }
}
