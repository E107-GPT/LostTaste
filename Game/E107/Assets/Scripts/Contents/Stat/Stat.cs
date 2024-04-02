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
                _hp = 300;
                _maxHp = 300;
                _mp = 100;
                _maxMp = 100;
                _attackDamage = 10;
                _moveSpeed = 5.0f;
                _attackRange = 1.8f;
                break;
            #region Stage 1
            case Define.UnitType.Mushroom:      // stage 1
                _hp = 230;
                _maxHp = 230;
                _attackDamage = 4;
                _moveSpeed = 6.0f;
                _attackRange = 1.4f;
                break;
            case Define.UnitType.TurtleSlime:
                _hp = 330;
                _maxHp = 330;
                _attackDamage = 5;
                _moveSpeed = 5.0f;
                _attackRange = 2.0f;
                break;
            case Define.UnitType.ToxicFlower:
                _hp = 300;
                _maxHp = 300;
                _attackDamage = 8;
                _moveSpeed = 6.5f;
                _attackRange = 10.0f;
                break;
            case Define.UnitType.DrillDuck:
                _hp = 1200;
                _maxHp = 1200;
                _attackDamage = 15;
                _moveSpeed = 8.0f;
                _attackRange = 3.0f;
                break;
            #endregion
            #region State 2
            case Define.UnitType.Crab:          // stage 2
                _hp = 300;
                _maxHp = 300;
                _attackDamage = 6;
                _moveSpeed = 6.0f;
                _attackRange = 2.3f;
                break;
            case Define.UnitType.Fishman:
                _hp = 380;
                _maxHp = 380;
                _attackDamage = 7;
                _moveSpeed = 6.0f;
                _attackRange = 2.3f;
                break;
            case Define.UnitType.NagaWizard:
                _hp = 300;
                _maxHp = 300;
                _attackDamage = 10;
                _moveSpeed = 5.5f;
                _attackRange = 10.0f;
                break;
            case Define.UnitType.Crocodile:
                _hp = 1700;
                _maxHp = 1700;
                _attackDamage = 22;
                _moveSpeed = 8.0f;
                _attackRange = 6.0f;
                break;
            #endregion
            #region State 3
            case Define.UnitType.Demon:         // stage 3
                _hp = 380;
                _maxHp = 380;
                _attackDamage = 14;
                _moveSpeed = 5.5f;
                _attackRange = 10.0f;
                break;
            case Define.UnitType.Salamander:
                _hp = 380;
                _maxHp = 380;
                _attackDamage = 12;
                _moveSpeed = 5.5f;
                _attackRange = 10.0f;
                break;
            case Define.UnitType.Specter:
                _hp = 380;
                _maxHp = 380;
                _attackDamage = 25;
                _moveSpeed = 6.5f;
                _attackRange = 4.0f;
                break;
            case Define.UnitType.Skeleton:
                _hp = 330;
                _maxHp = 330;
                _attackDamage = 25;
                _moveSpeed = 5.5f;
                _attackRange = 2.5f;
                break;
            case Define.UnitType.IceKing:
                _hp = 2300;
                _maxHp = 2300;
                _attackDamage = 25;
                _moveSpeed = 8.0f;
                _attackRange = 5.0f;
                break;
            #endregion
            case Define.UnitType.MonsterKing:   // Final Stage
                _hp = 5000;
                _maxHp = 5000;
                _attackDamage = 30;         // 사용안함
                _moveSpeed = 8.0f;
                _attackRange = 8.0f;
                break;        
        }
    }


}
