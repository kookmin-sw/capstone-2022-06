using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStat : ObjectStat
{
    public override void Initialize(string name)
    {
        stat.Init(Managers.Data.MinionStatDict[name]);
    }
}
