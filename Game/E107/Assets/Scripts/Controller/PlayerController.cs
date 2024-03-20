using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    //Stat
    PlayerStat _stat;
    
    // Item 관련 변수
    Item[] _inventory;
    int _currentItemNum;
    IPlayerInteractable _detectedInteractable;
    GameObject _righthand;

    public PlayerStat Stat { get { return _stat; } }


    public override void Init()
    {
        

        _stat = new PlayerStat(Define.UnitType.Player);
        _stat.InitStat(Define.UnitType.Player);

        ///
        _inventory = new Item[3];
        _righthand = Util.FindChild(gameObject, "weapon_r", true);

        Item first = Managers.Resource.Instantiate("Weapons/OHS01_Stick", _righthand.transform).GetComponent<Item>();
        Item second = Managers.Resource.Instantiate("Weapons/Feast", _righthand.transform).GetComponent<Item>();
        _inventory[1] = first;
        _inventory[2] = second;

        _currentItemNum = 1;

        second.gameObject.SetActive(false);
        
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
        DetectInteractable();
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
        LookMousePosition();

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
        _animator = GetComponent<Animator>();
        _animator.CrossFade("ATTACK", 0.1f, -1, 0);

        LookMousePosition();

                // 왼쪽클릭
        _inventory[_currentItemNum].LeftSKill();

    }

    public override void ExcuteSkill()
    {
        base.ExcuteSkill();
        if (Input.GetKeyDown(KeyCode.Space)) _statemachine.ChangeState(new DashState(this));
    }

    public override void ExitSkill()
    {
        base.ExitSkill();


    }

    public override void EnterDie()
    {
        base.EnterDie();
        _animator.CrossFade("DIE", 0.1f);
        
    }


    void OnMouseClicked(Define.MouseEvent evt)
    {
        //Debug.Log($"{CurState?.ToString()}");
        if (_statemachine.CurState is DieState || _statemachine.CurState is SkillState || CurState is DashState) return;



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


        // 무기 교체
        if (Input.GetKey(KeyCode.Alpha1))
        {
            if (_currentItemNum == 1) return; // 이미 1번 무기일 경우
            if (_inventory[1] != null) _inventory[1].gameObject.SetActive(true);
            if (_inventory[_currentItemNum] != null) _inventory[_currentItemNum].gameObject.SetActive(false);

            _currentItemNum = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (_currentItemNum == 2) return; // 이미 2번 무기일 경우
            if (_inventory[2] != null) _inventory[2].gameObject.SetActive(true);
            if (_inventory[_currentItemNum] != null) _inventory[_currentItemNum].gameObject.SetActive(false);

            _currentItemNum = 2;
        }

        // 무기 줍기
        if (_detectedInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            _detectedInteractable.OnInteracted(this.gameObject);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            Item currentItem = _inventory[_currentItemNum];
            // 맨손이면 못버린다.
            if (currentItem.gameObject.name == "Feast") return;

            DropCurrentItem();
            _inventory[_currentItemNum] = Managers.Resource.Instantiate("Weapons/Feast", _righthand.transform).GetComponent<Item>();
        }
    }


    void OnDashFinishedEvent()
    {
        _statemachine.ChangeState(new IdleState(this));
    }

    public override void TakeDamage(int skillObjectId, int damage)
    {
        // 대쉬 중에 무적
        if (CurState is DashState) return;

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


    public void DetectInteractable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f, LayerMask.GetMask("Item"));
        float closestSqrDistance = Mathf.Infinity;
        Collider closestCollider = null;

        foreach (var collider in colliders)
        {
            float sqrDistance = (collider.transform.position - transform.position).sqrMagnitude;
            if (sqrDistance < closestSqrDistance)
            {
                closestSqrDistance = sqrDistance;
                closestCollider = collider;
            }
        }

        if (closestCollider != null)
        {
            // 가장 가까운 오브젝트를 처리합니다. 예: 로그 출력
            Debug.Log("Closest Object: " + closestCollider.gameObject.name);
            _detectedInteractable = closestCollider.GetComponent<IPlayerInteractable>();
        }
        else
        {
            _detectedInteractable = null;
        }
    }

    public void EquipItem(Item item)
    {
        item.transform.parent = _righthand.transform;

        Item currentItem = _inventory[_currentItemNum];

        if (currentItem.gameObject.name == "Feast")
        {
            Destroy(currentItem);
        }
        else
        {
            DropCurrentItem();
        }

        _inventory[_currentItemNum] = item;
        item.OnEquipped();
        Debug.Log($"{_inventory[_currentItemNum].gameObject.name} Equipped");
    }

    public void DropCurrentItem()
    {
        Item currentItem = _inventory[_currentItemNum];

        currentItem.gameObject.transform.parent = Managers.Scene.CurrentScene.transform;
        currentItem.gameObject.transform.parent = null;
        currentItem.gameObject.transform.position = gameObject.transform.position;
        currentItem.OnDropped();
    }

    public void LookMousePosition()
    {
        LayerMask mask = LayerMask.GetMask("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool raycast = Physics.Raycast(ray, out hit, 100.0f, mask);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        Vector3 targetPoint = raycast ? hit.point : ray.GetPoint(10.0f); // 레이캐스트가 성공하면 hit.point를, 그렇지 않으면 레이의 방향으로 100 유닛 떨어진 지점을 사용
        Vector3 dir = targetPoint - transform.position;
        
        Quaternion quat = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Lerp(transform.rotation, quat, 1.0f);
    }
}
