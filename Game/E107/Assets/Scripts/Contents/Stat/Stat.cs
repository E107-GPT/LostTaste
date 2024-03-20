using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    // 공격자의 마지막 공격 시간을 저장하는 사전
    private Dictionary<int, float> lastAttackTimes = new Dictionary<int, float>();


    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attackDamage;
    [SerializeField]
    protected float _moveSpeed;
    [SerializeField]
    protected float _attackRange;
    [SerializeField]
    protected Define.UnitType _unitType;

    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float AttackRange { set => _attackRange = value; get => _attackRange; }
    public Define.UnitType UnitType { get { return _unitType; } set { _unitType = value; } }    

    public Stat(Define.UnitType unitType) { }

    public virtual void InitStat(Define.UnitType unitType) 
    {
        _unitType = unitType;
        switch (_unitType)
        {
            case Define.UnitType.Player:
                _hp = 100;
                _maxHp = 100;
                _attackDamage = 10;
                _moveSpeed = 5.0f;
                _attackRange = 1.8f;
                break;
            case Define.UnitType.Slime:
                _hp = 100;
                _maxHp = 100;
                _attackDamage = 1;
                _moveSpeed = 5.0f;
                _attackRange = 1.4f;
                break;
            case Define.UnitType.DrillDuck:
                _hp = 500;
                _maxHp = 500;
                _attackDamage = 5;
                _moveSpeed = 8.0f;
                _attackRange = 3.0f;
                break;
        }
    }

    public virtual void OnAttacked(Stat attacker)
    {
        int damage = attacker.AttackDamage;
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if (playerStat != null)
        {
            
        }

        //Managers.Game.Despawn(gameObject);
    }

}
