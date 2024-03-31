using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemChest : MonoBehaviour, IPlayerInteractable
{
    [field: SerializeField]
    public string Name { get; set; }

    public void OnInteracted(GameObject player)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject itemObject = PhotonNetwork.Instantiate($"Prefabs/Weapons/{GetItemPrefab().name}", gameObject.transform.position, new Quaternion());

        }
        //GameObject itemObject = Instantiate(GetItemPrefab());
        //itemObject.transform.position = gameObject.transform.position;

        //itemObject.GetComponent<Item>().OnDropped();

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