using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour, IPlayerInteractable
{
    public ItemChestType ChestType;
    public UInt16 ItemTierRandomKey;
    public UInt16 ItemTypeRandomKey;

    public void OnInteracted(GameObject player)
    {
        Animator animator = GetComponent<Animator>();

        animator.SetBool("Opened", true);
    }
}
