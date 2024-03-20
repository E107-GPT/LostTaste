using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager
{
    private KeyedRandomizer _randomizer;

    public KeyedRandomizer Randomizer { get; }

    public RandomManager()
    {
        setSeed(1); // TODO: 최초의 랜덤 시드는 어떻게?
    }

    public void setSeed(int seed)
    {
        _randomizer = new KeyedRandomizer(seed);
    }
}
