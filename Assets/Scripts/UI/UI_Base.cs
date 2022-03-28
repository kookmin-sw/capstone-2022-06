using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _uiobjects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    private void Start()
    {
        Init();
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = type.GetEnumNames();
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];

        if (typeof(T) == typeof(GameObject))
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i] = Util.SearchChild(gameObject, names[i], true);
            }
        }
        else
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i] = Util.SearchChild<T>(gameObject, names[i], true);
            }
        }

        _uiobjects.Add(typeof(T), objects);
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects;

        if (!_uiobjects.TryGetValue(typeof(T), out objects))
        {
            return null;
        }

        return objects[idx] as T;
    }
}
