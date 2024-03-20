using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager
{
    private KeyedRandomizer _randomizer;

    public KeyedRandomizer Randomizer { get; }

    public RandomManager()
    {
        setSeed(1); // TODO: ������ ���� �õ�� ���?
    }

    public void setSeed(int seed)
    {
        _randomizer = new KeyedRandomizer(seed);
    }
}
