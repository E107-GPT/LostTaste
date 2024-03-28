using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BibimbapSkill : ConsumingSkill
{
    [field: SerializeField]
    public int HpRecoveryAmount { get; set; }

    [field: SerializeField]
    public float GroupCooldownTime { get; set; }

    private static Dictionary<PlayerController, float> _groupLastCastTimes = new Dictionary<PlayerController, float>();

    protected override IEnumerator OnConsume(PlayerController playerController)
    {
        Managers.Sound.Play("spoon_clack", Define.Sound.Effect, 1.0f);

        playerController.Stat.Hp = Mathf.Min(playerController.Stat.MaxHp, playerController.Stat.Hp + HpRecoveryAmount);

        _groupLastCastTimes[playerController] = LastCastTime;

        yield return null;
    }

    public override bool IsPlayerCastable(PlayerController playerController)
    {
        float lastCastTime;
        bool hasCastBefore = _groupLastCastTimes.TryGetValue(playerController, out lastCastTime);
        return (!hasCastBefore || Time.time - lastCastTime > GroupCooldownTime) && base.IsPlayerCastable(playerController);
    }
}
