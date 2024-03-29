using System.Collections;
using UnityEngine;

public class IcePearlStaffSkill : Skill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [field: SerializeField]
    public Vector3 Scale { get; set; }

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        GameObject player = transform.root.gameObject;

        player.GetComponent<Animator>().CrossFade("ATTACK2", 0.1f, -1, 0, 1.5f);
        yield return new WaitForSeconds(0.5f);

        ParticleSystem ps = Managers.Effect.Play(Define.Effect.IceKingCleaveEffect, player.transform);

        GameObject skillObj = Managers.Resource.Instantiate("Skills/SkillObject");
        skillObj.GetComponent<SkillObject>().SetUp(player.transform, Damage, _seq);

        skillObj.transform.position = player.transform.TransformPoint(Vector3.forward * (Scale.z / 2)) + new Vector3(0, 0.5f, 0);
        skillObj.transform.rotation = player.transform.rotation;
        skillObj.transform.localScale = Scale;

        yield return new WaitForSeconds(0.5f);

        Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
    }
}
