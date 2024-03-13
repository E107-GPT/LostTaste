using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    PlayerStat _stat;
    Animator anime;

    public override void Init()
    {
        _stat = gameObject.GetOrAddComponent<PlayerStat>();
        anime = GetComponent<Animator>();

        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        //Managers.Resource.Instantiate("UI/UI_Button");
        //UI_Button ui = Managers.UI.ShowPopupUI<UI_Button>();

        //Managers.UI.ShowSceneUI<UI_Inven>();
    }

    protected override void UpdateMoving()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            //transform.Translate(Vector3.forward * Time.deltaTime * _stat.MoveSpeed);
            Vector3 dirTo12 = new Vector3(-1.0f, 0.0f, 1.0f).normalized;
            transform.position += dirTo12 * Time.deltaTime * _stat.MoveSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirTo12), 0.5f);

        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.back);
            //transform.Translate(Vector3.forward * Time.deltaTime * _stat.MoveSpeed);
            Vector3 dirTo6 = new Vector3(1.0f, 0.0f, -1.0f).normalized;
            transform.position += dirTo6 * Time.deltaTime * _stat.MoveSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirTo6), 0.5f);

        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            //transform.Translate(Vector3.forward * Time.deltaTime * _stat.MoveSpeed);
            Vector3 dirTo9 = new Vector3(-1.0f, 0.0f, -1.0f).normalized;
            transform.position += dirTo9 * Time.deltaTime * _stat.MoveSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirTo9), 0.5f);

        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            //transform.Translate(Vector3.forward * Time.deltaTime * _stat.MoveSpeed);
            Vector3 dirTo3 = new Vector3(1.0f, 0.0f, 1.0f).normalized;
            transform.position += dirTo3 * Time.deltaTime * _stat.MoveSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dirTo3), 0.5f);

        }

        if (Input.anyKey == false) State = Define.State.Idle;
    }

    void OnKeyboard()
    {
        
        if (State == Define.State.Skill || State == Define.State.Dash) return; 

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if(State != Define.State.Moving) State = Define.State.Moving;
        }
        if (Input.GetKey(KeyCode.Space)) State = Define.State.Dash;





    }
    void OnHitEvent()
    {
        Debug.Log("ATTACKED!");
        State = Define.State.Idle;
        
    }

    void OnDashFinishedEvent()
    {
        State = Define.State.Idle;
    }

    protected override void UpdateDash()
    {
        transform.position += transform.forward * Time.deltaTime * _stat.MoveSpeed * 2;
        
    }


    protected override void UpdateSkill()
    {
        //Debug.Log("TEST!");
        //LayerMask mask = LayerMask.GetMask("Wall");
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //bool raycast = Physics.Raycast(ray, out hit, 100.0f, mask);
        //Vector3 dir = hit.point - transform.position;
        //Quaternion quat = Quaternion.LookRotation(dir);
        //transform.rotation = Quaternion.Lerp(transform.rotation, quat, 0.5f);
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {

        if (State == Define.State.Die || State == Define.State.Skill) return;



        ////Debug.Log("OnMouseClicked !");
        LayerMask mask = LayerMask.GetMask("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool raycast = Physics.Raycast(ray, out hit, 100.0f, mask);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);


        if (raycast)
        {
            Vector3 dir = hit.point - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 1.0f);
            State = Define.State.Skill;
        }

        //
        ////int mask = (1 << 6);
        //

        //if ()
        //{
        //    _destPos = hit.point;
        //    _state = PlayerState.Moving;
        //    //Debug.Log($"Raycast Camera @ name : {hit.collider.gameObject.name} tag : {hit.collider.gameObject.tag}");
        //}

    }


}
