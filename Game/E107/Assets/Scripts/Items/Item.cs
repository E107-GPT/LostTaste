using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Item : MonoBehaviour, IPlayerInteractable
{
    [SerializeField]
    protected int _attackDamage = 50;
    [SerializeField]
    protected float _attackRange = 8.0f;
    [SerializeField]
    protected bool isDropped = false;
    protected CapsuleCollider _itemCollider;

    public string Name { get; set; }
    public string FlavorText { get; set; }


    [SerializeField]
    protected Skill _leftSkill;
    [SerializeField]
    protected Skill _rightSkill;

    public Skill LeftSkill
    {
        get { return _leftSkill; } private set { }
    }
    public Skill RightSkill
    {
        get { return _rightSkill; } private set { }
    }

    void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _itemCollider = gameObject.AddComponent<CapsuleCollider>();
        if (isDropped)
        {
            _itemCollider.enabled = true;
        }
        else
        {
            _itemCollider.enabled = false;
            OnEquipped();
        }    
    }

    public void OnEquipped()
    {
        _itemCollider.enabled = false;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        
        isDropped = false;
    }

    public void OnDropped()
    {
        isDropped = true;
        _itemCollider.enabled = true;

    }
    [PunRPC]
    public int LeftSKillCast()
    {
        return _leftSkill.Cast(_attackDamage, _attackRange);
    }

    [PunRPC]
    public int RightSkillCast()
    {
        return _rightSkill.Cast(_attackDamage, _attackRange);
    }

    public void OnInteracted(GameObject player)
    {
        player.GetComponent<PlayerController>().EquipItem(this);
    }
}

public enum ItemTier
{
    COMMON = 0, // 일반
    UNCOMMON = 1, // 고급
    RARE = 2, // 레어
    EPIC = 3, // 희귀
    LEGENDARY = 4, // 전설
    BOSS = 5 // 보스
}