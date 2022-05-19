using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQStat : ObjectStat
{
    /// <summary> 본부의 스탯을 초기화 하는 메서드 /// </summary>
    public override void Initialize(string name)
    {
        stat.Init(Managers.Data.HQStatDict[name]);
    }
}
