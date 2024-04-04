using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleAxeSkill : Skill, IAttackSkill
{
    // Start is called before the first frame update
    [field: SerializeField]
    public int Damage { get; set; }
    protected override void Init() { }

    public Define.Effect weaponEffect = Define.Effect.FireKatanaEffect;

    protected override IEnumerator SkillCoroutine()
    {
        Root = transform.root;
        PlayerController _playerController = Root.GetComponent<PlayerController>();
        PhotonView photonView = transform.root.GetComponent<PhotonView>();
        NavMeshAgent agent = Root.GetComponent<NavMeshAgent>();


        //Transform dir = RaycastGround();
        //photonView.RPC("RPC_StartCoroutine", RpcTarget.Others, Root.position, Damage, _seq);


        //Vector3 dir = Root.forward;
        Root.GetComponent<Animator>().CrossFade("SwordSpin", 0.1f, -1, 0);
        //dir = new Vector3(dir.x, 0, dir.z);

        ParticleSystem ps = Managers.Effect.Play(weaponEffect, Root);
        ps.transform.localScale = new Vector3(1f, 1f, 1f);
        ps.transform.parent = Root;
        ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y + 0.6f, ps.transform.position.z);

        Transform skillObj = Managers.Resource.Instantiate("Skills/CircleSkillObject").transform;
        //yield return new WaitForSeconds(0.3f);

        //skillObj.transform.position = new Vector3(0, 0, 0);

        skillObj.GetComponent<SkillObject>().SetUp(Root, Damage, _seq++);


        skillObj.localScale = new Vector3(4.0f, 4.0f, 4.0f);


        skillObj.transform.parent = ps.transform;
        skillObj.transform.localPosition = new Vector3(0, 1, 0);

        _playerController.isHolding = true;





        //ps.transform.position = new Vector3(ps.transform.position.x, ps.transform.position.y + 0.5f, ps.transform.position.z);

        //

        //skillObj.position = Root.transform.position;
        //skillObj.position = new Vector3(skillObj.position.x, Root.position.y + 0.5f, skillObj.position.z);
        //skillObj.rotation.SetLookRotation(dir);


        yield return new WaitForSeconds(2.0f);
        _playerController.StateMachine.ChangeState(new IdleState(_playerController));
        //Managers.Resource.Destroy(skillObj.gameObject);
        Managers.Effect.Stop(ps);
        _playerController.isHolding = false;
        Debug.Log("STOP SKill!");



    }

    private Transform RaycastGround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;
        bool isHit = Physics.Raycast(ray, out raycastHit, 100.0f, LayerMask.GetMask("Ground"));

        if (!isHit) return null;
        return raycastHit.transform;
    }
}
   

