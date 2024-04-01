using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealWandSkill : Skill
{
    [field: SerializeField]
    public int Damage { get; set; }
    [field: SerializeField]
    public int HpRecoveryAmount { get; set; }
    protected override void Init() { }
    protected override IEnumerator SkillCoroutine()
    {
        Debug.Log(transform.root.gameObject.name);

        Root = transform.root;
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        Root.GetComponent<Animator>().CrossFade("ATTACK2", 0.1f, -1, 0);
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.HealOnce, Root);

        yield return new WaitForSeconds(0.5f);
        
        playerController.Stat.Hp = Mathf.Min(playerController.Stat.MaxHp, playerController.Stat.Hp + HpRecoveryAmount);
    }
}
