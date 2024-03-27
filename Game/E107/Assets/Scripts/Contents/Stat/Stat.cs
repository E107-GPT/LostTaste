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
    protected int _mp;
    [SerializeField]
    protected int _maxMp;
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
    public int Mp { get { return _mp; } set { _mp = value; } }
    public int MaxMp { get { return _maxMp; } set { _maxMp = value; } }
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
                // 100으로 돌려야함
                _hp = 1000;
                _maxHp = 1000;
                _mp = 100;
                _maxMp = 100;
                _attackDamage = 10;
                _moveSpeed = 5.0f;
                _attackRange = 1.8f;
                break;
            case Define.UnitType.DrillDuck:
                _hp = 1000;
                _maxHp = 1000;
                _attackDamage = 15;
                _moveSpeed = 8.0f;
                _attackRange = 3.0f;        // skill collider와 상관있음
                break;
            case Define.UnitType.Crocodile:
                _hp = 1500;
                _maxHp = 1500;
                _attackDamage = 15;
                _moveSpeed = 8.0f;
                _attackRange = 6.0f;
                break;
            case Define.UnitType.IceKing:
                _hp = 2000;
                _maxHp = 2000;
                _attackDamage = 20;
                _moveSpeed = 8.0f;
                _attackRange = 5.0f;
                break;
            case Define.UnitType.MonsterKing:
                _hp = 4000;
                _maxHp = 4000;
                _attackDamage = 30;
                _moveSpeed = 8.0f;
                _attackRange = 8.0f;
                break;
            case Define.UnitType.Slime:
                _hp = 200;
                _maxHp = 100;
                _attackDamage = 5;
                _moveSpeed = 6.0f;
                _attackRange = 1.4f;
                break;
            case Define.UnitType.TurtleSlime:
                _hp = 300;
                _maxHp = 300;
                _attackDamage = 10;
                _moveSpeed = 5.0f;
                _attackRange = 1.8f;
                break;
            case Define.UnitType.ToxicFlower:
                _hp = 500;
                _maxHp = 500;
                _attackDamage = 15;
                _moveSpeed = 6.5f;
                _attackRange = 10.0f;
                break;
            case Define.UnitType.Crab:
                _hp = 300;
                _maxHp = 300;
                _attackDamage = 15;
                _moveSpeed = 6.0f;
                _attackRange = 2.3f;
                break;
            case Define.UnitType.Fishman:
                _hp = 500;
                _maxHp = 500;
                _attackDamage = 20;
                _moveSpeed = 6.0f;
                _attackRange = 2.3f;
                break;
            case Define.UnitType.NagaWizard:
                _hp = 700;
                _maxHp = 700;
                _attackDamage = 30;
                _moveSpeed = 5.5f;
                _attackRange = 10.0f;
                break;
            case Define.UnitType.Demon:
                _hp = 800;
                _maxHp = 800;
                _attackDamage = 30;
                _moveSpeed = 5.5f;
                _attackRange = 10.0f;
                break;
            case Define.UnitType.Salamander:
                _hp = 400;
                _maxHp = 400;
                _attackDamage = 15;
                _moveSpeed = 5.5f;
                _attackRange = 10.0f;
                break;
            case Define.UnitType.Specter:
                _hp = 500;
                _maxHp = 500;
                _attackDamage = 25;
                _moveSpeed = 6.5f;
                _attackRange = 2.5f;
                break;
            case Define.UnitType.Skeleton:
                _hp = 500;
                _maxHp = 500;
                _attackDamage = 25;
                _moveSpeed = 5.5f;
                _attackRange = 2.5f;
                break;
        }
    }


}
