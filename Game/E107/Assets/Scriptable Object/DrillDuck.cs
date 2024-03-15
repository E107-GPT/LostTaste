using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDuck : MonoBehaviour
{
    [SerializeField]
    private StatTest drillDucDdata;
    public StatTest DrillDuckData { set; get; }


    public void PrintText()
    {
        Debug.Log($"DrillDuck : {drillDucDdata.Name}");
    }
}
