using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterKingHitDownPattern : Pattern
{
    private MonsterKingController _controller;

    protected override void Init()
    {
        PatternName = "HitDown";
        _controller = GetComponent<MonsterKingController>();
    }

    public override void DeActiveCollider()
    {

    }

    IEnumerator HitDown(int attackDamage)
    {
        Debug.Log("HitDown - MonsterKing");

        // ���� ������ ���� ���� ��Ÿ��

        yield return new WaitForSeconds(0.8f);

        Transform skillObj = Managers.Resource.Instantiate("Skills/SkillObject").transform;
        skillObj.transform.parent = _controller.transform;
        skillObj.GetComponent<SkillObject>().SetUp(Root, attackDamage, _seq);   // Root�� Attacker -> Tag�� ����

        // Managers.Sound.Play("swing1");

        // Axe�� ��ġ�� ���缭 �����δ�. -> ������
        // ���簢��
        // skillObj.localScale = new Vector3(1.0f, 3.0f, attackDamage / 2);    // 5.0f
        skillObj.localScale = new Vector3(5.0f, 3.0f, 5.0f);

        skillObj.position = Root.transform.TransformPoint(Vector3.forward * (attackDamage / 3));
        skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 1.5f, skillObj.position.z);
        skillObj.rotation = Root.rotation;

        yield return new WaitForSeconds(0.8f);

        Managers.Resource.Destroy(skillObj.gameObject);

    }

    public override void SetCollider(int attackDamage)
    {
        StartCoroutine(HitDown(attackDamage));
    }
}
