using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuckItem : MonoBehaviour
{
    [SerializeField]
    protected int _attackDamage = 0;
    [SerializeField]
    private float _attackRange = 4.0f;

    GameObject _normalAttackObj;
    BoxCollider _normalAttackCollider;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    protected void Init()
    {
        _normalAttackObj = new GameObject("NormalAttack");
        _normalAttackObj.AddComponent<SkillObject>();
        _normalAttackCollider = _normalAttackObj.AddComponent<BoxCollider>();
        _normalAttackCollider.isTrigger = true;
        _normalAttackObj.transform.localScale = new Vector3(1.0f, 1.0f, _attackRange);
        _normalAttackObj.SetActive(false);
        //Object.Instantiate(_normalAttackObj);
    }
    public void NormalAttack()
    {
        // StartCoroutine(NormalAttackCorotine());
        InvokeRepeating("StartNormalAttack", 0.0f, 0.6f);
        InvokeRepeating("EndNormalAttack", 0.3f, 0.6f);
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

    private void StartNormalAttack()
    {
        Debug.Log("Normal Attack");
        _normalAttackObj.SetActive(true);
        Transform root = gameObject.transform.root;

        _normalAttackObj.transform.position = root.transform.TransformPoint(Vector3.forward * (_attackRange / 4));
        //_normalAttackObj.transform.position = root.position + root.forward * (_attackRange/2);
        _normalAttackObj.transform.position = new Vector3(_normalAttackObj.transform.position.x - 1.0f, root.position.y + 0.5f, _normalAttackObj.transform.position.z);
        _normalAttackObj.transform.localScale = new Vector3(2.5f, 5, 3);
        _normalAttackObj.transform.rotation = root.rotation;
    }

    private void EndNormalAttack()
    {
        _normalAttackObj.SetActive(false);
    }

    public void CancelNormalAttack()
    {
        CancelInvoke("StartNormalAttack");
        CancelInvoke("EndNormalAttack");
    }

    public void SkillAttack()
    {



    }
}
