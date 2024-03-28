using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 상자 확률표 (컨설턴트님 오열)
/// </summary>
public class ItemDropTables
{
    public static readonly GameObject[] COMMON_ITEMS = new GameObject[] {
        Load("0001_Cucumber"),
        Load("0027_Log"),
        Load("0025_Bibimbap"),
    };

    public static readonly GameObject[] UNCOMMON_ITEMS = new GameObject[] {
        Load("0004_Boomerang"),
        Load("0012_HeroSword"),
        Load("0019_FryingPan"),
        Load("0028_BubbleWand"),
        Load("0035_BoardApple"),
    };

    public static readonly GameObject[] RARE_ITEMS = new GameObject[] {
        Load("0014_GalaxyZzz"),
        Load("0029_RareSteak"),
    };

    public static readonly GameObject[] EPIC_ITEMS = new GameObject[] {
        Load("0008_SemUmbrella"),
    };

    public static readonly GameObject[] LEGENDARY_ITEMS = new GameObject[] {
        Load("0020_SixTimesBibimbap"),
    };

    public static readonly GameObject[] BOSS_ITEMS = new GameObject[] {
        Load("0031_MiniDrill")
    };

    public static readonly ProbabilityTable<GameObject[]> WOODEN_CHEST_TABLE = new ProbabilityTable<GameObject[]> {
        { COMMON_ITEMS,     43 },
        { UNCOMMON_ITEMS,   33 },
        { RARE_ITEMS,       23 },
        { EPIC_ITEMS,        1 },
        { LEGENDARY_ITEMS,   0 },
    };

    public static readonly ProbabilityTable<GameObject[]> BETTER_CHEST_TABLE = new ProbabilityTable<GameObject[]> {
        { COMMON_ITEMS,     20 },
        { UNCOMMON_ITEMS,   30 },
        { RARE_ITEMS,       39 },
        { EPIC_ITEMS,       10 },
        { LEGENDARY_ITEMS,   1 },
    };

    public static readonly ProbabilityTable<GameObject[]> GOLDEN_CHEST_TABLE = new ProbabilityTable<GameObject[]> {
        { COMMON_ITEMS,      5 },
        { UNCOMMON_ITEMS,   22 },
        { RARE_ITEMS,       30 },
        { EPIC_ITEMS,       40 },
        { LEGENDARY_ITEMS,   3 },
    };

    public static readonly ProbabilityTable<GameObject[]>[] CHEST_TABLES =
    {
        WOODEN_CHEST_TABLE,
        BETTER_CHEST_TABLE,
        GOLDEN_CHEST_TABLE
    };

    private static GameObject Load(string name)
    {
        return Managers.Resource.Load<GameObject>("Prefabs/Weapons/" + name);
    }
}

public enum ItemChestType
{
    WOODEN = 0,
    BETTER = 1,
    GOLDEN = 2,
}