using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberSkill : Skill
{
    protected override void Init()
    {
        RequiredMp = 0;
        SkillCoolDownTime = 1.0f;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        yield return null;

        Destroy(this.gameObject);
        Debug.Log("���� ��");
        Managers.Sound.Play("bite1");
        playerController.ObtainFist();

        playerController.Stat.Mp = 100;
        playerController.StateMachine.ChangeState(new IdleState(playerController));
    }
}
