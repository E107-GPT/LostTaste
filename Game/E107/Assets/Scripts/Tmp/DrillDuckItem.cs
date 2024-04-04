using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckItem : MonoBehaviour
{
    [SerializeField]
    protected int _attackDamage;
    [SerializeField]
    protected int _patternDamage;
    [SerializeField]
    protected float _attackRange;
    
    private bool _isNormalAttack;
    private bool _isSlideAttack;

    private GameObject _normalAttackObj;
    private BoxCollider _normalAttackCollider;
    private GameObject _slideAttackObj;
    private BoxCollider _slideAttackCollider;



    void Start()
    {
        Init();
    }

    protected void Init()
    {
        _attackDamage = gameObject.GetComponent<DrillDuckController>().Stat.AttackDamage;
        _patternDamage = gameObject.GetComponent<DrillDuckController>().Stat.PatternDamage;
        _attackRange = gameObject.GetComponent<DrillDuckController>().Stat.AttackRange;

        _normalAttackObj = new GameObject("NormalAttack");
        _normalAttackObj.AddComponent<SkillObject>();
        _normalAttackCollider = _normalAttackObj.AddComponent<BoxCollider>();
        _normalAttackCollider.isTrigger = true;
        //_normalAttackObj.transform.localScale = new Vector3(1.0f, 1.0f, _attackRange);
        _normalAttackObj.transform.localScale = new Vector3(2.5f, 5.0f, 3.0f);
        _normalAttackObj.SetActive(false);
        //Object.Instantiate(_normalAttackObj);

        _slideAttackObj = new GameObject("SildeAttack");
        _slideAttackObj.AddComponent<SkillObject>();
        _slideAttackCollider = _slideAttackObj.AddComponent<BoxCollider>();
        _slideAttackCollider.isTrigger = true;
        _slideAttackObj.transform.localScale = new Vector3(4.0f, 5.0f, 5.0f);
        _slideAttackObj.SetActive(false);

        _isNormalAttack = false;
        _isSlideAttack = false;
    }
    public void NormalAttack()
    {
        // StartCoroutine(NormalAttackCorotine());
        //InvokeRepeating("StartNormalAttack", 0.0f, 0.6f);
        //InvokeRepeating("EndNormalAttack", 0.3f, 0.6f);
        if (_isNormalAttack)
        {
            StartNormalAttack();
        }
        else
        {
            _normalAttackObj.SetActive(false);
        }
    }

    public void TrueNormalAttack()
    {
        _isNormalAttack = true;
    }
    public void FalseNormalAttack()
    {
        _isNormalAttack = false;
    }
    private void StartNormalAttack()
    {
        Debug.Log("Normal Attack - DrillDuck");

        _normalAttackObj.GetComponent<SkillObject>().SetUp(transform, _attackDamage, 2);      // 현재 스킬의 ID = 2

        Transform root = gameObject.transform.root;
        _normalAttackObj.transform.position = root.transform.TransformPoint(Vector3.forward * (_attackRange / 4));
        _normalAttackObj.transform.position = new Vector3(_normalAttackObj.transform.position.x - 1.0f, root.position.y + 0.5f, _normalAttackObj.transform.position.z);
        //_normalAttackObj.transform.localScale = new Vector3(2.5f, 5, 3);
        _normalAttackObj.transform.rotation = root.rotation;

        _normalAttackObj.SetActive(true);
    }


    public void PatternAttack()
    {
        if (_isSlideAttack)
        {
            SlideAttack();
        }
        else
        {
            _slideAttackObj.SetActive(false);
        }
    }

    private void SlideAttack()
    {
        Debug.Log("Slide Attack - DrillDuck");

        _slideAttackObj.GetComponent<SkillObject>().SetUp(transform, _patternDamage, 3);
        Transform root = gameObject.transform.root;
        _slideAttackObj.transform.position = root.transform.TransformPoint(Vector3.forward);
        _slideAttackObj.transform.position = new Vector3(_slideAttackObj.transform.position.x, root.position.y + 0.5f, _slideAttackObj.transform.position.z);
        _slideAttackObj.transform.rotation = root.rotation;

        _slideAttackObj.SetActive(true);
    }

    public void TrueSlideAttack()
    {
        _isSlideAttack = true;
    }
    public void FalseSlideAttack()
    {
        _isSlideAttack = false;
    }

    IEnumerator NormalAttackCorotine()
    {
        Debug.Log("Normal Attack");

        yield return new WaitForSeconds(0.3f);
        _normalAttackObj.SetActive(true);
        Transform root = gameObject.transform.root;

        _normalAttackObj.transform.position = root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        //_normalAttackObj.transform.position = root.position + root.forward * (_attackRange/2);
        _normalAttackObj.transform.position = new Vector3(_normalAttackObj.transform.position.x, root.position.y + 0.5f, _normalAttackObj.transform.position.z);
        _normalAttackObj.transform.rotation = root.rotation;


        yield return new WaitForSeconds(0.3f);
        _normalAttackObj.SetActive(false);


    }
}
