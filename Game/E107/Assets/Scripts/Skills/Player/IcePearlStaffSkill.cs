using System.Collections;
using UnityEngine;

public class IcePearlStaffSkill : Skill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    public float Duration { get; set; }

    [field: SerializeField]
    public Vector3 StartScale { get; set; }

    [field: SerializeField]
    public Vector3 ScaleDelta { get; set; } // 초당 Scale 변화량

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        GameObject player = transform.root.gameObject;

        player.GetComponent<Animator>().CrossFade("ATTACK2", 0.1f, -1, 0, 1.5f);
        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.IceKingCleaveEffect, player.transform);

        GameObject skillObj = Managers.Resource.Instantiate("Skills/SkillObject");
        skillObj.GetComponent<SkillObject>().SetUp(player.transform, Damage, _seq);

        float timer = 0.0f;
        Vector3 scale = StartScale;
        while (timer < Duration)
        {
            skillObj.transform.position = player.transform.TransformPoint(Vector3.forward * (scale.z / 2)) + new Vector3(0, 0.5f, 0);
            skillObj.transform.rotation = player.transform.rotation;
            skillObj.transform.localScale = scale;

            yield return null;  // 다음 프레임
            timer += Time.deltaTime;
            scale += ScaleDelta * Time.deltaTime;
        }

        Managers.Resource.Destroy(skillObj.gameObject);

        yield return new WaitForSeconds(0.5f);

        Managers.Effect.Stop(ps);
    }
}
