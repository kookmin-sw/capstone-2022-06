using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretStat : ObjectStat
{
    public override void Initialize(string name)
    {
        stat.Init(Managers.Data.TurretStatDict[name]);
    }
}
