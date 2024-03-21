using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Realtime;
using Photon.Pun;

public class PlayerController : BaseController
{
    //Stat
    PlayerStat _stat;
    
    // Item 관련 변수
    Item[] _inventory;
    int _currentItemNum;
    IPlayerInteractable _detectedInteractable;
    GameObject _righthand;
    Coroutine _mpRecoverCoroutine;


    private Renderer[] _allRenderers; // 캐릭터의 모든 Renderer 컴포넌트
    private Color[] _originalColors; // 원래의 머티리얼 색상 저장용 배열

    Color _attackedColor = Color.red;

    public PlayerStat Stat { get { return _stat; } }

    protected float _lastLeftSkillCastTime;
    protected float _lastRightSkillCastTime;


    public override void Init()
    {
        // 캐릭터의 모든 Renderer 컴포넌트를 찾음
        _allRenderers = GetComponentsInChildren<Renderer>();
        _originalColors = new Color[_allRenderers.Length];

        // 각 Renderer의 원래 머티리얼 색상 저장
        for (int i = 0; i < _allRenderers.Length; i++)
        {
            _originalColors[i] = _allRenderers[i].material.color;
        }


        _stat = new PlayerStat(Define.UnitType.Player);
        _stat.InitStat(Define.UnitType.Player);

        ///
        _inventory = new Item[3];
        _righthand = Util.FindChild(gameObject, "weapon_r", true);

        Item first = Managers.Resource.Instantiate("Weapons/0028_BubbleWand", _righthand.transform).GetComponent<Item>();
        Item second = Managers.Resource.Instantiate("Weapons/0000_Fist", _righthand.transform).GetComponent<Item>();
        _inventory[1] = first;
        _inventory[2] = second;

        _currentItemNum = 1;

        second.gameObject.SetActive(false);
        
        ///
    
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
        StartMpRecover();

        _statemachine.ChangeState(new IdleState(this));
    }

    public void StartMpRecover()
    {
        _mpRecoverCoroutine = StartCoroutine(MpRecoverCoroutine());
    }

    public void StopMpRecover()
    {
        StopCoroutine(_mpRecoverCoroutine);
    }

    IEnumerator MpRecoverCoroutine()
    {
        while(_stat.Hp > 0)
        {
            
            _stat.Mp += 5;
            if (_stat.Mp > _stat.MaxMp) _stat.Mp = _stat.MaxMp;
             yield return new WaitForSeconds(1.0f);

        }
    }
    
    IEnumerator ChangeColorTemporarily()
    {
        foreach (Renderer renderer in _allRenderers)
        {
            renderer.material.color = _attackedColor;
        }

        // 지정된 시간만큼 기다림
        yield return new WaitForSeconds(0.2f);

        // 모든 Renderer의 머티리얼 색상을 원래 색상으로 복구
        for (int i = 0; i < _allRenderers.Length; i++)
        {
            _allRenderers[i].material.color = _originalColors[i];
        }
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
        Debug.Log(_stat);
        _agent.Move(transform.forward * Time.deltaTime * _stat.MoveSpeed * 2);
        //transform.position += transform.forward * Time.deltaTime * _stat.MoveSpeed * 2;
    }

    [PunRPC]
    public override void EnterSkill()
    {
        base.EnterSkill();

        // TODO: animation도 어떻게 해줘야겠지?


        LookMousePosition();

        // 왼쪽클릭

        if (Input.GetMouseButton(0))
        {
            _inventory[_currentItemNum].LeftSKillCast();


        }
        else if (Input.GetMouseButton(1))
        {

            _inventory[_currentItemNum].RightSkillCast();
            _stat.Mp -= _inventory[_currentItemNum].RightSkill.RequiredMp;
            Debug.Log(_stat.Mp);
            _lastRightSkillCastTime = Time.time;


        }



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

        // 추가한 부분
        GetComponent<Collider>().enabled = false;
        _agent.enabled = false;
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
            if (Input.GetMouseButton(0))
            {
                _statemachine.ChangeState(new SkillState(this));

            }
            else if (Input.GetMouseButton(1))
            {
                if(_stat.Mp >= _inventory[_currentItemNum].RightSkill.RequiredMp)
                {
                    Debug.Log($"Rquired Mp {_inventory[_currentItemNum].RightSkill.RequiredMp}");
                    if(_lastRightSkillCastTime == 0 || Time.time - _lastRightSkillCastTime >= _inventory[_currentItemNum].RightSkill.SkillCoolDownTime)
                    {

                        _statemachine.ChangeState(new SkillState(this));
                    }

                }

            }
            
        }
        if(isConnected) photonView.RPC("EnterSkill", RpcTarget.All);
    }

    void OnKeyboard()
    {

        if (_statemachine.CurState is DieState || CurState is DashState || CurState is SkillState) return;


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (isConnected)
            {
                photonView.RPC("ChangeMoveState", RpcTarget.All);
            }
            else
            {
                if (_statemachine.CurState is not MoveState) _statemachine.ChangeState(new MoveState(this));
            }

        }
        if (Input.GetKey(KeyCode.Space))
        {
            if (isConnected)
            {
                photonView.RPC("ChangeDashState", RpcTarget.All);
            }
            else
                _statemachine.ChangeState(new DashState(this));
        }
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
            if (currentItem.gameObject.name == "0000_Fist") return;

            DropCurrentItem();
            ObtainWeapon("0000_Fist");
        }



    }

	[PunRPC]
    void ChangeMoveState()
    {
        if (_statemachine.CurState is not MoveState) 
            _statemachine.ChangeState(new MoveState(this));
    }

	[PunRPC]
    void ChangeDashState()
    {
        _statemachine.ChangeState(new DashState(this));
    }

    [PunRPC]
    void ChangeIDLEState()
    {
        _statemachine.ChangeState(new IdleState(this));
    }
    void OnHitEvent()
    {
      if(isConnected)
            photonView.RPC("ChangeIDLEState", RpcTarget.All);

      else
            _statemachine.ChangeState(new IdleState(this));

    }


    void OnDashFinishedEvent()
    {
        if (isConnected)
            photonView.RPC("ChangeIDLEState", RpcTarget.All);
        else
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

        StartCoroutine(ChangeColorTemporarily());
        Managers.Sound.Play("Player/Attacked",Define.Sound.Effect, 1.0f);

        _stat.Hp -= damage;
        if (_stat.Hp < 0) _stat.Hp = 0;
        lastAttackTimes[skillObjectId] = Time.time; // 해당 공격자의 마지막 공격 시간 업데이트
        Debug.Log($"{_stat.Hp}!!!");

        if (_stat.Hp <= 0)
        {
            _statemachine.ChangeState(new DieState(this));
        }

    }

    public void ResetHP()
    {
        if (_stat != null)
        {
            _stat.Hp = _stat.MaxHp; // HP를 최대 HP로 초기화
        }
    }


    public void DetectInteractable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f, LayerMask.GetMask("Item") | LayerMask.GetMask("NPC"));
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

        if (currentItem.gameObject.name == "0000_Fist")
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

    public void ObtainWeapon(string weaponName)
    {
        _inventory[_currentItemNum] = Managers.Resource.Instantiate("Weapons/" + weaponName, _righthand.transform).GetComponent<Item>();
    }
}
