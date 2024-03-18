using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItem : MonoBehaviour
{
    [SerializeField]
    protected int _attackDamage = 0;
    [SerializeField]
    private float _attackRange = 1.4f;
    [SerializeField]
    private Define.UnitType _unitType;


    private bool _isNormalAttack;

    private GameObject _normalAttackObj;
    private BoxCollider _normalAttackCollider;
    private MonsterController _controller;

    void Start()
    {
        Init();
    }

    protected void Init()
    {
        _controller = GetComponent<MonsterController>();

        _normalAttackObj = new GameObject($"{_controller.gameObject.name} Attack");
        _normalAttackObj.AddComponent<SkillObject>();
        _normalAttackCollider = _normalAttackObj.AddComponent<BoxCollider>();
        _normalAttackCollider.isTrigger = true;

        // 몬스터마다 변하는 부분
        _normalAttackObj.transform.localScale = new Vector3(1.0f, 5.0f, 1.1f);

        _normalAttackObj.SetActive(false);

        _isNormalAttack = false;
    }
    public void NormalAttack()
    {
        if (_isNormalAttack)
        {
            StartNormalAttack();
        }
        else
        {
            _normalAttackObj.SetActive(false);
        }
    }

    public void TrueNormalAttack()
    {
        _isNormalAttack = true;
    }
    public void FalseNormalAttack()
    {
        _isNormalAttack = false;
    }
    private void StartNormalAttack()
    {
        
        Debug.Log($"Normal Attack - {_controller.gameObject.name}");

        Transform root = gameObject.transform.root;
        _normalAttackObj.transform.position = root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        _normalAttackObj.transform.position = new Vector3(_normalAttackObj.transform.position.x, root.position.y + 0.5f, _normalAttackObj.transform.position.z);
        //_normalAttackObj.transform.localScale = new Vector3(2.5f, 5, 3);
        _normalAttackObj.transform.rotation = root.rotation;

        _normalAttackObj.SetActive(true);
    }

}
