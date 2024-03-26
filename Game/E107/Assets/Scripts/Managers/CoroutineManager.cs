using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoroutineManager
{
    Managers _parent;

    public void Init(Managers parent)
    {
        _parent = parent;
    }

    public void Run(IEnumerator coroutine)
    {
        _parent.StartCoroutine(coroutine);
    }
}
