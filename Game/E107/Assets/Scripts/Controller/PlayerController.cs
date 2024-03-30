using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Realtime;
using Photon.Pun;

public class PlayerController : BaseController
{
    public bool isStarted = false;
    //Stat
    PlayerStat _stat;

    // Item 관련 변수
    Dictionary<string, Item> _itemList = new Dictionary<string, Item>();
    Item[] _inventory;
    int _currentItemNum;
    IPlayerInteractable _detectedInteractable;
    public GameObject _righthand;
    Coroutine _mpRecoverCoroutine;
    private float _lastDashTime;
    float _dashCoolDownTime = 1.0f;
    Define.SkillType _curSkill = Define.SkillType.None;
    PlayerClass _playerClass;


    private Renderer[] _allRenderers; // 캐릭터의 모든 Renderer 컴포넌트
    private Color[] _originalColors; // 원래의 머티리얼 색상 저장용 배열

    Color _attackedColor = Color.red;

    public PlayerStat Stat { get { return _stat; } }
    public Item[] Inventory { get { return _inventory; } }
    public int CurrentItemNum { get { return _currentItemNum; } }
    public IPlayerInteractable DetectedInteractable { get { return _detectedInteractable; } }


    void LoadItemList()
    {
        
        UnityEngine.Object[] objects = Resources.LoadAll("Prefabs/Weapons");
        foreach (UnityEngine.Object obj in objects)
        {
            if (!(obj is GameObject)) continue;

            GameObject gameObject = obj as GameObject;

            string weaponName = gameObject.name;
            Item loadedItem = Managers.Resource.Instantiate($"Weapons/{weaponName}", _righthand.transform).GetComponent<Item>();
            loadedItem.OnEquipped();
            _itemList.Add(weaponName, loadedItem);
            loadedItem.gameObject.SetActive(false);
        }


    }
    /// <summary>
    /// ////////////////////////////////////////////////
    /// </summary>
    public override void Init()
    {

        // 캐릭터의 모든 Renderer 컴포넌트를 찾음

        _playerClass = gameObject.GetOrAddComponent<PlayerClass>();

        _allRenderers = GetComponentsInChildren<Renderer>();
        _originalColors = new Color[_allRenderers.Length];

        // 각 Renderer의 원래 머티리얼 색상 저장
        for (int i = 0; i < _allRenderers.Length; i++)
        {
            _originalColors[i] = _allRenderers[i].material.color;
        }


        _stat = new PlayerStat(Define.UnitType.Player);
        _stat.InitStat(Define.UnitType.Player);


        _inventory = new Item[3];
        _righthand = Util.FindChild(gameObject, "weapon_r", true);

        LoadItemList();

        _inventory[1] = _itemList["0028_BubbleWand"];
        _inventory[1].gameObject.SetActive(true);
        _inventory[2] = _itemList["0000_Fist"];

        _currentItemNum = 1;

        //second.gameObject.SetActive(false);


        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        StartMpRecover();

        _statemachine.ChangeState(new IdleState(this));
    }
    private void OnDestroy()
    {

        Debug.Log("Player가 Destroy 됩니다.");
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        StopMpRecover();


    }

    #region StateMethod
    public override void EnterIdle()
    {
        base.EnterIdle();
        _animator.CrossFade("WAIT", 0.1f);
        if (PhotonNetwork.IsConnected && photonView.IsMine) photonView.RPC("ChangeIdleState", RpcTarget.Others);
    }

    public override void ExcuteIdle()
    {
        base.ExcuteIdle();
        if (PhotonNetwork.IsConnected && photonView.IsMine) photonView.RPC("ChangeIdleState", RpcTarget.Others);
        DetectInteractable();
    }

    public override void ExitIdle()
    {
        base.ExitIdle();
        _detectedInteractable = null;
    }

    public override void EnterMove()
    {
        base.EnterMove();
        _animator.CrossFade("RUN", 0.3f);
        if (PhotonNetwork.IsConnected && photonView.IsMine) photonView.RPC("ChangeMoveState", RpcTarget.Others);
    }
    public override void ExcuteMove()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            if ((PhotonNetwork.IsConnected && photonView.IsMine == false))
            {
                return;
            }
        }
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



        if (Input.anyKey == false)
        {
            _statemachine.ChangeState(new IdleState(this));
            //if (PhotonNetwork.IsConnected) photonView.RPC("ChangeIdleState", RpcTarget.Others);
        }

    }



    public override void EnterDash()
    {
        base.EnterDash();
        if (photonView.IsMine || !PhotonNetwork.IsConnected || PhotonNetwork.InLobby) LookMousePosition();

        _animator.Play("DASH", -1, 0);
        _lastDashTime = Time.time;
        if (PhotonNetwork.IsConnected && photonView.IsMine) photonView.RPC("ChangeDashState", RpcTarget.Others);

    }

    public override void ExcuteDash()
    {
        base.ExcuteDash();
        _agent.Move(transform.forward * Time.deltaTime * _stat.MoveSpeed * 2);
        //transform.position += transform.forward * Time.deltaTime * _stat.MoveSpeed * 2;
    }

    public override void EnterSkill()
    {
        //if (isConnected && photonView.IsMine == false) return;
        base.EnterSkill();

        if (photonView.IsMine || !PhotonNetwork.IsConnected || PhotonNetwork.InLobby) LookMousePosition();

        // 왼쪽클릭

        switch (_curSkill)
        {
            case Define.SkillType.LeftSkill:
                _inventory[_currentItemNum].CastLeftSkill();
                if (photonView.IsMine) photonView.RPC("ChageSkillState", RpcTarget.Others, Define.SkillType.LeftSkill, gameObject.transform.rotation);
                break;
            case Define.SkillType.RightSkill:
                _inventory[_currentItemNum].CastRightSkill();
                _stat.Mp -= _inventory[_currentItemNum].RightSkill.RequiredMp;
                if (photonView.IsMine) photonView.RPC("ChageSkillState", RpcTarget.Others, Define.SkillType.RightSkill, gameObject.transform.rotation);
                break;
            case Define.SkillType.ClassSkill:
                _playerClass.ClassSkill.Cast();
                //gameObject.GetComponent<WarriorClassSkill>().Cast();
                if (photonView.IsMine) photonView.RPC("ChageSkillState", RpcTarget.Others, Define.SkillType.ClassSkill, gameObject.transform.rotation);
                break;
        }
    }

    public override void ExcuteSkill()
    {
        base.ExcuteSkill();
        if (Input.GetKeyDown(KeyCode.Space) && (Time.time - _lastDashTime >= _dashCoolDownTime || _lastDashTime == 0)) _statemachine.ChangeState(new DashState(this));
    }

    public override void ExitSkill()
    {
        base.ExitSkill();


    }

    public override void EnterDie()
    {
        base.EnterDie();
        _animator.CrossFade("DIE", 0.1f);
        if (PhotonNetwork.IsConnected && photonView.IsMine) photonView.RPC("ChangeDieState", RpcTarget.Others);

        // 추가한 부분
        GetComponent<Collider>().enabled = false;
        // _agent.enabled = false;  // 꺼야 setDestination 에러가 발생하지 않음

        

    }
    #endregion

    #region InputMethod
    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            if ((PhotonNetwork.IsConnected && photonView.IsMine == false)) return;
        }
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
                _curSkill = Define.SkillType.LeftSkill;
                _statemachine.ChangeState(new SkillState(this));

                //if(isConnected) photonView.RPC("ChageSkillState", RpcTarget.Others);

            }
            else if (Input.GetMouseButton(1))
            {
                if (!_inventory[_currentItemNum].RightSkill.IsPlayerCastable(this)) return;

                _curSkill = Define.SkillType.RightSkill;
                _statemachine.ChangeState(new SkillState(this));

                //if (isConnected) photonView.RPC("ChageSkillState", RpcTarget.Others);
            }


        }
        //if(isConnected) photonView.RPC("EnterSkill", RpcTarget.All);
    }

    void OnKeyboard()
    {
        //Debug.Log($"{gameObject.name} {isConnected}, {photonView != null}");
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom) {
            if ((PhotonNetwork.IsConnected && photonView.IsMine == false)) return;
        }


        if (_statemachine.CurState is DieState || CurState is DashState || CurState is SkillState) return;

        // Move
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (_statemachine.CurState is not MoveState)
            {
                _statemachine.ChangeState(new MoveState(this));
                //if (PhotonNetwork.IsConnected && photonView.IsMine) photonView.RPC("ChangeMoveState", RpcTarget.Others);
            }

        }
        // Dash
        if (Input.GetKey(KeyCode.Space) && (Time.time - _lastDashTime >= _dashCoolDownTime || _lastDashTime == 0))
        {
            _statemachine.ChangeState(new DashState(this));
            
            //if (isConnected) photonView.RPC("ChangeDashState", RpcTarget.Others);
        }


        // 무기 교체
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ChangeToItem(1);

            //if (photonView.IsMine) photonView.RPC("ChangeFirstItem", RpcTarget.Others);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            ChangeToItem(2);
            //if (photonView.IsMine) photonView.RPC("ChangeSecondItem", RpcTarget.Others);
        }

        // 상호작용
        if (_detectedInteractable != null && Input.GetKeyDown(KeyCode.E))
        {
            _detectedInteractable.OnInteracted(this.gameObject);
            if (photonView.IsMine)
            {
                if(_detectedInteractable is ItemChest) photonView.RPC("OpenChestRPC", RpcTarget.Others);
            }

        }

        if (isStarted && Input.GetKeyDown(KeyCode.B))
        {
            Item currentItem = _inventory[_currentItemNum];
            // 맨손이면 못버린다.
            if (currentItem.gameObject.name == "0000_Fist") return;

            DropCurrentItem();
            //ObtainWeapon("0000_Fist");

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!_playerClass.ClassSkill.IsPlayerCastable(this)) return;
            _curSkill = Define.SkillType.ClassSkill;
            _statemachine.ChangeState(new SkillState(this));
            //if (isConnected) photonView.RPC("ChageSkillState", RpcTarget.Others);
        }


        // 여기 잠시 임시키 넣어야할듯

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            ChangeClass(Define.ClassType.Warrior);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            ChangeClass(Define.ClassType.Priest);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            ChangeClass(Define.ClassType.Mage);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ChangeClass(Define.ClassType.Ninja);
        }
    }

    public void ChangeClass(Define.ClassType classType)
    {
        _playerClass.ChangeClass(classType);
        if (photonView.IsMine) photonView.RPC("RPC_ChangeClass", RpcTarget.Others, classType);
    }

    #endregion

    #region PunRPC

    [PunRPC]
    void RPC_ChangeClass(Define.ClassType classType)
    {
        _playerClass.ChangeClass(classType);
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
    void ChangeIdleState()
    {
        //Debug.Log("STOP PLZ");
        if(CurState is not  IdleState) _statemachine.ChangeState(new IdleState(this));
    }

    [PunRPC]
    void ChageSkillState(Define.SkillType skillType, Quaternion _rotation)
    {
        gameObject.transform.rotation = _rotation;
        _curSkill = skillType;
        _statemachine.ChangeState(new SkillState(this));
    }
    [PunRPC]
    void ChangeDieState()
    {
        _statemachine.ChangeState(new DieState(this));
    }
    [PunRPC]
    void ChangeFirstItem()
    {
        ChangeToItem(1);
    }
    [PunRPC]
    void ChangeSecondItem()
    {
        ChangeToItem(2);
    }
    [PunRPC]
    void RPC_EquipItem(int inventoryNum, string itemName, int viewID)
    {

        // 이미 바닥에 아이템은 버려진 상태임 왜냐하면 이 메세지를 보낸 녀석이 Instantiate 해버렸기 떄문
        // 그러니까 여기서 해야할 일은 itemName을 이용해서 내 손에 켜주면됨

        // 손을 비워준다.
        _inventory[CurrentItemNum].gameObject.SetActive(false);
        // 인벤 정보 바꿔주고
        _inventory[CurrentItemNum] = _itemList[itemName];
        // 실제로 껴준다.
        _inventory[CurrentItemNum].gameObject.SetActive(true);


        if(PhotonNetwork.IsMasterClient)
        {
            foreach(var item in GameObject.FindObjectsOfType<PhotonView>())
            {
                if(item.ViewID == viewID)
                {
                    PhotonNetwork.Destroy(item.gameObject);
                    break;
                }
            }
        }



        


        //ObtainWeapon(itemName);
        //_detectedInteractable.OnInteracted(this.gameObject);
        //GameObject go = Managers.Resource.Instantiate($"Weapons/{itemName}", _righthand.transform);
    }

    [PunRPC]
    void OpenChestRPC()
    {
        // 추후 수정
        // todo 이름 받아와서 열까?
        //
        _detectedInteractable.OnInteracted(this.gameObject);
    }

    [PunRPC]

    #endregion

    void OnDashFinishedEvent()
    {

        if (PhotonNetwork.InRoom && !photonView.IsMine) return;
        _statemachine.ChangeState(new MoveState(this));
        //if (photonView.IsMine) photonView.RPC("ChangeIdleState", RpcTarget.Others);
    }

    public override void TakeDamage(int skillObjectId, int damage)
    {
        // 대쉬 중에 무적
        if (CurState is DashState) return;
        if (photonView.IsMine == false) return;

        base.TakeDamage(skillObjectId, damage);

        float lastAttackTime;
        lastAttackTimes.TryGetValue(skillObjectId, out lastAttackTime);

        if (Time.time - lastAttackTime < damageCooldown)
        {
            // 쿨다운 중이므로 피해를 주지 않음
            return;
        }
        lastAttackTimes[skillObjectId] = Time.time; // 해당 공격자의 마지막 공격 시간 업데이트
        StartCoroutine(ChangeColorTemporarily());
        Managers.Sound.Play("Player/Attacked",Define.Sound.Effect, 1.0f);

        
        // 내꺼가 아니면 데미지 안받음, 즉 죽지않음
        // 죽는 것은 포톤으로만

        photonView.RPC("RPC_Attacked", RpcTarget.Others, damage);
        _stat.Hp -= damage;
        if (_stat.Hp < 0) _stat.Hp = 0;
        
        //Debug.Log($"{_stat.Hp}!!!");
        

        if (_stat.Hp <= 0)
        {
            _statemachine.ChangeState(new DieState(this));
            //if (photonView.IsMine) photonView.RPC("ChageDieState", RpcTarget.Others);
        }

    }



    public void DetectInteractable()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.5f, LayerMask.GetMask("Item") | LayerMask.GetMask("NPC"));
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
            //Debug.Log("Closest Object: " + closestCollider.gameObject.name);
            _detectedInteractable = closestCollider.GetComponent<IPlayerInteractable>();
        }
        else
        {
            _detectedInteractable = null;
        }
    }


    public void ChangeToItem(int num)
    {
        if (_currentItemNum == num) return; // 이미 num번 무기일경우
        if (_inventory[num] != null) _inventory[num].gameObject.SetActive(true);
        if (_inventory[_currentItemNum] != null) _inventory[_currentItemNum].gameObject.SetActive(false);

        _currentItemNum = num;

        if (photonView.IsMine) photonView.RPC("RPC_ItemChanged", RpcTarget.Others, CurrentItemNum,_inventory[CurrentItemNum].gameObject.name);
    }

    [PunRPC]
    void RPC_ItemChanged(int inventoryNum, string itemName)
    {
        _inventory[_currentItemNum].gameObject.SetActive(false);
        _inventory[inventoryNum] = _itemList[itemName];
        _inventory[inventoryNum].gameObject.SetActive(true);

        _currentItemNum = inventoryNum;
    }

    public void EquipItem(Item item)
    {
        Item currentItem = _inventory[_currentItemNum];

        currentItem.gameObject.SetActive(false);


        // 내가 가진것 일단 바닥에 둠
        if(currentItem.gameObject.name != "0000_Fist")
        {
            DropCurrentItem();
        }
        // 주먹 상태
        string dropItemName = item.gameObject.name.Replace("(Clone)", "");
        _inventory[_currentItemNum].gameObject.SetActive(false);
        _inventory[_currentItemNum] = _itemList[dropItemName];
        _inventory[_currentItemNum].gameObject.SetActive(true);

        int viewID = item.GetComponent<PhotonView>().ViewID;
        Debug.Log($"{_inventory[_currentItemNum].gameObject.name} Equipped");
        if(PhotonNetwork.IsMasterClient)PhotonNetwork.Destroy(item.gameObject);
        if (photonView.IsMine) photonView.RPC("RPC_EquipItem", RpcTarget.Others, _currentItemNum, dropItemName, viewID);

    }

    public void DropCurrentItem()
    {
        // 현재 아이템 끄기
        Item currentItem = _inventory[_currentItemNum];
        currentItem.gameObject.SetActive(false);

        // 현재아이템 주먹으로 바꾸기
        _inventory[_currentItemNum] = _itemList["0000_Fist"];
        _inventory[_currentItemNum].gameObject.SetActive(true);

        // 바닥에 생성 해주기

        // TODO : 마스터에서 생성해야하나?

        //int ViewID = go.GetComponent<PhotonView>().ViewID;

        //currentItem.gameObject.transform.parent = Managers.Scene.CurrentScene.transform;
        //currentItem.gameObject.transform.parent = null;
        //currentItem.gameObject.transform.position = gameObject.transform.root.position;
        //currentItem.gameObject.transform.rotation = new Quaternion();
        //currentItem.OnDropped();
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate($"Prefabs/Weapons/{currentItem.gameObject.name}", transform.position, new Quaternion());
        }
        if (photonView.IsMine) photonView.RPC("RPC_DropCurrentItem", RpcTarget.Others, currentItem.gameObject.name, transform.position);

    }

    [PunRPC]
    void RPC_DropCurrentItem(string itemName, Vector3 position)
    {
        // 보낸쪽에서 이미 바닥에아이템 생성해뒀음;
        Item currentItem = _inventory[_currentItemNum];
        currentItem.gameObject.SetActive(false);

        // 현재아이템 주먹으로 바꾸기
        _inventory[_currentItemNum] = _itemList["0000_Fist"];
        _inventory[_currentItemNum].gameObject.SetActive(true);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate($"Prefabs/Weapons/{itemName}", position, new Quaternion());
        }
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
        //_inventory[_currentItemNum] = Managers.Resource.Instantiate("Weapons/" + weaponName, _righthand.transform).GetComponent<Item>();

        _inventory[_currentItemNum].gameObject.SetActive(false);
        _inventory[_currentItemNum] = _itemList[weaponName];
        _inventory[_currentItemNum].gameObject.SetActive(true);

        if (photonView.IsMine)
        {
            photonView.RPC("RPC_ObtainWeapon", RpcTarget.Others, weaponName);
        }
        //_inventory[_currentItemNum].OnEquipped();
    }

    [PunRPC]
    void RPC_ObtainWeapon(string weaponName)
    {
        ObtainWeapon(weaponName);
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
        while (_stat.Hp > 0)
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


    public void WarpTo(Vector3 position)
    {
        _agent.Warp(position);
        if (PhotonNetwork.IsConnected && photonView.IsMine) photonView.RPC("RPC_WarpTo", RpcTarget.Others, position);
    }

    [PunRPC]
    void RPC_WarpTo(Vector3 position)
    {
        _agent.Warp(position);
    }

    [PunRPC]
    void RPC_Attacked(int damage)
    {
        StartCoroutine(ChangeColorTemporarily());
        Managers.Sound.Play("Player/Attacked", Define.Sound.Effect, 1.0f);


        _stat.Hp -= damage;
        if (_stat.Hp < 0) _stat.Hp = 0; // 해당 공격자의 마지막 공격 시간 업데이트
                                                    //Debug.Log($"{_stat.Hp}!!!");
    }




}
