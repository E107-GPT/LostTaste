using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Connect;
using UnityEngine;

/// <summary>
/// 아이템 상자 확률표 (컨설턴트님 오열)
/// </summary>
public class ItemManager
{
    public readonly Dictionary<ItemTier, IList<GameObject>> ChestItemDictionary = new Dictionary<ItemTier, IList<GameObject>>();

    public readonly Dictionary<ItemChestType, ProbabilityTable<ItemTier>> RandomChestTables
        = new Dictionary<ItemChestType, ProbabilityTable<ItemTier>>()
        {
            { ItemChestType.WOODEN, new ProbabilityTable<ItemTier> {
                { ItemTier.COMMON,     43 },
                { ItemTier.UNCOMMON,   33 },
                { ItemTier.RARE,       23 },
                { ItemTier.EPIC,        1 },
                { ItemTier.LEGENDARY,   0 }
            }},
            { ItemChestType.BETTER, new ProbabilityTable<ItemTier> {
                { ItemTier.COMMON,     20 },
                { ItemTier.UNCOMMON,   30 },
                { ItemTier.RARE,       39 },
                { ItemTier.EPIC,       10 },
                { ItemTier.LEGENDARY,   1 },
            }},
            { ItemChestType.GOLDEN, new ProbabilityTable<ItemTier> {
                { ItemTier.COMMON,      5 },
                { ItemTier.UNCOMMON,   22 },
                { ItemTier.RARE,       30 },
                { ItemTier.EPIC,       40 },
                { ItemTier.LEGENDARY,   3 },
            }},
        };

    public void Init()
    {
        foreach (ItemTier tier in Enum.GetValues(typeof(ItemTier)))
        {
            ChestItemDictionary[tier] = new List<GameObject>();
        }

        UnityEngine.Object[] objects = Resources.LoadAll("Prefabs/Weapons");
        foreach (UnityEngine.Object obj in objects)
        {
            if (!(obj is GameObject)) continue;

            GameObject gameObject = obj as GameObject;

            Item item = gameObject.GetComponent<Item>();
            if (item == null) continue;
            if (item.IsHidden) continue;

            ChestItemDictionary[item.Tier].Add(gameObject);
        }
    }
}