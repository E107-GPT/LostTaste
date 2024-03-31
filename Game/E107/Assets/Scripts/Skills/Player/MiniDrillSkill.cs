using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniDrillSkill : AttackSkill
{
    [field: SerializeField]
    public float Duration { get; set; }

    [field: SerializeField]
    public float BaseSpeed { get; set; }

    [field: SerializeField]
    private Vector3 Scale = new Vector3(1.0f, 1.0f, 1.0f);

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();
        Animator playerAnimator = player.GetComponent<Animator>();

        ParticleSystem particleSystem;

        particleSystem = Managers.Effect.Play(Define.Effect.DrillDuckBeforeEffect, player.transform);
        particleSystem.transform.parent = player.transform;
        particleSystem.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);

        yield return new WaitForSeconds(1.0f);

        Managers.Effect.Stop(particleSystem);

        playerAnimator.CrossFade("SLIDE", 0.1f, -1, 0);

        particleSystem = Managers.Effect.Play(Define.Effect.DrillDuckSlideEffect, player.transform);
        particleSystem.transform.parent = player.transform;
        particleSystem.transform.localScale = new Vector3(10.0f, 2.5f, 5.0f);
        particleSystem.transform.localPosition = new Vector3(0, 0.5f, -1.0f);

        GameObject skillObject = Managers.Resource.Instantiate("Skills/SkillObject");
        skillObject.GetComponent<SkillObject>().SetUp(player.transform, Damage, _seq);
        skillObject.transform.localScale = Scale;

        float timer = 0.0f;
        while (timer < 1.0f)
        {
            Vector3 step = player.transform.forward * Time.deltaTime * GetStepSize(timer / Duration) * BaseSpeed;
            player.transform.position += step;
            skillObject.transform.position = player.transform.position;

            yield return null;
            timer += Time.deltaTime;
        }

        playerController.StateMachine.ChangeState(new IdleState(playerController));

        Managers.Resource.Destroy(skillObject);
        Managers.Effect.Stop(particleSystem);
    }

    private float GetStepSize(float normalizedTime)
    {
        if (normalizedTime <= 0.8f) return 1.0f * normalizedTime;
        else return 0.2f;
    }
}
