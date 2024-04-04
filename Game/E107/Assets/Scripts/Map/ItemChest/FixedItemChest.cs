using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedItemChest : ItemChest
{
    public GameObject itemPrefab;

    public override GameObject GetItemPrefab()
    {
        return itemPrefab;
    }
}
