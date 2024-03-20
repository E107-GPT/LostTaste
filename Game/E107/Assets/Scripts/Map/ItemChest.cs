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
        var chestTable = ItemDropTables.CHEST_TABLES[(int) ChestType];
        var items = Managers.Random.Randomizer.GetFromTable(ItemTierRandomKey, chestTable);
        GameObject itemPrefab = Managers.Random.Randomizer.Get(ItemTypeRandomKey, items);

        GameObject itemObject = Instantiate(itemPrefab);
        itemObject.transform.position = gameObject.transform.position;

        itemObject.GetComponent<Item>().OnDropped();

        Animator animator = GetComponent<Animator>();
        animator.SetBool("Opened", true);

        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
    }
}
