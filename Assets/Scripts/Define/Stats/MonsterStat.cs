using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStat : ObjectStat
{
    public override void Initialize(string name)
    {
        stat.Init(Managers.Data.MonsterStatDict[name]);
    }
}
