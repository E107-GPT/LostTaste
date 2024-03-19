using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPlayerInteractable
{
    [SerializeField]
    protected int _attackDamage = 50;
    [SerializeField]
    float _attackRange = 8.0f;
    [SerializeField]
    bool isDropped = false;
    CapsuleCollider _itemCollider;

    GameObject _normalAttackObj;
    BoxCollider _normalAttackCollider;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected void Init()
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
        _normalAttackObj = new GameObject("NormalAttack");

        Debug.Log(_attackDamage);
        _normalAttackObj.AddComponent<SkillObject>().SetUp(transform, _attackDamage, 1);

        _normalAttackCollider = _normalAttackObj.AddComponent<BoxCollider>();
        _normalAttackCollider.isTrigger = true;
        _normalAttackObj.SetActive(false);
        _itemCollider.enabled = false;
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = new Quaternion(0, 0, 0, 0);
        DontDestroyOnLoad(_normalAttackObj);
        isDropped = false;
    }

    public void OnDropped()
    {
        Destroy(_normalAttackObj);
        isDropped = true;
        _itemCollider.enabled = true;

    }
    public void NormalAttack()
    {
        StartCoroutine(NormalAttackCorotine());
    }

    IEnumerator NormalAttackCorotine()
    {
        Debug.Log("Normal Attack");
        
        yield return new WaitForSeconds(0.3f);
        _normalAttackObj.SetActive(true);
        _normalAttackObj.GetComponent<SkillObject>().SetUp(transform.root, _attackDamage, 1);
        Managers.Sound.Play("swing1");
        Transform root = gameObject.transform.root;

        _normalAttackObj.transform.localScale = new Vector3(1.0f, 1.0f, _attackRange);
        _normalAttackObj.transform.position = root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        //_normalAttackObj.transform.position = root.position + root.forward * (_attackRange/2);
        _normalAttackObj.transform.position = new Vector3(_normalAttackObj.transform.position.x, root.position.y + 0.5f, _normalAttackObj.transform.position.z);
        _normalAttackObj.transform.rotation = root.rotation;


        yield return new WaitForSeconds(0.3f);
        _normalAttackObj.SetActive(false);


    }

    public void SkillAttack() { /* do nothing */}

    public void OnInteracted(GameObject player)
    {
        player.GetComponent<PlayerController>().EquipItem(this);
    }
}
