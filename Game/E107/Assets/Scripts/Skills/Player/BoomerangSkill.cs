using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangSkill : Skill
{
    protected override void Init()
    {
        SkillCoolDownTime = 1.0f;
        RequiredMp = 0;
    }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();
        
        playerController.gameObject.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0, 0.7f);

        yield return null;

        Item boomerang = playerController.DropCurrentItem();
        boomerang.gameObject.transform.parent = null;

        playerController.ObtainWeapon("0000_Fist");
    }
}
