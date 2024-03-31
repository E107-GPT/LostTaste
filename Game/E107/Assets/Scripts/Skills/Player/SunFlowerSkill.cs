using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlowerSkill : Skill
{
    [field: SerializeField]
    public int Damage { get; set; }
    [field: SerializeField]
    private Vector3 Scale = new Vector3(5.0f, 5.0f, 5.0f);

    protected override void Init() { }

    protected override IEnumerator SkillCoroutine()
    {
        // 해당 플레이어 위치에 떨어뜨리기
        Root = transform.root;
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();
        player.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0, 1.5f);
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.SunFallEffect, Root);
        yield return new WaitForSeconds(1.0f);

        Transform trans = Root;
        trans.position += new Vector3(0, 10, 0);

        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq);

        skillObj.localScale = Scale;
        skillObj.position = Root.position;
        Managers.Resource.Destroy(skillObj.gameObject);
    }
}
