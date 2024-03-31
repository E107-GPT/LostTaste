using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemChest : MonoBehaviour, IPlayerInteractable
{
    [field: SerializeField]
    public string Name { get; set; }

    private static Dictionary<ItemTier, string> SOUND_DICTIONARY = new Dictionary<ItemTier, string>()
    {
        { ItemTier.COMMON,      "item_common"       },
        { ItemTier.UNCOMMON,    "item_uncommon"     },
        { ItemTier.RARE,        "item_rare"         },
        { ItemTier.EPIC,        "item_epic"         },
        { ItemTier.LEGENDARY,   "item_legendary"    },
        { ItemTier.BOSS,        "item_boss"         },
    };

    public void OnInteracted(GameObject player)
    {
        GameObject itemPrefab = GetItemPrefab();
        Item item = itemPrefab.GetComponent<Item>();

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate($"Prefabs/Weapons/{itemPrefab.name}", gameObject.transform.position, new Quaternion());
        }

        Animator animator = GetComponent<Animator>();
        animator.SetBool("Opened", true);

        Collider collider = GetComponent<Collider>();
        collider.enabled = false;

        Managers.Sound.Play(SOUND_DICTIONARY[item.Tier]);
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