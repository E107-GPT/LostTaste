using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �� ���� ���� ��ü�� ������ Info�� ������.
public class MonsterInfo : MonoBehaviour
{
    protected MonsterController _controller;

    protected Define.UnitType _unitType;
    protected int _attackDamage;
    protected float _attackRange;

    protected Skill _skill;             // �� ���Ͱ� ���� �⺻ ����
    protected List<Pattern> _patterns;   // �� ���Ͱ� ���� ���� ����


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

        Debug.Log($"Normal Attack - " + _unitType.ToString());
    }

}
