using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChest : MonoBehaviour, IPlayerInteractable
{
    public ItemDropTables.ChestType chestType;

    public void OnInteract(GameObject player)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteracted(GameObject player)
    {
        throw new System.NotImplementedException();
    }
}
