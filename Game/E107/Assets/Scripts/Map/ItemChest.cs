using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour, IPlayerInteractable
{
    public ItemChestType chestType;

    public void OnInteracted(GameObject player)
    {
        Animator animator = GetComponent<Animator>();

        animator.SetBool("Opened", true);
    }
}
