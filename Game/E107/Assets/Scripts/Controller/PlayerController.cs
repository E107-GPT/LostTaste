using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    PlayerStat _stat;
    Item _currentItem;

    Item[] _inventory;

    Item _detectedItem;

    
    

    public override void Init()
    {
        

        _stat = new PlayerStat(Define.UnitType.Player);
        _stat.InitStat(Define.UnitType.Player);

        ///
        _inventory = new Item[2];
        GameObject righthand = Util.FindChild(gameObject, "weapon_r", true);

        Item first = Managers.Resource.Instantiate("Weapons/OHS01_Stick", righthand.transform).GetComponent<Item>();
        Item second = Managers.Resource.Instantiate("Weapons/THS07_Sword", righthand.transform).GetComponent<Item>();
        
        second.gameObject.SetActive(false);

        _inventory[0] = first;
        _inventory[1] = second;

        _currentItem = first;
        ///

        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        _statemachine.ChangeState(new IdleState(this));
        //_hitCollider = gameObject.GetComponent<BoxCollider>();
        

        //Managers.Resource.Instantiate("UI/UI_Button");
        //UI_Button ui = Managers.UI.ShowPopupUI<UI_Button>();

        //Managers.UI.ShowSceneUI<UI_Inven>();
    }

    public override void EnterIdle()
    {
        base.EnterIdle();
        _animator.CrossFade("WAIT", 0.1f);
    }

    public override void ExcuteIdle()
    {
        base.ExcuteIdle();  
        DetectItem();
        Debug.Log("IDLE");
    }

    public override void EnterMove()
    {
        base.EnterMove();
        _animator.CrossFade("RUN", 0.1f);
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
        _animator.CrossFade("DASH", 0.1f, -1, 0);

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
        _animator.CrossFade("ATTACK", 0.1f, -1, 0);

        LayerMask mask = LayerMask.GetMask("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool raycast = Physics.Raycast(ray, out hit, 100.0f, mask);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        Vector3 dir = hit.point - transform.position;
        Quaternion quat = Quaternion.LookRotation(dir);
        
        transform.rotation = Quaternion.Lerp(transform.rotation, quat, 1.0f);


        // 왼쪽클릭
        _currentItem.NormalAttack();
        ParticleSystem effect = GetComponentInChildren<ParticleSystem>();
        effect.Play();

    }

    public override void ExitSkill()
    {
        base.ExitSkill();
        ParticleSystem effect = GetComponentInChildren<ParticleSystem>();
        effect.Stop();

    }




    void OnMouseClicked(Define.MouseEvent evt)
    {
        //Debug.Log($"{CurState?.ToString()}");
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

        if (Input.GetKey(KeyCode.Alpha1))
        {
            _inventory[0].gameObject.SetActive(true);
            _inventory[1].gameObject.SetActive(false);

            _currentItem = _inventory[0];
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            _inventory[0].gameObject.SetActive(false);
            _inventory[1].gameObject.SetActive(true);

            _currentItem = _inventory[1];


        }


    }
    void OnHitEvent()
    {
        
        _statemachine.ChangeState(new IdleState(this));

    }

    void OnDashFinishedEvent()
    {
        _statemachine.ChangeState(new IdleState(this));
    }

    public override void TakeDamage(int skillObjectId, int damage)
    {
        base.TakeDamage(skillObjectId, damage);

        float lastAttackTime;
        lastAttackTimes.TryGetValue(skillObjectId, out lastAttackTime);

        if (Time.time - lastAttackTime < damageCooldown)
        {
            // 쿨다운 중이므로 피해를 주지 않음
            return;
        }

        _stat.Hp -= damage;
        lastAttackTimes[skillObjectId] = Time.time; // 해당 공격자의 마지막 공격 시간 업데이트
        Debug.Log($"{_stat.Hp}!!!");

        if (_stat.Hp <= 0)
        {
            _statemachine.ChangeState(new DieState(this));
        }


    }


    public void DetectItem()
    {
        Collider[] items = Physics.OverlapSphere(transform.position, 10.0f, LayerMask.GetMask("Item"));
        float closestDistance = Mathf.Infinity;
        Collider closestItem = null;

        foreach (var item in items)
        {
            float distance = (item.transform.position - transform.position).sqrMagnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestItem = item;
            }
        }

        if (closestItem != null)
        {
            // 가장 가까운 오브젝트를 처리합니다. 예: 로그 출력
            Debug.Log("Closest Object: " + closestItem.gameObject.name);
            _detectedItem = closestItem.GetComponent<Item>();
        }
    }






}
