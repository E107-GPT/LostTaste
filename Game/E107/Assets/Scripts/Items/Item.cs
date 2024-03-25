using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Item : MonoBehaviour, IPlayerInteractable
{
    protected CapsuleCollider _itemCollider;
    
    public string Name { get; set; }
    public ItemTier Tier { get; set; }
    public string FlavorText { get; set; }
    public Sprite Sprite { get; set; }

    public Skill LeftSkill { get; protected set; }
    public Skill RightSkill { get; protected set; }

    [SerializeField]
    protected bool isDropped = false;

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
    public int CastLeftSkill() { return LeftSkill.Cast(); }

    public int CastRightSkill() { return RightSkill.Cast(); }

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