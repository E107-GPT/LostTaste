using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected int _attackDamage = 0;

    float _attackRange = 8.0f;

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
        _normalAttackCollider = _normalAttackObj.AddComponent<BoxCollider>();
        _normalAttackObj.transform.localScale = new Vector3(1.0f, 1.0f, _attackRange);
        _normalAttackObj.SetActive(false);
        //Object.Instantiate(_normalAttackObj);
    }
    public void NormalAttack()
    {
        StartCoroutine(NormalAttackCorotine());
    }

    IEnumerator NormalAttackCorotine()
    {
        Debug.Log("123");
        _normalAttackObj.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        Transform root = gameObject.transform.root;

        _normalAttackObj.transform.position = root.transform.TransformPoint(Vector3.forward * (_attackRange / 2));
        //_normalAttackObj.transform.position = root.position + root.forward * (_attackRange/2);
        _normalAttackObj.transform.position = new Vector3(_normalAttackObj.transform.position.x, root.position.y + 0.5f, _normalAttackObj.transform.position.z);
        _normalAttackObj.transform.rotation = root.rotation;


        yield return new WaitForSeconds(0.3f);
        _normalAttackObj.SetActive(false);


    }

    public void SkillAttack()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (transform.root.GetComponent<PlayerController>().CurState is SkillState && other.gameObject.CompareTag("Monster"))
        {
            // 적의 HP를 참조하여 감소시킵니다.
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1, _attackDamage);
            }
        }
        
    }


}
