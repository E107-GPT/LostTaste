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
        // ��� GameObject�� Transform�� ������.
        // Transform�� ã�Ƽ� �׳༮�� gameObject�� ��ȯ���ش�.

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
                    // tranform�� �̿��ؼ� �̸��� �´°� ã��
                    // �� transform�� ���� GameObject�� ��ȯ�ؼ� ���Ͻ����ִ°�
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
                // name���� �ƹ��͵� �ȹ����� type�� �´°� �ϳ� �׳� ��ȯ ���ش�.
                if (string.IsNullOrEmpty(name) || component.name == name) return component;
            }
        }

        return null;
    }
}
