using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int seed = 1821983547;
        Managers.Random.SetSeed(seed);

        Debug.Log("·£´ý °á°ú=======");
        Debug.Log("Raw 7: " + Managers.Random.Randomizer.GetRaw(7));
        Debug.Log("Raw 117: " + Managers.Random.Randomizer.GetRaw(117));
        Debug.Log("Double 7: " + Managers.Random.Randomizer.GetDouble(7));
        Debug.Log("Golden ItemTier 7: " + Managers.Random.Randomizer.GetFromTable(7, Managers.Item.RandomChestTables[ItemChestType.GOLDEN]));
        Debug.Log("Range 0~(Uncommon Item size) 117: " + Managers.Random.Randomizer.GetInt(117, 0, Managers.Item.ChestItemDictionary[ItemTier.UNCOMMON].Count));
        Debug.Log("Uncommon Item 117: " + Managers.Random.Randomizer.Get(117, Managers.Item.ChestItemDictionary[ItemTier.UNCOMMON]).name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
