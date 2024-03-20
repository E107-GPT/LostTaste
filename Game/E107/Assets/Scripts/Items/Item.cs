using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPlayerInteractable
{
    [SerializeField]
    protected int _attackDamage = 50;
    [SerializeField]
    protected float _attackRange = 8.0f;
    [SerializeField]
    protected bool isDropped = false;
    protected CapsuleCollider _itemCollider;


    [SerializeField]
    protected Skill _leftSkill;
    [SerializeField]
    protected Skill _rightSkill;

    // Start is called before the first frame update
    void Start()
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
    public void LeftSKill()
    {
        _leftSkill.Cast(_attackDamage, _attackRange);
    }

    public void RightSkill()
    {
        _rightSkill.Cast(_attackDamage, _attackRange);
    }

    public void OnInteracted(GameObject player)
    {
        player.GetComponent<PlayerController>().EquipItem(this);
    }
}
