using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObject : MonoBehaviour
{
    private int _damage;
    private int _id;
    private Transform _attacker;

    private void Start()
    {
    }

    // skill script에서 Init()을 호출해서 세팅한다.
    public void Setup(Transform attacker, int damage, int id)
    {
        _damage = damage;
        _id = id;
        _attacker = attacker;
    }

    private void OnPlay()
    {
        // EFFECT PLAY

        // Sound Play
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player와 Ground가 아닌 Object에 부딪히면 없애고 싶음
        if ( _attacker.gameObject.CompareTag("Player") && !other.transform.CompareTag("Ground")  && !other.transform.CompareTag("Player") )
        {
            Debug.Log($"attack: {other.transform.name}");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (other.transform.CompareTag("Monster"))
            {
                other.gameObject.GetComponent<MonsterController>().TakeDamage(_id, _damage);
                OnPlay();
                Destroy(gameObject);
            }
            else
            {
                OnPlay();
                Destroy(gameObject);
            }
        }
    }
}
