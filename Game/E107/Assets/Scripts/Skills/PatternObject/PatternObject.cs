using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternObject : MonoBehaviour
{
    int _damage;
    int _id;
    Transform _attacker;

    void Start()
    {
        
    }

    public void Init(Transform attacker, int damage, int id)
    {
        _damage = damage;
        _id = id;
        _attacker = attacker;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (_attacker.gameObject.CompareTag("Monster") && other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Monster Target: {other.gameObject.name}");

            other.gameObject.GetComponent<PlayerController>().TakeDamage(_id, _damage);
        }

    }
}
