using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float damageCooldown = 1f; // 피해를 다시 받기까지의 대기 시간(초)

    // 공격자의 마지막 공격 시간을 저장하는 사전
    private Dictionary<int, float> lastAttackTimes = new Dictionary<int, float>();

    void Start()
    {
        currentHealth = maxHealth;
    }

    // 공격자의 고유 식별자(예: GameObject의 Instance ID)와 피해량을 매개변수로 받습니다.
    public void TakeDamage(int attackerId, int amount)
    {
        float lastAttackTime;
        lastAttackTimes.TryGetValue(attackerId, out lastAttackTime);

        if (Time.time - lastAttackTime < damageCooldown)
        {
            // 쿨다운 중이므로 피해를 주지 않음
            return;
        }

        currentHealth -= amount;
        lastAttackTimes[attackerId] = Time.time; // 해당 공격자의 마지막 공격 시간 업데이트
        Debug.Log($"{currentHealth}!!!");

        if (currentHealth <= 0)
        {
            Debug.Log("Died...");
        }
    }

    void Die()
    {
        // 적 캐릭터 사망 시 수행될 로직
        Destroy(gameObject);
    }
}