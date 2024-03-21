using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pattern : MonoBehaviour
{
    public static int _seq;
    protected string _patternName;
    protected float _lastCastTime;
    protected float _patternCoolDownTime;
    protected Transform _root;
    // protected Transform _effect;
    protected ParticleSystem _slideEffect;
    protected GameObject _skillObj;

    public string PatternName
    {
        get { return _patternName; }
        set { _patternName = value; }
    }
    public float LastCastTime { get { return _lastCastTime; } set { _lastCastTime = value; } }
    public float PatternCoolDownTime
    {
        get { return _patternCoolDownTime; }
        set { _patternCoolDownTime = value; }
    }

    public Transform Root
    {
        get { return _root; }
        set { _root = value; }
    }
    //public Transform Effect
    //{
    //    get { return _effect; }
    //    set { _effect = value; }
    //}
    public ParticleSystem SlideEffect { get { return _slideEffect; } set { _slideEffect = value; } }
    public GameObject SkillObj {  get { return _skillObj; } set { _skillObj = value; } }

    protected virtual void Init() 
    {
        Root = transform.root;
        // Effect = transform.root.Find("Effect");
    }

    void Start()
    {
        Init();
    }

    public abstract void SetCollider(int attackDamage);
    public abstract void DeActiveCollider();
}
