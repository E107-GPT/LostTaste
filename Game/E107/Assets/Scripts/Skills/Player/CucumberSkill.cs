using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberSkill : ConsumingSkill
{
    protected override IEnumerator OnConsume(PlayerController playerController)
    {
        Debug.Log("ø¿¿Ã ≥»");
        Managers.Sound.Play("bite1");

        playerController.Stat.Mp = 100;

        yield return null;
    }
}
