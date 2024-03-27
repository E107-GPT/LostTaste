using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardAppleSkill : ConsumingSkill
{
    [field: SerializeField]
    [field: Header("ȸ�� �ֱ�(��)")]
    public float RecoveryPeriod { get; set; }

    [field: SerializeField]
    [field: Header("�ֱ�� MP ȸ����")]
    public int MpRecoveryAmountPerPeriod { get; set; }

    [field: SerializeField]
    [field: Header("�ֱ� Ƚ��")]
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
