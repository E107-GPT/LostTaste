using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPersist : MonoBehaviour
{
    private static bool playerExists = false;
    private static bool maincameraExists = false;

    public enum ObjectType
    {
        player,
        MainCamera,
        Guest
    }

    public ObjectType objectType;

    void Awake()
    {
        switch (objectType)
        {
            case ObjectType.player:
                if (!playerExists)
                {
                    playerExists = true;
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
    public void Init()
    {
        switch (objectType)
        {
            case ObjectType.player:
                if (!playerExists)
                {
                    playerExists = true;
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
