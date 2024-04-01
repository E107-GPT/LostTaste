using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스킬을 사용하면 아이템을 소모하거나 다른 아이템으로 바뀌는 스킬입니다.
/// </summary>
public abstract class ConsumingSkill : Skill
{
    public static event Action<bool> OnConsumingSkillCast; // 회복 스킬 시전 여부 전달을 위한 이벤트 정의

    [field: SerializeField]
    public GameObject NextItem { get; set; }

    protected override void Init()
    {
        RequiredMp = 0;
        SkillCoolDownTime = 1.0f;

        if (NextItem == null)
        {
            NextItem = Resources.Load<GameObject>("Prefabs/Weapons/0000_Fist");
            Debug.Log(NextItem);
        }
    }

    protected override IEnumerator SkillCoroutine()
    {
        OnConsumingSkillCast?.Invoke(true); // 스킬 시전 성공하면 이벤트 발생

        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        yield return null;

        //Managers.Resource.Destroy(gameObject);
        

        Managers.Coroutine.Run(OnConsume(playerController));

        playerController.ObtainWeapon(NextItem.name);
        playerController.StateMachine.ChangeState(new IdleState(playerController));
    }

    protected abstract IEnumerator OnConsume(PlayerController playerController);
}
