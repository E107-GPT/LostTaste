using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniDrillSkill : Skill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    public float Duration { get; set; }

    [field: SerializeField]
    public float BaseSpeed { get; set; }

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();
        Animator playerAnimator = player.GetComponent<Animator>();

        playerAnimator.CrossFade("SLIDE", 0.1f, -1, 0);
        ParticleSystem particleSystem = Managers.Effect.Play(Define.Effect.DrillDuckSlideEffect, player.transform);

        float timer = 0.0f;
        while (timer < 1.0f)
        {
            player.transform.position += player.transform.forward * Time.deltaTime * GetStepSize(timer / Duration) * BaseSpeed;

            yield return null;
            timer += Time.deltaTime;
        }

        playerAnimator.StopPlayback();
        Managers.Effect.Stop(particleSystem);
    }

    private float GetStepSize(float normalizedTime)
    {
        if (normalizedTime <= 0.8f) return 1.0f * normalizedTime;
        else return 0.2f;
    }
}
