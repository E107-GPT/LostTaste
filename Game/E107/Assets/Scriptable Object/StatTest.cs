using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Monster Data", menuName = "Scriptable Object/Monster Data", order = int.MaxValue)]
public class StatTest : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private int _hp;
    [SerializeField]
    private int _maxHp;
    [SerializeField]
    private int _attackDamage;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _attackRange;

    public string Name { get { return _name; } set { _name = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float AttackRange { set => _attackRange = value; get => _attackRange; }

}
