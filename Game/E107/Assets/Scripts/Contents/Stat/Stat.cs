using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected float _moveSpeed;
    [SerializeField]
    private float _attackRange;
    [SerializeField]
    private float _detectRange;

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float AttackRange
    {
        set => _attackRange = value;
        get => _attackRange;
    }
    public float DetectRange
    {
        set => _detectRange = value;
        get => _detectRange;
    }

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _moveSpeed = 5.0f;
        _attackRange = 1.8f;
        _detectRange = 15.0f;
    }
    public virtual void OnAttacked(Stat attacker)
    {
        int damage = attacker.Attack;
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
