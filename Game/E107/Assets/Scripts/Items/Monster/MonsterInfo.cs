using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterInfo : MonoBehaviour
{
    protected MonsterController _controller;

    protected Define.UnitType _unitType;
    protected int _attackDamage;
    protected float _attackRange;

    protected List<Skill> _skillList;       // 각 몬스터가 가진 공격 기술을 저장

    public Define.UnitType UnitType {  get { return _unitType; } set { _unitType = value; } }
    public List<Skill> SkillList { get { return _skillList; } }

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _controller = GetComponent<MonsterController>();

        _unitType = _controller.UnitType;
        _attackDamage = _controller.Stat.AttackDamage;
        _attackRange = _controller.Stat.AttackRange;

        _skillList = new List<Skill>();

        Debug.Log($"Normal Attack - " + _unitType.ToString());
    }
}
