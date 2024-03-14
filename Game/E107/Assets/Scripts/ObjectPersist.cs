using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPersist : MonoBehaviour
{
    private static bool unitychanExists = false;
    private static bool maincameraExists = false;

    public enum ObjectType
    {
        unitychan,
        MainCamera,
    }

    public ObjectType objectType;

    void Awake()
    {
        switch (objectType)
        {
            case ObjectType.unitychan:
                if (!unitychanExists)
                {
                    unitychanExists = true;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
                break;  
            case ObjectType.MainCamera:
                if (!maincameraExists)
                {
                    maincameraExists = true;
                    DontDestroyOnLoad(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}