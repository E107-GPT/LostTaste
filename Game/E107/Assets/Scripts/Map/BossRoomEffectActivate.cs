using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomEffectActivate : MonoBehaviour
{
    public GameObject bossRoomEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bossRoomEffect.SetActive(true);
        }
    }
}
