using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null, bool isPrivate = false)
    {
        string destination;

        if (isPrivate)
        {
            destination = $"Private/Prefabs/{path}";
        }
        else
        {
            destination = $"Prefabs/{path}";
        }

        GameObject prefab = Load<GameObject>(destination);
        if (prefab == null)
        {
            Debug.Log($"failed to load prefab : {path}");
            return null;
        }

        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
