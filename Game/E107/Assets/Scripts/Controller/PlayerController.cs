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
    Define.SkillType _curSkill = Define.SkillType.None;


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
        first.OnEquipped();
        Item second = Managers.Resource.Instantiate("Weapons/0000_Fist", _righthand.transform).GetComponent<Item>();
        second.OnEquipped();
        _inventory[1] = first;
        _inventory[2] = second;

        _currentItemNum = 1;

        second.gameObject.SetActive(false);

        ///

        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        Debug.Log("키 이벤트 추가됨");
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
        _animator.CrossFade("RUN", 0.1f);
        if (PhotonNetwork.IsConnected && photonView.IsMine) photonView.RPC("ChangeMoveState", RpcTarget.Others);
    }
    public override void ExcuteMove()
    {
        if (isConnected && PhotonNetwork.InRoom)
        {
            if ((isConnected && photonView.IsMine == false)) return;
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
            if (isConnected) photonView.RPC("ChangeIdleState", RpcTarget.Others);
        }

    }



    public override void EnterDash()
    {
        base.EnterDash();
        if (photonView.IsMine || !PhotonNetwork.IsConnected || PhotonNetwork.InLobby) LookMousePosition();

        _animator.CrossFade("DASH", 0.1f, -1, 0);
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
                Debug.Log(_stat.Mp);
                _lastRightSkillCastTime = Time.time;
                break;
            case Define.SkillType.ClassSkill:
                gameObject.GetOrAddComponent<WarriorClassSkill>().Cast(_stat.AttackDamage, 10.0f);
                if (photonView.IsMine) photonView.RPC("ChageSkillState", RpcTarget.Others, Define.SkillType.ClassSkill, gameObject.transform.rotation);
                break;
        }

        //if (Input.GetMouseButton(0))
        //{



        //}
        //else if (Input.GetMouseButton(1))
        //{




        //}
        //else if (Input.GetKey(KeyCode.Q))
        //{
        //    Debug.Log("QQQQQQQQ");

        //}



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
        if (PhotonNetwork.IsConnected && photonView.IsMine) photonView.RPC("ChangeDieState", RpcTarget.Others);

        // 추가한 부분
        GetComponent<Collider>().enabled = false;
        //_agent.enabled = false;

        

    }
    #endregion

    #region InputMethod
    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (isConnected && PhotonNetwork.InRoom)
        {
            if ((isConnected && photonView.IsMine == false)) return;
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
                if (_stat.Mp >= _inventory[_currentItemNum].RightSkill.RequiredMp)
                {
                    Debug.Log($"Rquired Mp {_inventory[_currentItemNum].RightSkill.RequiredMp}");
                    if (_lastRightSkillCastTime == 0 || Time.time - _lastRightSkillCastTime >= _inventory[_currentItemNum].RightSkill.SkillCoolDownTime)
                    {
                        _curSkill = Define.SkillType.RightSkill;
                        _statemachine.ChangeState(new SkillState(this));


                        //if (isConnected) photonView.RPC("ChageSkillState", RpcTarget.Others);
                    }

                }

            }


        }
        //if(isConnected) photonView.RPC("EnterSkill", RpcTarget.All);
    }

    void OnKeyboard()
    {
        Debug.Log($"{gameObject.name} {isConnected}, {photonView != null}");
        if (isConnected && PhotonNetwork.InRoom) {
            if ((isConnected && photonView.IsMine == false)) return;
        }


        Debug.Log("왜 안되지?");

        if (_statemachine.CurState is DieState || CurState is DashState || CurState is SkillState) return;


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (_statemachine.CurState is not MoveState)
            {
                _statemachine.ChangeState(new MoveState(this));
                //if (PhotonNetwork.IsConnected && photonView.IsMine) photonView.RPC("ChangeMoveState", RpcTarget.Others);
            }

        }
        if (Input.GetKey(KeyCode.Space))
        {
            _statemachine.ChangeState(new DashState(this));
            //if (isConnected) photonView.RPC("ChangeDashState", RpcTarget.Others);
        }
        // 무기 교체
        if (Input.GetKey(KeyCode.Alpha1))
        {
            ChangeToItem(1);

            if (photonView.IsMine) photonView.RPC("ChangeFirstItem", RpcTarget.Others);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            ChangeToItem(2);
            if (photonView.IsMine) photonView.RPC("ChangeSecondItem", RpcTarget.Others);
        }

        // 상호작용
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _curSkill = Define.SkillType.ClassSkill;
            _statemachine.ChangeState(new SkillState(this));
            //if (isConnected) photonView.RPC("ChageSkillState", RpcTarget.Others);
        }



    }
    #endregion

    #region PunRPC
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
        _statemachine.ChangeState(new IdleState(this));
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
    void EquipItemRPC(string itemName)
    {

        //ObtainWeapon(itemName);
        _detectedInteractable.OnInteracted(this.gameObject);
        //GameObject go = Managers.Resource.Instantiate($"Weapons/{itemName}", _righthand.transform);


    }
    [PunRPC]
    void DropCurrentItemRPC()
    {
        Item currentItem = _inventory[_currentItemNum];
        ObtainWeapon("0000_Fist");
        GameObject go = Managers.Resource.Instantiate($"Weapons/{currentItem.gameObject.name}", gameObject.transform);
        go.transform.position = gameObject.transform.position;
        go.transform.SetParent(null);
        go.transform.rotation = new Quaternion();
        go.GetComponent<Item>().OnDropped();

        Managers.Resource.Destroy(currentItem.gameObject);
    }

    #endregion

    void OnDashFinishedEvent()
    {
        

        _statemachine.ChangeState(new IdleState(this));
        //if (photonView.IsMine) photonView.RPC("ChangeIdleState", RpcTarget.Others);
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
        //Debug.Log($"{_stat.Hp}!!!");

        if (_stat.Hp <= 0)
        {
            _statemachine.ChangeState(new DieState(this));
            //if (photonView.IsMine) photonView.RPC("ChageDieState", RpcTarget.Others);
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
    }

    public void EquipItem(Item item)
    {
        item.transform.parent = _righthand.transform;
        item.transform.SetParent(_righthand.transform);

        Debug.Log(_righthand.transform.root.name);

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
        if (photonView.IsMine) photonView.RPC("EquipItemRPC", RpcTarget.Others, item.gameObject.name);
    }

    public Item DropCurrentItem()
    {
        Item currentItem = _inventory[_currentItemNum];

        currentItem.gameObject.transform.parent = Managers.Scene.CurrentScene.transform;
        currentItem.gameObject.transform.parent = null;
        currentItem.gameObject.transform.position = gameObject.transform.root.position;
        currentItem.gameObject.transform.rotation = new Quaternion();
        currentItem.OnDropped();

        if (photonView.IsMine) photonView.RPC("DropCurrentItemRPC", RpcTarget.Others);
        return currentItem;
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
        _inventory[_currentItemNum].OnEquipped();
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

    

}
