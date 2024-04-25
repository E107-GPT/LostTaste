using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SkillObject : MonoBehaviour
{
    public static int _newId;
    int _damage;
    int _id;
    protected Transform _attacker;
    int _penetration;

    public void SetUp(Transform attacker, int damage, int id)
    {
        SetUp(attacker, damage, id, -1);
    }

    public void SetUp(Transform attacker, int damage, int id, int penetration)
    {
        
        _damage = damage;
        _id = _newId++;
        if (id == -1)
        {
            _id = -1;
        }
        
        Debug.Log(_id);
        _attacker = attacker;
        _penetration = penetration;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        if (_attacker.gameObject.CompareTag("Player") && other.gameObject.CompareTag("Monster"))
        {
            Debug.Log($"{other.gameObject.name}");

            other.gameObject.GetComponent<MonsterController>().TakeDamage(_id, _damage);
            _penetration--;
        }
        else if (_attacker.gameObject.CompareTag("Monster") && other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Monster Target: {other.gameObject.name}");
            
            other.gameObject.GetComponent<PlayerController>().TakeDamage(_id, _damage);
            _penetration--;
        }

        if (_penetration == 0)
        {
            OnBreak(other);
            Managers.Resource.Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }

    /// <summary>
    /// 관통 횟수가 모두 소진되어 부서졌을 때 발동할 이벤트
    /// </summary>
    protected virtual void OnBreak(Collider other) { }
}
