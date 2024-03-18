using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public float damageCooldown = 1f; // ���ظ� �ٽ� �ޱ������ ��� �ð�(��)

    // �������� ������ ���� �ð��� �����ϴ� ����
    private Dictionary<int, float> lastAttackTimes = new Dictionary<int, float>();

    void Start()
    {
        currentHealth = maxHealth;
    }

    // �������� ���� �ĺ���(��: GameObject�� Instance ID)�� ���ط��� �Ű������� �޽��ϴ�.
    public void TakeDamage(int attackerId, int amount)
    {
        float lastAttackTime;
        lastAttackTimes.TryGetValue(attackerId, out lastAttackTime);

        if (Time.time - lastAttackTime < damageCooldown)
        {
            // ��ٿ� ���̹Ƿ� ���ظ� ���� ����
            return;
        }

        currentHealth -= amount;
        lastAttackTimes[attackerId] = Time.time; // �ش� �������� ������ ���� �ð� ������Ʈ
        Debug.Log($"{currentHealth}!!!");

        if (currentHealth <= 0)
        {
            Debug.Log("Died...");
        }
    }

    void Die()
    {
        // �� ĳ���� ��� �� ����� ����
        Destroy(gameObject);
    }
}