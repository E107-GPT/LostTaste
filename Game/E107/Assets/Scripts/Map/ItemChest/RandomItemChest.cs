using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemChest : ItemChest
{
    public ItemChestType ChestType;
    public UInt16 ItemTierRandomKey;
    public UInt16 ItemTypeRandomKey;

    public override GameObject GetItemPrefab()
    {
        var chestTable = ItemDropTables.CHEST_TABLES[(int)ChestType];
        var items = Managers.Random.Randomizer.GetFromTable(ItemTierRandomKey, chestTable);
        return Managers.Random.Randomizer.Get(ItemTypeRandomKey, items);
    }
}
