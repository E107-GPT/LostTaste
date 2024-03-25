using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LogSkill : Skill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    private Vector3 Scale = new Vector3(5.0f, 2.0f, 5.0f);

    [field: SerializeField]
    private float BreakProbability = 0.5f;

    protected override void Init()
    {
        SkillCoolDownTime = 10.0f;
        RequiredMp = 30;
    }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        Debug.Log("Log Attack");
        Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0, 0.7f);
        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.StrongSwingEffect, Root);
        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq);

        Vector3 offset = Root.forward.normalized * 1.5f;
        ps.transform.position += offset;
        skillObj.position += offset;

        skillObj.localScale = Scale;
        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (Scale.z / 2));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.1f);

        if (UnityEngine.Random.Range(0.0f, 1.0f) < BreakProbability)
        {
            // ºÎ¼­Áü!
            Debug.Log("ºÎ¼­Áü!");
            Managers.Sound.Play("wood_break", Define.Sound.Effect, 1);

            Destroy(this.gameObject);
            playerController.ObtainWeapon("0000_Fist");
            playerController.StateMachine.ChangeState(new IdleState(playerController));
        }

        yield return new WaitForSeconds(0.2f);
        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
