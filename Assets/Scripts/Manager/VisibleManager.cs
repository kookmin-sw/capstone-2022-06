using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleManager
{
    /*
    아군에게 스캔된 유닛이 몇 번 스캔되었는지 관리하는 딕셔너리
    예를 들어 value가 2면 아군에게 2번 스캔된 오브젝트임
    */
    Dictionary<Transform, int> visibleObjects = new Dictionary<Transform, int>();

    /// <summary>
    /// 인자로 들어온 게임 오브젝트를 visibleObjects value에 1 추가하고 처음 추가되면 OnRenderer 호출
    /// </summary>
    public void IncreaseVisible(Transform unit)
    {
        if (visibleObjects.ContainsKey(unit))
        {
            ++visibleObjects[unit];
        }
        else
        {
            Util.OnRenderer(unit);
            visibleObjects[unit] = 1;
        }
    }

    /// <summary>
    /// 인자로 들어온 게임 오브젝트를 visibleObjects value에 1 빼고 0 이하면 OffRenderer 호출
    /// </summary>
    public void ReduceVisible(Transform unit)
    {
        if (!visibleObjects.ContainsKey(unit))
        {
            return;
        }

        --visibleObjects[unit];

        if (visibleObjects[unit] <= 0)
        {
            Util.OffRenderer(unit);
            visibleObjects.Remove(unit);
        }
    }
}