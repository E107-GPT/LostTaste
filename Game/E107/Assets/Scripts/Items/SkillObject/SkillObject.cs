using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    // Start is called before the first frame update
    int _damage;
    int _id;
    Transform _attacker;

    private void Start()
    {
    }


    public void SetUp(Transform attacker, int damage, int id)
    {
        _damage = damage;
        _id = id;
        _attacker = attacker;
    }




    private void OnTriggerEnter(Collider other)
    {

        if (_attacker.gameObject.CompareTag("Player") && other.gameObject.CompareTag("Monster"))
        {
            Debug.Log($"{other.gameObject.name}");

            other.gameObject.GetComponent<MonsterController>().TakeDamage(_id, _damage);
        }
        else if (_attacker.gameObject.CompareTag("Monster") && other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Monster Target: {other.gameObject.name}");

            other.gameObject.GetComponent<PlayerController>().TakeDamage(_id, _damage);
        }

    }

}
