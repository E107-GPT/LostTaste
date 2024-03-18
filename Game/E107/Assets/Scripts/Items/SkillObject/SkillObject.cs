using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    // Start is called before the first frame update
    int _damage;
    int _id;

    private void Start()
    {
        _id = 1;
        _damage = 10;
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log($"{other.gameObject.name}");

            other.gameObject.GetComponent<MonsterController>().TakeDamage(_id, _damage);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Monster Target: {other.gameObject.name}");

            other.gameObject.GetComponent<PlayerController>().TakeDamage(_id, _damage);
        }

    }

}
