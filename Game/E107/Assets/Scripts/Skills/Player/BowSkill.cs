using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSkill : Skill, IAttackSkill
{
    [field: SerializeField]
    public int Damage { get; set; }

    [SerializeField]
    public GameObject _arrowSpawn;

    private Transform _arrow;

    protected override void Init()
    {
    }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;

        Vector3 dir = Root.forward;
        dir = new Vector3(dir.x, 0, dir.z);


        Root.GetComponent<Animator>().CrossFade("BOW", 0.1f, -1, 0);
        GetComponent<Animator>().CrossFade("ATTACK", 0.1f, -1, 0);

        // 활 쏘는 자세를 취하기 전
        // 활이 시위를 당기는 모션이 취한 상태
        yield return new WaitForSeconds(0.3f);
        // 활 쏘는 자세를 취하면
        // 활 시위가 다 당겨지면

        _arrow = Managers.Resource.Instantiate("Skills/ArrowObject").transform;
        _arrow.GetComponent<ArrowObject>().Setup(transform, Damage, _seq);
        _arrow.rotation = Quaternion.identity;
        _arrow.parent = _arrowSpawn.transform;

        Root.GetComponent<PlayerController>().StateMachine.ChangeState(new IdleState(Root.GetComponent<PlayerController>()));
        GetComponent<Animator>().CrossFade("IDLE", 0.1f, -1, 0);

        float moveDuration = 1.1f;
        float timer = 0;
        float speed = 10.0f;

        while (timer < moveDuration)
        {
            Vector3 moveStep = dir * speed * Time.deltaTime;
            _arrow.position += moveStep;

            timer += Time.deltaTime;
            yield return null;
        }

        Managers.Resource.Destroy(_arrow.gameObject);
    }


}
