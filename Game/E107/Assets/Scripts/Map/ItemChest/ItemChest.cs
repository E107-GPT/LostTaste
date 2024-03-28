using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemChest : MonoBehaviour, IPlayerInteractable
{
    public void OnInteracted(GameObject player)
    {
        GameObject itemObject = Instantiate(GetItemPrefab());
        itemObject.transform.position = gameObject.transform.position;

        itemObject.GetComponent<Item>().OnDropped();

        Animator animator = GetComponent<Animator>();
        animator.SetBool("Opened", true);

        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public abstract GameObject GetItemPrefab();
}

public enum ItemChestType
{
    WOODEN = 0,
    BETTER = 1,
    GOLDEN = 2,
    BOSS = 3,
}