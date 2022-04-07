using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleManager
{
    /*
    아군에게 스캔된 유닛이 몇 번 스캔되었는지 관리하는 딕셔너리
    예를 들어 value가 2면 아군에게 2번 스캔된 오브젝트임
    */
    Dictionary<GameObject, int> visibleObjects = new Dictionary<GameObject, int>();

    /*
    인자로 들어온 게임 오브젝트를 visibleObjects value에 1 추가하고 1 이상이면 enabled를 true로 바꿈
    */
    public void AddVisible(GameObject unit)
    {
        if (visibleObjects.ContainsKey(unit))
        {
            ++visibleObjects[unit];
        }
        else
        {
            visibleObjects[unit] = 1;
            Renderer renderer = unit.GetComponent<Renderer>();
            if (renderer)
            {
                renderer.enabled = true;
            }
        }
    }

    /*
    인자로 들어온 게임 오브젝트를 visibleObjects value에 1 빼고 0 이하면 enabled를 false로 바꿈
    */
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
