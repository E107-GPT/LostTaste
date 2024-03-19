using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrillDuckSkillState : State
{
    private DrillDuckController _drillDuckController;
    private NavMeshAgent _agent;
    private Transform _attackPlayer;
    private DrillDuckItem _item;

    private Animator _animator;
    

    public DrillDuckSkillState(DrillDuckController controller) : base(controller)
    {
        _drillDuckController = controller;

        _agent = _drillDuckController.Agent;
        _item = _drillDuckController.Item;
        _animator = controller.GetComponent<Animator>();
    }

    public override void Enter()
    {
        _agent.speed = 0;
        _agent.velocity = Vector3.zero;
        //_attackPlayer = _drillDuckController.AttackPlayer;

        _animator.CrossFade("Attack", 0.3f);
    }

    public override void Execute()
    {
        Vector3 thisToTargetDist = _attackPlayer.position - _drillDuckController.transform.position;
        Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);
        // Quaternion rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
        _drillDuckController.transform.rotation = Quaternion.Slerp(_drillDuckController.transform.rotation, Quaternion.LookRotation(dirToTarget.normalized, Vector3.up), 0.5f);

        _item.NormalAttack();
    }

    public override void Exit()
    {
    }
}
