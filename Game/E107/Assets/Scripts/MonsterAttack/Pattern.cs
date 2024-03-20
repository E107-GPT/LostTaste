using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern : MonoBehaviour
{
    public static int _seq;
    protected string _skillName;
    protected float _lastCastTime;
    protected float _skillCoolDownTime;
    protected Transform _root;
    protected Transform _effect;
    protected GameObject _skillObj;

    public string SkillName
    {
        get { return _skillName; }
        set { _skillName = value; }
    }
    public float LastCastTime { get { return _lastCastTime; } set { _lastCastTime = value; } }
    public float SkillCoolDownTime
    {
        get { return _skillCoolDownTime; }
        set { _skillCoolDownTime = value; }
    }

    public Transform Root
    {
        get { return _root; }
        set { _root = value; }
    }
    public Transform Effect
    {
        get { return _effect; }
        set { _effect = value; }
    }
    public GameObject SkillObj {  get { return _skillObj; } set { _skillObj = value; } }

    protected virtual void Init() 
    {
        Root = transform.root;
        Effect = transform.root.Find("Effect");

        _skillObj = Managers.Resource.Instantiate("Skills/SkillObject");
        _skillObj.SetActive(false);
    }

    void Start()
    {
        Init();
    }

    public abstract void SetCollider(int _attackDamage, float _attackRange);
    public abstract void DeActiveCollider();
}
