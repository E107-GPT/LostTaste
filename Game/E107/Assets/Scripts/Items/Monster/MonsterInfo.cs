using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    protected int _attackDamage;
    protected float _attackRange;
    private Define.UnitType _unitType;

    private MonsterController _controller;
    protected List<Skill> _skillList;

    public Define.UnitType UnitType {  get { return _unitType; } set { _unitType = value; } }
    public List<Skill> SkillList { get { return _skillList; } }

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _controller = GetComponent<MonsterController>();

        _attackDamage = _controller.Stat.AttackDamage;
        _attackRange = _controller.Stat.AttackRange;
        _skillList = new List<Skill>();
        
    }
    //private IEnumerator NormalAttackCorotine()
    //{
    //    Debug.Log($"Normal Attack - {_controller.gameObject.name}");

    //    yield return new WaitForSeconds(0.3f);

    //    _normalAttackObj.GetComponent<SkillObject>().SetUp(transform, _attackDamage, 1);
    //    Transform root = gameObject.transform.root;
    //    _normalAttackObj.transform.position = root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
    //    _normalAttackObj.transform.position = new Vector3(_normalAttackObj.transform.position.x, root.position.y + 0.5f, _normalAttackObj.transform.position.z);
    //    //_normalAttackObj.transform.localScale = new Vector3(2.5f, 5, 3);
    //    _normalAttackObj.transform.rotation = root.rotation;

    //    // Managers.Sound.Play();
    //    _normalAttackObj.SetActive(true);

    //    yield return new WaitForSeconds(0.3f);
    //    _normalAttackObj.SetActive(false);
    //}

}
