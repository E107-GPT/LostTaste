using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedTest : MonoBehaviour
{
    public int seed = 0;
    // Start is called before the first frame update
    System.Random rng;
    void Start()
    {
        rng = new System.Random(seed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitState()
    {
        int rndseed = rng.Next();        
        float randomSeed = Random.Range(0, 100);
        
        Debug.Log(rndseed);
    }
}
