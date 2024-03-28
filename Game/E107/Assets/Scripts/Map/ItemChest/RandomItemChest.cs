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
        ProbabilityTable<ItemTier> chestTable = Managers.Item.RandomChestTables[ChestType];
        ItemTier itemTier = Managers.Random.Randomizer.GetFromTable(ItemTierRandomKey, chestTable);
        return Managers.Random.Randomizer.Get(ItemTypeRandomKey, Managers.Item.ChestItemDictionary[itemTier]);
    }
}
