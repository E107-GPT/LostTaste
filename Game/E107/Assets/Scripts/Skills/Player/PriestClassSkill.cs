using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriestClassSkill : Skill
{
    [field: SerializeField]
    public int HealMount { get; set; }
    [field: SerializeField]
    public int HealCount { get; set; }

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;

        PlayerController _playerController = gameObject.GetComponent<PlayerController>();

        
        Root.GetComponent<Animator>().CrossFade("ATTACK2", 0.2f, -1, 0);
        yield return new WaitForSeconds(0.5f);
        _playerController.StateMachine.ChangeState(new IdleState(_playerController));
        ParticleSystem start = Managers.Effect.Play(Define.Effect.PriestClassSkillEffect, Root);
        start.transform.parent = Root;
        start.transform.position += Root.up;
        start.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

        int cnt = 0;
        while(cnt < HealCount)
        {
            Collider[] players = Physics.OverlapSphere(Root.transform.position, 5.0f, LayerMask.GetMask("Player"));
            foreach(var player in players)
            {
                player.GetComponent<PlayerController>().TakeHeal(HealMount);

            }
            
            cnt += 1;
            
            yield return new WaitForSeconds(0.5f);
            
        }
        Managers.Effect.Stop(start);
        




    }
}
