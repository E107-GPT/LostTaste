using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomManager
{
    private KeyedRandomizer _randomizer;

    public KeyedRandomizer Randomizer { get { return _randomizer; } }


    public void Init()
    {
        SetSeed(1); // TODO: ������ ���� �õ�� ���?
    }

    public void SetSeed(int seed)
    {
        _randomizer = new KeyedRandomizer(seed);
    }
}
