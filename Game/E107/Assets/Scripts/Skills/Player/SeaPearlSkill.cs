using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeaPearlSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    public float Duration { get; set; }

    [field: SerializeField]
    public float Velocity { get; set; }

    [field: SerializeField]
    public int DirectionCount { get; set; }

    [field: SerializeField]
    public int WaveCount { get; set; }

    [field: SerializeField]
    public float Delay { get; set; }

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        player.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);

        yield return new WaitForSeconds(0.3f);


        for (int wave = 0; wave < WaveCount; wave++)
        {
            float angleOffset = (wave % 2 == 0) ? 0 : (float)360 / 2 / DirectionCount;

            for (int dir =  0; dir < DirectionCount; dir++)
            {
                Managers.Coroutine.Run(SpawnPearl(player.transform, angleOffset + 360.0f * dir / DirectionCount));
            }

            yield return new WaitForSeconds(Delay);
        }
    }

    private IEnumerator SpawnPearl(Transform root, float angle)
    {
        float radian = Mathf.Deg2Rad * angle;
        Vector3 dir = new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.SeaPearlSkillEffect, root.transform);
        ps.transform.position += Vector3.up * 0.5f;

        Transform skillObject = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObject.GetComponent<SkillObject>().SetUp(root.transform, Damage, _seq);
        skillObject.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        skillObject.position = ps.transform.position;
        skillObject.rotation.SetLookRotation(dir);

        float timer = 0.0f;
        while (timer < Duration) {
            Vector3 step = dir * Velocity * Time.deltaTime;
            skillObject.transform.position += step;
            ps.transform.position += step;

            timer += Time.deltaTime;
            yield return null;
        }

        Managers.Resource.Destroy(skillObject.gameObject);
        Managers.Effect.Stop(ps);
    }
}
