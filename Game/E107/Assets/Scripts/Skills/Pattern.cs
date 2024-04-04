using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern : MonoBehaviour
{
    public static int _seq;
    protected string _patternName;
    protected Transform _root;
    protected GameObject _skillObj;

    public string PatternName
    {
        get { return _patternName; }
        set { _patternName = value; }
    }

    public Transform Root
    {
        get { return _root; }
        set { _root = value; }
    }
    public GameObject SkillObj {  get { return _skillObj; } set { _skillObj = value; } }

    protected virtual void Init() 
    {
        Root = transform.root;
    }

    void Start()
    {
        Init();
    }

    public abstract void SetCollider();
    public abstract void SetCollider(int attackDamage);
    public abstract void DeActiveCollider();
}
