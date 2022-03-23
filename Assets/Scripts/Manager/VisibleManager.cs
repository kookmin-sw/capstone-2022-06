using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleManager
{
    Dictionary<GameObject, int> visibleObjects = new Dictionary<GameObject, int>();

    public void AddVisible(GameObject unit)
    {
        if (visibleObjects.ContainsKey(unit))
        {
            ++visibleObjects[unit];
        }
        else
        {
            Renderer renderer = unit.GetComponent<Renderer>();
            if (renderer)
            {
                renderer.enabled = true;
            }
        }
    }

    public void SubtractVisible(GameObject unit)
    {
        if (!visibleObjects.ContainsKey(unit))
        {
            return;
        }

        --visibleObjects[unit];

        if (visibleObjects[unit] <= 0)
        {
            Renderer renderer = unit.GetComponent<Renderer>();
            renderer.enabled = false;
            visibleObjects.Remove(unit);
        }
    }
}
