using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StormDaggerSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }
    [field: SerializeField]
    public float Distance { get; set; }


    protected override void Init(){}

    protected override IEnumerator SkillCoroutine()
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();
        Animator playerAnimator = player.GetComponent<Animator>();
        Root = transform.root;

        ParticleSystem start, finish;


        //particleSystem.transform.parent = player.transform;
        //particleSystem.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        yield return new WaitForSeconds(0.1f);
        start = Managers.Effect.Play(Define.Effect.StormDaggerEffect, player.transform);

        start.transform.position = Root.transform.TransformPoint(Vector3.forward * Distance);
        start.transform.position = new Vector3(start.transform.position.x, Root.position.y + 0.5f, start.transform.position.z);
        Transform skillObject = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObject.GetComponent<SkillObject>().SetUp(player.transform, Damage, _seq);

        skillObject.transform.localScale = new Vector3(1, 2, 7);
        skillObject.parent = Root.transform;
        skillObject.localPosition = new Vector3(0, 0, 3.5f);
        skillObject.localRotation = new Quaternion(0, 0, 0, 0);
        player.GetComponent<NavMeshAgent>().Warp(Root.transform.position + Root.forward * 7);
        Managers.Resource.Destroy(skillObject.gameObject);
        yield return new WaitForSeconds(0.4f);
        Managers.Effect.Stop(start);

        finish = Managers.Effect.Play(Define.Effect.StormDaggerFinishEffect, player.transform);
        finish.transform.position = Root.transform.TransformPoint(Vector3.forward * Distance * -1);
        finish.transform.position = new Vector3(finish.transform.position.x, Root.position.y + 0.5f, finish.transform.position.z);
        finish.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);



        playerController.StateMachine.ChangeState(new IdleState(playerController));
        yield return new WaitForSeconds(1.5f);
        

        Managers.Effect.Stop(finish);

        
        
    }
    // Start is called before the first frame update
}
