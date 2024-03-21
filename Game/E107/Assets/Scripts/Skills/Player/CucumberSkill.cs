using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberSkill : Skill
{
    protected override void Init()
    {
        SkillCoolDownTime = 0;
        RequiredMp = 0;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        Debug.Log("ø¿¿Ã ≥»");
        Managers.Sound.Play("bite1");

        yield return null;

        Destroy(this.gameObject);
        playerController.ObtainFist();

        playerController.Stat.Mp = 100;
        playerController.StateMachine.ChangeState(new IdleState(playerController));
    }
}
