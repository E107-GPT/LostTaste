using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItem : Item
{


    [SerializeField]
    private Define.UnitType _unitType;
    private MonsterController _controller;

    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        _controller = GetComponent<MonsterController>();

        _attackDamage = _controller.Stat.AttackDamage;
        _attackRange = _controller.Stat.AttackRange;

        _normalAttackObj = new GameObject($"{_controller.gameObject.name} Attack");
        _normalAttackObj.AddComponent<SkillObject>();
        _normalAttackCollider = _normalAttackObj.AddComponent<BoxCollider>();
        _normalAttackCollider.isTrigger = true;
        // 몬스터마다 변하는 부분
        _normalAttackObj.transform.localScale = new Vector3(1.0f, 5.0f, 1.1f);

        _normalAttackObj.SetActive(false);
    }
    private IEnumerator NormalAttackCorotine()
    {
        Debug.Log($"Normal Attack - {_controller.gameObject.name}");

        yield return new WaitForSeconds(0.3f);

        _normalAttackObj.GetComponent<SkillObject>().SetUp(transform, _attackDamage, 1);
        Transform root = gameObject.transform.root;
        _normalAttackObj.transform.position = root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        _normalAttackObj.transform.position = new Vector3(_normalAttackObj.transform.position.x, root.position.y + 0.5f, _normalAttackObj.transform.position.z);
        //_normalAttackObj.transform.localScale = new Vector3(2.5f, 5, 3);
        _normalAttackObj.transform.rotation = root.rotation;

        // Managers.Sound.Play();
        _normalAttackObj.SetActive(true);

        yield return new WaitForSeconds(0.3f);
        _normalAttackObj.SetActive(false);
    }

}
