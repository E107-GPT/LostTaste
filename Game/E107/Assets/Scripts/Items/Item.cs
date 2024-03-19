using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected int _attackDamage = 50;
    [SerializeField]
    float _attackRange = 8.0f;
    [SerializeField]
    bool isDropped = false;
    CapsuleCollider _itemCollider;


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
            OnEquip();
        }    
    }

    public void OnEquip()
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


}
