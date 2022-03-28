using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static T SearchChild<T>(GameObject go, string name = null, bool pushdown = false) where T : UnityEngine.Object
    {
        if (!go)
        {
            return null;
        }

        if (pushdown)
        {
            foreach(T e in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || e.name == name)
                {
                    return e;
                }
            }
        }
        else
        {
            foreach(Transform child in go.transform)
            {
                if (string.IsNullOrEmpty(name) || child.name == name)
                {
                    T ret = child.GetComponent<T>();
                    if (ret)
                    {
                        return ret;
                    }
                }
            }
        }

        return null;
    }
}
