using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowSkill : Skill
{
    [field: SerializeField]
    public int Damage { get; set; }

    protected override void Init()
    {
        Animator = GetComponent<Animator>();
    }

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;

        yield return new WaitForSeconds(0.3f);

        Vector3 dir = Root.forward;
        dir = new Vector3(dir.x, 0, dir.z);

        Root.GetComponent<Animator>().CrossFade("BOW", 0.1f, -1, 0);
        Animator.CrossFade("ATTACK", 0.1f, -1, 0);
//         _statemachine.ChangeState()

        if (Animator.IsInTransition(0) == false && Animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
        {
            float aniTime = Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (aniTime <= 0.5f)
            {

            }
        }

        Transform skillObj = Managers.Resource.Instantiate("Skills/ArrowObject").transform;
        skillObj.GetComponent<ArrowObject>().Setup(transform, Damage, _seq, 20.0f);
        
        skillObj.rotation.SetLookRotation(dir);

    }


}
