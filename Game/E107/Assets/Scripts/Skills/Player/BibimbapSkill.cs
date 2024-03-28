using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BibimbapSkill : ConsumingSkill
{
    [field: SerializeField]
    public int HpRecoveryAmount { get; set; }

    private Dictionary<PlayerController, float> _lastCastTimes = new Dictionary<PlayerController, float>();

    protected override IEnumerator OnConsume(PlayerController playerController)
    {
        Managers.Sound.Play("bite1");

        playerController.Stat.Hp = Mathf.Min(playerController.Stat.MaxHp, playerController.Stat.Hp + HpRecoveryAmount);

        yield return null;
    }

    public override bool IsPlayerCastable(PlayerController playerController)
    {
        // TODO
        return base.IsPlayerCastable(playerController);
    }
}
