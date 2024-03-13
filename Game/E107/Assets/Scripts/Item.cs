using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected int _attackDamage = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (transform.root.GetComponent<PlayerController>().State == Define.State.Skill && other.gameObject.CompareTag("Monster"))
        {
            // 적의 HP를 참조하여 감소시킵니다.
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(1, _attackDamage);
            }
        }

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
