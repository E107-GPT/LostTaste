using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    PlayerStat _stat;

    public override void Init()
    {
        _stat = gameObject.GetOrAddComponent<PlayerStat>();

        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        _statemachine.ChangeState(new IdleState(this));

        //Managers.Resource.Instantiate("UI/UI_Button");
        //UI_Button ui = Managers.UI.ShowPopupUI<UI_Button>();

        //Managers.UI.ShowSceneUI<UI_Inven>();
    }

    public override void EnterIdle()
    {
        base.EnterIdle();
        Animator anim = GetComponent<Animator>();
        anim.CrossFade("WAIT", 0.1f);
    }

    public override void EnterMove()
    {
        base.EnterMove();
        Animator anim = GetComponent<Animator>();
        anim.CrossFade("RUN", 0.1f);
    }
    public override void ExcuteMove()
    {
        base.ExcuteMove();
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 dirTo12 = new Vector3(-1.0f, 0.0f, 1.0f).normalized;
            //transform.position += dirTo12 * Time.deltaTime * _stat.MoveSpeed;

            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            //transform.Translate(Vector3.forward * Time.deltaTime * _stat.MoveSpeed);
            _agent.Move(dirTo12 * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirTo12), 0.5f);


        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.back);
            //transform.Translate(Vector3.forward * Time.deltaTime * _stat.MoveSpeed);
            //transform.position += dirTo6 * Time.deltaTime * _stat.MoveSpeed;
            Vector3 dirTo6 = new Vector3(1.0f, 0.0f, -1.0f).normalized;
            
            _agent.Move(dirTo6 * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirTo6), 0.5f);

        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            //transform.Translate(Vector3.forward * Time.deltaTime * _stat.MoveSpeed);
            //transform.position += dirTo9 * Time.deltaTime * _stat.MoveSpeed;
            Vector3 dirTo9 = new Vector3(-1.0f, 0.0f, -1.0f).normalized;
            _agent.Move(dirTo9 * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirTo9), 0.5f);

        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            //transform.Translate(Vector3.forward * Time.deltaTime * _stat.MoveSpeed);
            //transform.position += dirTo3 * Time.deltaTime * _stat.MoveSpeed;
            Vector3 dirTo3 = new Vector3(1.0f, 0.0f, 1.0f).normalized;
            _agent.Move(dirTo3 * Time.deltaTime * _stat.MoveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirTo3), 0.5f);

        }

        
        if (Input.anyKey == false) _statemachine.ChangeState(new IdleState(this));

    }



    public override void EnterDash()
    {
        base.EnterDash();
        Animator anim = GetComponent<Animator>();
        anim.CrossFade("DASH", 0.1f, -1, 0);

    }

    public override void ExcuteDash()
    {
        base.ExcuteDash();
        _agent.Move(transform.forward * Time.deltaTime * _stat.MoveSpeed * 2);
        //transform.position += transform.forward * Time.deltaTime * _stat.MoveSpeed * 2;
    }
    public override void EnterSkill()
    {
        base.EnterSkill();
        Animator anim = GetComponent<Animator>();
        anim.CrossFade("ATTACK", 0.1f, -1, 0);

        LayerMask mask = LayerMask.GetMask("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool raycast = Physics.Raycast(ray, out hit, 100.0f, mask);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        Vector3 dir = hit.point - transform.position;
        Quaternion quat = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, quat, 1.0f);
    }

    public override void ExitSkill()
    {
        base.ExitSkill();
    }



    void OnMouseClicked(Define.MouseEvent evt)
    {
        Debug.Log($"{CurState?.ToString()}");
        if (_statemachine.CurState is DieState || _statemachine.CurState is SkillState) return;



        ////Debug.Log("OnMouseClicked !");
        LayerMask mask = LayerMask.GetMask("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool raycast = Physics.Raycast(ray, out hit, 100.0f, mask);

        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);



        if (raycast)
        {
            //Vector3 dir = hit.point - transform.position;
            //Quaternion quat = Quaternion.LookRotation(dir);
            //transform.rotation = Quaternion.Lerp(transform.rotation, quat, 1.0f);
            _statemachine.ChangeState(new SkillState(this));
        }

    }

    void OnKeyboard()
    {

        if (_statemachine.CurState is DieState || CurState is DashState || CurState is SkillState) return;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (_statemachine.CurState is not MoveState) _statemachine.ChangeState(new MoveState(this));
        }
        if (Input.GetKey(KeyCode.Space)) _statemachine.ChangeState(new DashState(this));


    }
    void OnHitEvent()
    {
        PrintText("Attacked!!");
        
        _statemachine.ChangeState(new IdleState(this));

    }

    void OnDashFinishedEvent()
    {
        _statemachine.ChangeState(new IdleState(this));
    }




}
