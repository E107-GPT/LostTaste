using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각 몬스터 게임 객체가 각자의 Info를 가진다.
public class MonsterInfo : MonoBehaviour
{
    protected MonsterController _controller;

    protected Define.UnitType _unitType;
    protected int _attackDamage;
    protected float _attackRange;

    [SerializeField]
    protected Skill _skill;              // 각 몬스터가 가진 기본 공격
    [SerializeField]
    protected List<Pattern> _patterns;   // 각 몬스터가 가진 패턴 공격


    public Define.UnitType UnitType {  get { return _unitType; } set { _unitType = value; } }
    public float AttackRange { get { return _attackRange; } }
    public Skill Skill { get { return _skill; } set { _skill = value; } }
    public List<Pattern> Patterns { get { return _patterns; } set { _patterns = value; } }

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

        _patterns = new List<Pattern>();

        // Debug.Log($"Normal Attack - " + _unitType.ToString());
    }

}
