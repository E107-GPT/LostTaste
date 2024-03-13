using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MonsterStateItem
{
    public class IDLE : State<MonsterController>
    {
        NavMeshAgent _agent;

        public override void Enter(MonsterController entity)
        {
            entity.PrintText($"대기중");
            _agent = entity.GetComponent<NavMeshAgent>();
            _agent.speed = 0;
        }

        public override void Execute(MonsterController entity)
        {
            // 자동 회복
            if (entity.Hp < entity.MaxHp)
            {
                entity.Hp += 10;
            }
            else if (entity.Hp > entity.MaxHp)
            {
                entity.Hp = entity.MaxHp;
            }

            entity.PrintText("자동 회복 중...");
        }

        public override void Exit(MonsterController entity)
        {
            entity.PrintText($"행동 시작");
        }
    }
    public class CHASE : State<MonsterController>
    {
        NavMeshAgent _agent;

        public override void Enter(MonsterController entity)
        {
            entity.PrintText("인식 범위 내에 들어온 플레이어 확인");
            _agent = entity.GetComponent<NavMeshAgent>();
            _agent.speed = entity.MoveSpeed;
        }

        public override void Execute(MonsterController entity)
        {
            // 장애물을 피해서 추격하는 코드를 작성해야함
            // NavMeshAgent 사용할까..?

            entity.PrintText("플레이어를 추격 중...");

            Vector3 thisToTargetDist = entity.DetectPlayer.position - entity.transform.position;
            Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);

            Quaternion rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
            entity.transform.rotation = rotation;

            //entity.transform.Translate(dirToTarget.normalized * entity.MoveSpeed * Time.deltaTime, Space.World);
            _agent.SetDestination(entity.DetectPlayer.position);
        }

        public override void Exit(MonsterController entity)
        {
            // 목표로 하는 플레이어 사망, 인식 범위내에 플레이어가 없음, 공격 사거리 내에 플레이어가 존재
            entity.PrintText("추격 종료");
        }
    }
    public class ATTACK : State<MonsterController>
    {
        NavMeshAgent _agent;

        public override void Enter(MonsterController entity)
        {
            entity.PrintText("공격 범위 내에 들어온 플레이어 확인");
            _agent = entity.GetComponent<NavMeshAgent>();
            _agent.speed = 0;
        }

        public override void Execute(MonsterController entity)
        {
            entity.PrintText("플레이어 공격");

            // 플레이어 바라보기
            Vector3 thisToTargetDist = entity.AttackPlayer.position - entity.transform.position;
            Vector3 dirToTarget = new Vector3(thisToTargetDist.x, 0, thisToTargetDist.z);

            Quaternion rotation = Quaternion.LookRotation(dirToTarget.normalized, Vector3.up);
            entity.transform.rotation = rotation;

            // 플레이어 공격 -> 플레이어의 HP를 몬스터의 ATTACK만큼 차감 : 명성이랑 해야함
            // entity.AttackPlayer
            // entity.Attack
        }

        public override void Exit(MonsterController entity)
        {
            // 플레이어 사망, 사거리 내에 플레이어가 없음
            entity.PrintText("공격 종료");
        }
    }

    // Global State에 의해 DIE 상태가 호출된다.
    public class DIE : State<MonsterController>
    {
        public override void Enter(MonsterController entity)
        {
            entity.PrintText("사망");
        }

        public override void Execute(MonsterController entity)
        {
            // 몬스터 객체 제거
            // GameController의 배열에서 제거 및 null 부여

        }

        public override void Exit(MonsterController entity)
        {
            entity.PrintText("부활");
        }
    }

    public class StateGlobal : State<MonsterController>
    {
        public override void Enter(MonsterController entity)
        {
            // 비워둔다.
        }

        // stateMachine에서 매 프레임마다 수행하면서 조건에 부합한지 확인한다.
        public override void Execute(MonsterController entity)
        {
            // 만약 현재 상태가 Global State를 통해 수행되는 상태라면 종료한다.
            if (entity.curState == MonsterState.DIE) return;

            if (entity.Hp <= 0)
            {
                entity.Hp = 0;
                entity.ChangeState(MonsterState.DIE);
            }
            // 여기에 확률적으로 사용하는 패턴( 상태 )을 넣어도 된다.
            int patternState = Random.Range(0, 100);
            if (patternState < 10) return;              // 10% 확률
        }

        public override void Exit(MonsterController entity)
        {
            // 비워둔다.
        }
    }
}
