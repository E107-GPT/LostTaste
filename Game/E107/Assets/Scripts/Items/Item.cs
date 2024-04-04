using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Item : MonoBehaviour, IPlayerInteractable
{
    protected CapsuleCollider _itemCollider;

    [field: SerializeField]
    public int Id { get; set; }

    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public ItemTier Tier { get; set; }
    [field: SerializeField]
    public string FlavorText { get; set; }
    [field: SerializeField]
    public Sprite Icon { get; set; }

    [field: SerializeField]
    public Skill LeftSkill { get; protected set; }
    [field: SerializeField]
    public Skill RightSkill { get; protected set; }

    // true�� �� ������ ���ڿ��� ������ ����
    [field: SerializeField]
    public bool IsHidden { get; protected set; }

    [SerializeField]
    protected bool isDropped = false;

    void Awake()
    {
        if (LeftSkill == null) {
            LeftSkill = gameObject.GetOrAddComponent<NormalAttackSkill>();
        }
        if (RightSkill == null) {
            RightSkill = gameObject.GetOrAddComponent<EmptySkill>();
        }

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

    public string GetFullId()
    {
        return "ITM_" + Id.ToString().PadLeft(4, '0');
    }
}

public enum ItemTier
{
    COMMON = 0, // �Ϲ�
    UNCOMMON = 1, // ���
    RARE = 2, // ����
    EPIC = 3, // ���
    LEGENDARY = 4, // ����
    BOSS = 5 // ����
}