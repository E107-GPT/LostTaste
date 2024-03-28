using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ų�� ����ϸ� �������� �Ҹ��ϰų� �ٸ� ���������� �ٲ�� ��ų�Դϴ�.
/// </summary>
public abstract class ConsumingSkill : Skill
{
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
        GameObject player = transform.root.gameObject;
        PlayerController playerController = player.GetComponent<PlayerController>();

        yield return null;

        Managers.Resource.Destroy(gameObject);

        Managers.Coroutine.Run(OnConsume(playerController));

        playerController.ObtainWeapon(NextItem.name);
        playerController.StateMachine.ChangeState(new IdleState(playerController));
    }

    protected abstract IEnumerator OnConsume(PlayerController playerController);
}
