using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null) component = go.AddComponent<T>();
        return component;

    }
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        // 모든 GameObject는 Transform을 가진다.
        // Transform을 찾아서 그녀석의 gameObject를 반환해준다.

        if (transform == null) return null;

        return transform.gameObject;
    }
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null) return null;

        if (!recursive)
        {
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    // tranform을 이용해서 이름에 맞는거 찾음
                    // 그 transform의 실제 GameObject를 반환해서 리턴시켜주는것
                    T component = transform.GetComponent<T>();
                    if (component != null) return component;

                }
            }
        }
        else
        {
            var tmp = go.GetComponentsInChildren<T>();
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                // name으로 아무것도 안받으면 type에 맞는거 하나 그냥 반환 해준다.
                if (string.IsNullOrEmpty(name) || component.name == name) return component;
            }
        }

        return null;
    }
}
