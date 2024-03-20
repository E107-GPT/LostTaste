using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//namespace TMP
//{
//    public class DrillDuckSlideState : State
//    {
//        private DrillDuckController _drillDuckController;

//        private NavMeshAgent _agent;
//        private Transform _detectPlayer;
//        private MonsterStat _stat;
//        private Animator _animator;

//        // Loop�� �ƴ� ����
//        // Excute�� �� ���� ����ȴ�.
//        public DrillDuckSlideState(DrillDuckController controller) : base(controller)
//        {
//            _drillDuckController = controller.GetComponent<DrillDuckController>();

//            _agent = _drillDuckController.Agent;
//            _stat = _drillDuckController.Stat;

//            _animator = _drillDuckController.GetComponent<Animator>();
//        }

//        public override void Enter()
//        {
//            _detectPlayer = _drillDuckController.DetectPlayer;
//            _drillDuckController.IsDonePattern = false;

//            _agent.velocity = Vector3.zero;
//            Vector3 dirTarget = (_detectPlayer.position - _drillDuckController.transform.position).normalized;
//            Vector3 destPos = _drillDuckController.transform.position + dirTarget * _stat.TargetRange;

//            _agent.SetDestination(destPos);
//            _animator.CrossFade("Slide", 0.5f);
//        }

//        public override void Execute()
//        {
//            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
//            {
//                float aniTime = _animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

//                if (aniTime <= 0.1f)
//                {
//                    _agent.speed = _stat.MoveSpeed;
//                }
//                else if (aniTime <= 0.5f)
//                {
//                    _agent.speed = _stat.MoveSpeed * 3.0f;
//                }
//                else if (aniTime < 1.0f)
//                {
//                    _agent.speed = _stat.MoveSpeed / 2;
//                }
//                else if (aniTime >= 1.0f)
//                {
//                    // �ִϸ��̼� ����
//                    _drillDuckController.StateMachine.ChangeState(new IdleState(_controller));
//                }

//                // Skill ��������
//                //_drillDuckController.MonsterInfo.SkillList[1].Cast(_drillDuckController.Stat.AttackDamage, _drillDuckController.Stat.AttackRange);
//                //_drillDuckController.Item.PatternAttack();
//            }

//        }

//        public override void Exit()
//        {
//            //_drillDuckController.TargetPlayer = null;
//            _drillDuckController.IsDonePattern = true;
//        }
//    }
//}

