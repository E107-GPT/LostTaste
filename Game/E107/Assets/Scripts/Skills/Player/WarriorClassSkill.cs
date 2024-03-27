using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClassSkill : Skill
{
    protected override void Init() { }

    protected override IEnumerator SkillCoroutine(int _attackDamage, float _attackRange)
    {
        Root = transform.root;

        Debug.Log("Class Skill");
        Root.GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);
        yield return new WaitForSeconds(0.3f);
        ParticleSystem ps = Managers.Effect.Play(Define.Effect.WarriorClassSkillEffect, Root);
        ps.transform.parent = Root;

        float duration = 5.0f;
        float timer = 0; // Ÿ�̸� �ʱ�ȭ

        while (timer < duration)
        {
            // ����ü�� ��ƼŬ �ý����� ������ �����Դϴ�.
            Collider[] monsters = Physics.OverlapSphere(Root.position, 20.0f, LayerMask.GetMask("Monster"));
            
            foreach (var monster in monsters)
            {
                Debug.Log(monster.name);
                var mon = monster.GetComponent<MonsterController>();
                mon.DetectPlayer = Root;
                Vector3 dir = (Root.transform.position - mon.transform.position).normalized;
                mon.Agent.Move(dir * Time.deltaTime * 10.0f);
            }

            timer += Time.deltaTime; // Ÿ�̸Ӹ� ������Ʈ�մϴ�.
            yield return null; // ���� �����ӱ��� ����մϴ�.
        }

        //yield return new WaitForSeconds(0.3f);
        Managers.Effect.Stop(ps);



    }
}
